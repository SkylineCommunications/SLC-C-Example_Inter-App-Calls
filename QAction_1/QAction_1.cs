namespace Skyline.Common
{
	namespace Api.Calls.SlcSdfInterApp
	{
		using System.Collections.Generic;

		using Skyline.Common.InterApp.InterAppCalls.BaseCall;

		public class CreateLineup
		{
			public class Create : Message
			{
				public List<Models.Channel> Channels { get; set; }

				public string LineupName { get; set; }

				public string PoolId { get; set; }

				public string Type { get; set; }
			}

			public class ExpectedReturn : Message
			{
				public int LineUpId { get; set; }
			}

			public class Models
			{
				public class AudioMapping
				{
					public string AudioEncodedId { get; set; }

					public bool Merge { get; set; }
				}

				public class Channel
				{
					public string AudioTemplateId { get; set; }

					public string DensityId { get; set; }

					public string Name { get; set; }

					public Output Output { get; set; }

					public string SourceId { get; set; }

					public string TranscodingTemplateId { get; set; }

					public string XCodeType { get; set; }
				}

				public class Output
				{
					public List<Profile> Profiles { get; set; }
				}

				public class Profile
				{
					public List<AudioMapping> AudioMapping { get; set; }

					public string ProfileId { get; set; }

					public string TargetIp { get; set; }

					public int TargetUdp { get; set; }
				}
			}
		}

		public class DeleteLineup : Message
		{
			public string LineupId { get; set; }
		}

		public class DuplicateLineup : Message
		{
			public string FromLineupId { get; set; }

			public string ToLineupName { get; set; }
		}
	}

	namespace InterApp
	{
		namespace InterAppCalls
		{
			using System;
			using System.Collections;
			using System.Collections.Concurrent;
			using System.Collections.Generic;
			using System.Linq;
			using System.Reflection;
			using System.Threading;

			using Skyline.Common.Serializing;
			using Skyline.DataMiner.Library.Common;
			using Skyline.DataMiner.Net;
			using Skyline.DataMiner.Net.Messages;

			using SLNetHelper;

			namespace BaseCall
			{
				/// <summary>
				/// Factory class that can create InterAppCalls.
				/// </summary>
				public static class InterAppCallFactory
				{
					/// <summary>
					/// Creates an InterAppCall from a raw serialized string.
					/// </summary>
					/// <param name="rawData">The serialized raw data.</param>
					/// <returns>An InterAppCall.</returns>
					public static InterAppCall CreateFromRaw(string rawData)
					{
						if (String.IsNullOrWhiteSpace(rawData)) return null;
						var serializer = Serializing.SerializerFactory.CreateSerializer(typeof(InterAppCall));
						var returnedResult = serializer.DeserializeFromString<InterAppCall>(rawData);

						returnedResult.ReceivingTime = DateTime.Now;
						return returnedResult;
					}

					/// <summary>
					/// Creates an InterAppCall from the contents of a remote parameter somewhere.
					/// </summary>
					/// <param name="connection">The raw SLNet connection.</param>
					/// <param name="dataMinerId">The source dataminer id.</param>
					/// <param name="elementId">The source element id.</param>
					/// <param name="parameterId">The source parameter id.</param>
					/// <returns>An InterAppCall.</returns>
					public static InterAppCall CreateFromRemote(Skyline.DataMiner.Net.Connection connection, int dataMinerId, int elementId, int parameterId)
					{
						IDms thisDms = connection.GetDms();
						var element = thisDms.GetElement(new DmsElementId(dataMinerId, elementId));
						var parameter = element.GetStandaloneParameter<string>(parameterId);
						var returnedResultRaw = parameter.GetValue();
						return CreateFromRaw(returnedResultRaw);
					}

					/// <summary>
					/// Creates a blank InterAppCall.
					/// </summary>
					/// <returns>An InterAppCall.</returns>
					public static InterAppCall CreateNew()
					{
						return new InterAppCall();
					}
				}

				public static class MessageFactory
				{
					/// <summary>
					/// Creates a Message from a raw serializes string.
					/// </summary>
					/// <param name="rawData">The serialized raw data.</param>
					/// <returns>An interapp message.</returns>
					public static Message CreateFromRaw(string rawData)
					{
						if (String.IsNullOrWhiteSpace(rawData)) return null;
						var serializer = Serializing.SerializerFactory.CreateSerializer(typeof(Message));
						var returnedResult = serializer.DeserializeFromString<Message>(rawData);

						return returnedResult;
					}

					public static Message CreateFromRemote(Skyline.DataMiner.Net.Connection connection, int dataMinerId, int elementId, int parameterId)
					{
						IDms thisDms = connection.GetDms();
						var element = thisDms.GetElement(new DmsElementId(dataMinerId, elementId));
						var parameter = element.GetStandaloneParameter<string>(parameterId);
						var returnedResultRaw = parameter.GetValue();
						return CreateFromRaw(returnedResultRaw);
					}

					public static Message CreateNew()
					{
						return new Message();
					}
				}

				public class InterAppCall
				{
					public InterAppCall(string guid)
					{
						Guid = guid;
						Messages = new Messages(this);
					}

					public InterAppCall()
					{
						Guid = System.Guid.NewGuid().ToString();
						Messages = new Messages(this);
					}

					public string Guid { get; set; }

					public Messages Messages { get; set; }

					public DateTime ReceivingTime { get; set; }

					public ReturnAddress ReturnAddress { get; set; }

					public DateTime SendingTime { get; private set; }

					public Source Source { get; set; }

					public void Send(Skyline.DataMiner.Net.Connection connection, int dataMinerId, int elementId, int parameterId)
					{
						DmsElementId destination = new DmsElementId(dataMinerId, elementId);

						BubbleDownReturn();
						SendToElement(connection, destination, parameterId);
					}

					public IEnumerable<Message> Send(Skyline.DataMiner.Net.Connection connection, int dataMinerId, int elementId, int parameterId, TimeSpan timeout)
					{
						BubbleDownReturn();
						if (ReturnAddress != null)
						{
							using (MessageWaiter waiter = new MessageWaiter(connection, Messages.ToArray()))
							{
								DmsElementId destination = new DmsElementId(dataMinerId, elementId);
								SendToElement(connection, destination, parameterId);
								foreach (var returnedMessage in waiter.WaitNext(timeout))
								{
									yield return returnedMessage;
								}
							}
						}
					}

					public string Serialize()
					{
						var serializer = SerializerFactory.CreateSerializer(typeof(InterAppCall));
						return serializer.SerializeToString(this);
					}

					private void BubbleDownReturn()
					{
						foreach (var message in Messages)
						{
							if (message.Source == null && Source != null) message.Source = Source;
							if (ReturnAddress != null) message.ReturnAddress = ReturnAddress;
						}
					}

					private void SendToElement(Skyline.DataMiner.Net.Connection connection, DmsElementId destination, int parameterId)
					{
						IDms thisDms = connection.GetDms();
						var element = thisDms.GetElement(destination);
						if (element.State == DataMiner.Library.Common.ElementState.Active)
						{
							var parameter = element.GetStandaloneParameter<string>(parameterId);
							var serializer = Serializing.SerializerFactory.CreateSerializer(typeof(InterAppCall));
							SendingTime = DateTime.Now;
							string value = serializer.SerializeToString(this);
							parameter.SetValue(value);
						}
						else
						{
							throw new InvalidOperationException("Could not send message to element " + element.Name + "(" + element.DmsElementId + ")" + " with state " + element.State);
						}
					}
				}

				public class Message
				{
					public Message()
					{
						Guid = System.Guid.NewGuid().ToString();
					}

					public Message(string guid)
					{
						Guid = guid;
					}

					public string Guid { get; set; }

					public ReturnAddress ReturnAddress { get; set; }

					public Source Source { get; set; }

					public MessageExecution.IMessageExecutor CreateExecutor()
					{
						return MessageExecution.MessageExecutorFactory.CreateExecutor(this);
					}

					public void Send(Skyline.DataMiner.Net.Connection connection, int dataMinerId, int elementId, int parameterId)
					{
						var destination = new DmsElementId(dataMinerId, elementId);
						IDms thisDma = connection.GetDms();
						var element = thisDma.GetElement(destination);
						var parameter = element.GetStandaloneParameter<string>(parameterId);
						var serializer = Serializing.SerializerFactory.CreateSerializer(typeof(Message));
						string value = serializer.SerializeToString(this);
						parameter.SetValue(value);
					}

					public Message Send(Skyline.DataMiner.Net.Connection connection, int dataMinerId, int elementId, int parameterId, TimeSpan timeout)
					{
						if (ReturnAddress != null)
						{
							using (MessageWaiter waiter = new MessageWaiter(connection, this))
							{
								Send(connection, dataMinerId, elementId, parameterId);
								return waiter.WaitNext(timeout).First();
							}
						}

						return null;
					}
				}

				public class Messages : ICollection<Message>
				{
					private readonly Dictionary<string, Message> content = new Dictionary<string, Message>();

					public Messages(InterAppCall parentCall)
					{
						ParentCall = parentCall;
					}

					public Messages()
					{
					}

					public int Count
					{
						get { return content.Count; }
					}

					public bool IsReadOnly
					{
						get { return false; }
					}

					public InterAppCall ParentCall { get; set; }

					public void Add(Message item)
					{
						AddMessage(item);
					}

					public void AddMessage(params Message[] msgs)
					{
						Type executorType = typeof(MessageExecution.IMessageExecutor);

						for (int i = 0; i < msgs.Length; i++)
						{
							if (executorType.IsAssignableFrom(msgs[i].GetType()))
							{
								throw new ArgumentException("Message provided should not implement IMessageExecutor. Make sure you decouple data and logic.", "msgs");
							}

							content.Add(msgs[i].Guid, msgs[i]);
						}
					}

					public void Clear()
					{
						content.Clear();
					}

					public bool Contains(Message item)
					{
						return content.ContainsKey(item.Guid);
					}

					public void CopyTo(Message[] array, int arrayIndex)
					{
						var valuesArray = content.Values.ToArray();
						Array.Copy(valuesArray, 0, array, arrayIndex, valuesArray.Length);
					}

					public IEnumerator<Message> GetEnumerator()
					{
						return content.Values.GetEnumerator();
					}

					IEnumerator IEnumerable.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					public bool Remove(Message item)
					{
						return RemoveMessage(item.Guid);
					}

					public bool RemoveMessage(params string[] guids)
					{
						bool result = true;
						foreach (var guid in guids)
						{
							bool innerResult = content.Remove(guid);
							result = result && innerResult;
						}

						return result;
					}

					public bool TryGetMessage(string guid, out Message message)
					{
						return content.TryGetValue(guid, out message);
					}
				}

				public class MessageWaiter : IDisposable
				{
					private readonly Skyline.DataMiner.Net.Connection connection;
					private readonly List<SLNetWaitHandle> handles;
					private readonly HashSet<string> monitoredGuids;

					private bool disposedValue;

					public MessageWaiter(Skyline.DataMiner.Net.Connection connection, params Message[] commands)
					{
						// I need to make a single subscription for a unique dmaid/eleid/pid
						// So I first have to filter all the received commands and group them by that key.
						var commandsGrouped = commands.GroupBy(p => p.ReturnAddress.DmaId + "/" + p.ReturnAddress.ElementId + "/" + p.ReturnAddress.ParameterId);

						this.connection = connection;
						handles = new List<SLNetWaitHandle>();
						monitoredGuids = new HashSet<string>();
						foreach (var cmd in commands)
						{
							monitoredGuids.Add(cmd.Guid);
						}

						foreach (var commandGroup in commandsGrouped)
						{
							string uniquePid = commandGroup.Key;
							string[] splitUniquePid = uniquePid.Split('/');
							if (splitUniquePid.Length < 3) throw new FormatException("Return Address needs dmaId, eleId and pid: " + uniquePid);

							int dmaId = Convert.ToInt32(splitUniquePid[0]);
							int eleId = Convert.ToInt32(splitUniquePid[1]);
							int pid = Convert.ToInt32(splitUniquePid[2]);
							var thisHandle = new SLNetWaitHandle();
							thisHandle.Flag = new AutoResetEvent(false);
							thisHandle.HandleGuid = uniquePid + System.Guid.NewGuid();
							thisHandle.TriggeredQueue = new ConcurrentQueue<Message>();

							thisHandle.Handler = CreateHandler(thisHandle, dmaId, eleId, pid);
							thisHandle.Subscriptions = new SubscriptionFilter[] { new SubscriptionFilterParameter(typeof(ParameterChangeEventMessage), dmaId, eleId, pid) };
							handles.Add(thisHandle);
							connection.OnNewMessage += thisHandle.Handler;
							connection.AddSubscription(thisHandle.HandleGuid, thisHandle.Subscriptions);
						}
					}

					// This code added to correctly implement the disposable pattern.
					public void Dispose()
					{
						Dispose(true);
						GC.SuppressFinalize(this);
					}

					public IEnumerable<Message> WaitNext(TimeSpan timeout)
					{
						AutoResetEvent[] handleFlags = handles.Select(p => p.Flag).ToArray();
						while (monitoredGuids.Count > 0)
						{
							int trigger = AutoResetEvent.WaitAny(handleFlags, timeout);
							if (trigger != WaitHandle.WaitTimeout)
							{
								var handle = handles[trigger];

								Message response;
								if (handle.TriggeredQueue.TryDequeue(out response))
								{
									yield return response;
									monitoredGuids.Remove(response.Guid);
								}
							}
							else
							{
								throw new TimeoutException("Timeout while waiting on responses: " + String.Join(";", monitoredGuids));
							}
						}

						foreach (var handle in handles)
						{
							connection.RemoveSubscription(handle.HandleGuid, handle.Subscriptions);
							connection.OnNewMessage -= handle.Handler;
							handle.Handler = null;
							handle.HandleGuid = null;
							handle.Subscriptions = null;
						}
					}

					// To detect redundant calls
					protected virtual void Dispose(bool disposing)
					{
						if (!disposedValue)
						{
							if (disposing)
							{
								var allUnreceived = FindRunningSubscriptions();
								foreach (var handle in allUnreceived)
								{
									connection.RemoveSubscription(handle.HandleGuid, handle.Subscriptions);
									connection.OnNewMessage -= handle.Handler;
								}
							}

							disposedValue = true;
						}
					}

					private NewMessageEventHandler CreateHandler(SLNetWaitHandle thisHandle, int dmaId, int eleId, int pid)
					{
						return (sender, e) =>
						{
							try
							{
								if (e.FromSet(thisHandle.HandleGuid) && e.Message is ParameterChangeEventMessage)
								{
									var paramChangeMessage = e.Message as ParameterChangeEventMessage;
									var parameterData = SLNetUtility.ParseStandaloneParameterChangeEventMessageString(paramChangeMessage);

									if (parameterData.DmaId == dmaId && parameterData.ElementId == eleId && parameterData.ParameterId == pid)
									{
										Message internalReturn = MessageFactory.CreateFromRaw(parameterData.Value);
										if (monitoredGuids.Contains(internalReturn.Guid))
										{
											thisHandle.TriggeredQueue.Enqueue(internalReturn);
											thisHandle.Flag.Set();
										}
									}
								}
							}
							catch
							{
								// Robustness: Do not Handle. This is to make sure invalid received events don't crash the whole setup.
							}
						};
					}

					private List<SLNetWaitHandle> FindRunningSubscriptions()
					{
						List<SLNetWaitHandle> runningSubs = new List<SLNetWaitHandle>();
						for (int j = 0; j < handles.Count; j++)
						{
							var handle = handles[j];
							if (handle.HandleGuid != null || handle.Handler != null)
							{
								runningSubs.Add(handle);
							}
						}

						return runningSubs;
					}

					private class SLNetWaitHandle
					{
						public AutoResetEvent Flag { get; set; }

						public string HandleGuid { get; set; }

						public NewMessageEventHandler Handler { get; set; }

						public ConcurrentQueue<Message> TriggeredQueue { get; set; }

						public SubscriptionFilter[] Subscriptions { get; set; }
					}
				}

				public class ReturnAddress
				{
					public ReturnAddress(int dmaId, int elementId, int pid)
					{
						ParameterId = pid;
						DmaId = dmaId;
						ElementId = elementId;
					}

					public ReturnAddress()
					{
					}

					public int DmaId { get; set; }

					public int ElementId { get; set; }

					public int ParameterId { get; set; }
				}

				public class Source
				{
					public Source(string name)
					{
						Name = name;
					}

					public Source(string name, int dmaId, int elementId)
					{
						Name = name;
						DmaId = dmaId;
						ElementId = elementId;
					}

					public Source()
					{
					}

					public int DmaId { get; set; }

					public int ElementId { get; set; }

					public string Name { get; set; }
				}
			}

			namespace MessageExecution
			{
				using BaseCall;

				public interface IMessageExecutor
				{
					Message CreateReturnMessage();

					void DataGets(object dataSource);

					void DataSets(object dataDestination);

					void Modify();

					void Parse();

					bool Validate();
				}

				public static class MessageExecutorFactory
				{
					public static IMessageExecutor CreateExecutor(Message message)
					{
						Type concreteType = message.GetType();

						Type concreteExecutor = null;

						// Find the Concrete Executor for this.
						foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
						{
							if (type.IsInterface || type.IsAbstract || !type.BaseType.IsAbstract) continue;
							Type baseType = type.BaseType;
							Type expectedBase = typeof(MessageExecutor<>);

							if (baseType.Name == expectedBase.Name)
							{
								var genericType = baseType.GetGenericArguments()[0];

								if (genericType == concreteType)
								{
									concreteExecutor = type;
									break;
								}
							}
						}

						if (concreteExecutor != null)
						{
							return (IMessageExecutor)Activator.CreateInstance(concreteExecutor, message);
						}
						else
						{
							throw new AmbiguousMatchException("Unable to find executor for message with type:" + concreteType + " Verify you have a class implementing :MessageExecutor<" + concreteType + ">.");
						}
					}
				}

				public abstract class MessageExecutor<T> : IMessageExecutor
				{
					protected MessageExecutor(T message)
					{
						Message = message;
					}

					public T Message { get; private set; }

					/// <summary>
					/// Allows the creation of a new Message to answer the parsing of this one. Should run last.
					/// </summary>
					/// <returns>Returns an object implementing the Message class.</returns>
					public abstract Message CreateReturnMessage();

					/// <summary>
					/// Retrieving additional data needed for parsing. Should run 1st.
					/// </summary>
					/// <param name="dataSource">A source to retrieve data from. Can be SLProtocol, Automation.Engine, or custom classes.</param>
					public abstract void DataGets(object dataSource);

					/// <summary>
					/// Performs the sets for the desired output. Should run 5th.
					/// </summary>
					/// <param name="dataDestination">A destination to set data into. Can be SLProtocol, Automation.Engine, or custom classes.</param>
					public abstract void DataSets(object dataDestination);

					/// <summary>
					/// Modifies Input data into desired output. Should run 4th.
					/// </summary>
					public abstract void Modify();

					/// <summary>
					/// Additional parsing of input data. Should run 3rd.
					/// </summary>
					public abstract void Parse();

					/// <summary>
					/// Validating data before starting to parse. Should run 2nd.
					/// </summary>
					/// <returns>A boolean indicating if Validation succeeded or not.</returns>
					public abstract bool Validate();
				}
			}
		}

		namespace SLNetHelper
		{
			using System;

			using Skyline.DataMiner.Net.Messages;

			internal class SLNetUtility
			{
				public static ParameterData ParseStandaloneParameterChangeEventMessageString(ParameterChangeEventMessage paramChangeMessage)
				{
					string sEventDmaId = Convert.ToString(paramChangeMessage.DataMinerID);
					string sEventElId = Convert.ToString(paramChangeMessage.ElementID);
					string sEventPid = Convert.ToString(paramChangeMessage.ParameterID);

					int dmaId;
					int elementId;
					int parameterId;

					Int32.TryParse(sEventDmaId, out dmaId);
					Int32.TryParse(sEventElId, out elementId);
					Int32.TryParse(sEventPid, out parameterId);

					string value = paramChangeMessage.NewValue.StringValue;

					return new ParameterData(dmaId, elementId, parameterId, value);
				}

				public class ParameterData
				{
					public ParameterData(int dmaId, int elementId, int parameterId, string value)
					{
						DmaId = dmaId;
						ElementId = elementId;
						ParameterId = parameterId;
						Value = value;
					}

					public int DmaId { get; private set; }

					public int ElementId { get; private set; }

					public int ParameterId { get; private set; }

					public string Value { get; private set; }
				}
			}
		}
	}

	namespace Serializing
	{
		using System;
		using System.Collections.Generic;
		using System.IO;
		using System.Linq;
		using System.Reflection;
		using System.Runtime.Serialization;
		using System.Text;
		using System.Xml;

		using Newtonsoft.Json;
		using Newtonsoft.Json.Serialization;

		namespace UsingDataContracts
		{
			internal class Serializer : ISerializer
			{
				public Serializer(Type baseType, List<Type> overrides = null)
				{
					if (overrides != null)
					{
						InnerSerialize = new DataContractSerializer(baseType, overrides);
					}
					else
					{
						InnerSerialize = new DataContractSerializer(baseType);
					}
				}

				public DataContractSerializer InnerSerialize { get; private set; }

				public T DeserializeFromString<T>(string input)
				{
					T result;

					using (XmlReader reader = XmlReader.Create(new StringReader(input)))
					{
						result = (T)InnerSerialize.ReadObject(reader);
					}

					return result;
				}

				public string SerializeToString(object input)
				{
					var sb = new StringBuilder();

					using (XmlWriter writer = XmlWriter.Create(sb))
					{
						InnerSerialize.WriteObject(writer, input);
					}

					return sb.ToString();
				}
			}
		}

		namespace UsingJsonNewtonSoft
		{
			internal class ContractResolverWithPrivates : CamelCasePropertyNamesContractResolver
			{
				protected override Newtonsoft.Json.Serialization.JsonProperty CreateProperty(System.Reflection.MemberInfo member, MemberSerialization memberSerialization)
				{
					var prop = base.CreateProperty(member, memberSerialization);

					if (!prop.Writable)
					{
						var property = member as System.Reflection.PropertyInfo;
						if (property != null)
						{
							var hasPrivateSetter = property.GetSetMethod(true) != null;
							prop.Writable = hasPrivateSetter;
						}
					}

					return prop;
				}
			}

			internal class KnownTypesBinder : ISerializationBinder
			{
				public IList<Type> KnownTypes { get; set; }

				public void BindToName(Type serializedType, out string assemblyName, out string typeName)
				{
					assemblyName = String.Empty;

					if (KnownTypes != null && KnownTypes.Contains(serializedType))
					{
						typeName = serializedType.Name;
					}
					else
					{
						typeName = serializedType.FullName;
					}
				}

				public Type BindToType(string assemblyName, string typeName)
				{
					Type foundType = null;

					if (KnownTypes != null)
					{
						foundType = KnownTypes.SingleOrDefault(t => t.Name == typeName);
					}

					if (foundType == null)
					{
						foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
						{
							if (typeName == t.FullName)
							{
								foundType = t;
							}
						}
					}

					if (foundType == null)
					{
						DefaultSerializationBinder def = new DefaultSerializationBinder();
						foundType = def.BindToType(assemblyName, typeName);
					}

					return foundType;
				}
			}

			internal class Serializer : ISerializer
			{
				public Serializer()
				{
					KnownTypes = new KnownTypesBinder();

					Settings = new JsonSerializerSettings
					{
						SerializationBinder = KnownTypes,
						TypeNameHandling = TypeNameHandling.Auto,
						TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
						MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
						ObjectCreationHandling = ObjectCreationHandling.Replace,
						MissingMemberHandling = MissingMemberHandling.Ignore,
						ContractResolver = new ContractResolverWithPrivates(),
						PreserveReferencesHandling = PreserveReferencesHandling.Objects
					};
				}

				public Serializer(List<Type> knownTypes) : this()
				{
					KnownTypes.KnownTypes = knownTypes;
				}

				public Serializer(Type rootType, List<Type> knownTypes = null) : this()
				{
					RootType = rootType;
					if (knownTypes != null)
					{
						KnownTypes.KnownTypes = knownTypes;
					}
				}

				public KnownTypesBinder KnownTypes { get; private set; }

				public Type RootType { get; private set; }

				public JsonSerializerSettings Settings { get; private set; }

				public T DeserializeFromString<T>(string input)
				{
					return JsonConvert.DeserializeObject<T>(input, Settings);
				}

				public string SerializeToString(object input)
				{
					if (RootType != null)
					{
						return JsonConvert.SerializeObject(input, RootType, Settings);
					}
					else
					{
						return JsonConvert.SerializeObject(input, Settings);
					}
				}
			}
		}

		public enum XmlSerializerType
		{
			JsonNewtonSoft,
			DataContracts
		}

		public interface ISerializer
		{
			T DeserializeFromString<T>(string input);

			string SerializeToString(object input);
		}

		public static class SerializerFactory
		{
			/// <summary>
			/// Will create an object of type ISerializer allowing you to serialize and deserialize the same data.
			/// This method does not specify what serializer it will actually use in the background.
			/// It only provides you with the guarantee that it uses the same from both serialization and deserialization.
			/// </summary>
			/// <param name="baseType">The root type of the object you're serializing.</param>
			/// <param name="namespaces">Namespaces that contain custom types that could be part of the serialization.</param>
			/// <returns>An object implementing the ISerializer interface.</returns>
			public static ISerializer CreateSerializer(Type baseType, string[] namespaces = null)
			{
				SerializerBuilder builder = new SerializerBuilder().WithSerializer(XmlSerializerType.JsonNewtonSoft).WithBaseType(baseType);

				if (namespaces != null)
				{
					builder.WithTypesInNamespace(namespaces);
				}

				return builder.Build();
			}
		}

		/// <summary>
		/// Allows the creation of an ISerializer implementation where you specify some generic settings.
		/// </summary>
		public class SerializerBuilder
		{
			private readonly List<Type> overrides = new List<Type>();
			private Type baseType;
			private XmlSerializerType xmlSerializerType = XmlSerializerType.JsonNewtonSoft;

			public ISerializer Build()
			{
				if (overrides.Count > 0)
				{
					return BuildWithOverrides();
				}
				else
				{
					return BuildWithoutOverrides();
				}
			}

			public SerializerBuilder WithBaseType(Type t)
			{
				baseType = t;
				return this;
			}

			public SerializerBuilder WithPossibleTypes(params Type[] possibleTypes)
			{
				overrides.AddRange(possibleTypes);
				return this;
			}

			public SerializerBuilder WithSerializer(XmlSerializerType type)
			{
				xmlSerializerType = type;
				return this;
			}

			public SerializerBuilder WithTypesInNamespace(params string[] namespaces)
			{
				var possibleTypes = GetTypesInNamespace(Assembly.GetExecutingAssembly(), namespaces);
				overrides.AddRange(possibleTypes);
				return this;
			}

			private static List<Type> GetTypesInNamespace(Assembly assembly, params string[] nameSpaces)
			{
				List<Type> allTypes = new List<Type>();
				Type[] allInAssembly = assembly.GetTypes();
				foreach (Type t in allInAssembly)
				{
					foreach (string ns in nameSpaces)
					{
						if (String.Equals(t.Namespace, ns, StringComparison.Ordinal)) allTypes.Add(t);
					}
				}

				return allTypes;
			}

			private ISerializer BuildWithoutOverrides()
			{
				if (xmlSerializerType == XmlSerializerType.JsonNewtonSoft)
				{
					if (baseType != null)
					{
						return new UsingJsonNewtonSoft.Serializer(baseType);
					}
					else
					{
						return new UsingJsonNewtonSoft.Serializer();
					}
				}
				else
				{
					if (baseType != null)
					{
						return new UsingDataContracts.Serializer(baseType);
					}
					else
					{
						throw new InvalidOperationException("DataContractSerialized requires a defined baseType.");
					}
				}
			}

			private ISerializer BuildWithOverrides()
			{
				if (xmlSerializerType == XmlSerializerType.JsonNewtonSoft)
				{
					if (baseType != null)
					{
						return new UsingJsonNewtonSoft.Serializer(baseType, overrides);
					}
					else
					{
						return new UsingJsonNewtonSoft.Serializer(overrides);
					}
				}
				else
				{
					if (baseType != null)
					{
						return new UsingDataContracts.Serializer(baseType, overrides);
					}
					else
					{
						// Not Possible
						throw new InvalidOperationException("DataContractSerialized requires a defined baseType.");
					}
				}
			}
		}
	}
}

namespace Skyline.Protocol
{
	namespace API.Executors.SlcSdfInterApp
	{
		using DeviceCommunication;

		using Skyline.Common.Api.Calls.SlcSdfInterApp;
		using Skyline.Common.InterApp.InterAppCalls.BaseCall;
		using Skyline.Common.InterApp.InterAppCalls.MessageExecution;
		using Skyline.DataMiner.Scripting;
		using Skyline.Protocol.Common.Buffering;
		using Skyline.Protocol.Common.InterAppSLProtocolHelper;

		public class CreateLineupExecutor : MessageExecutor<CreateLineup.Create>
		{
			private HttpData data;
			public CreateLineupExecutor(CreateLineup.Create message) : base(message)
			{
			}

			public override Message CreateReturnMessage()
			{
				return null;
			}

			public override void DataGets(object dataSource)
			{
				// No additional gets needed.
			}

			public override void DataSets(object dataDestination)
			{
				SLProtocol protocol = Helper.GenericToSlprotocol(dataDestination);
				protocol.Log("QA" + protocol.QActionID + "|GUID in buffer:" + Message.Guid, LogType.Error, LogLevel.NoLogging);
				InternalBuffer buffer = new InternalBuffer(protocol, true);
				buffer.AddToInternalBufferAndRun(104, 10, data);
			}

			public override void Modify()
			{
				// Need to create the command to match the Device Communication requirements.
				data = new HttpData();
				data.Data = Message.LineupName + ";" + Message.PoolId;
				data.OriginalMessage = Message;
			}

			public override void Parse()
			{
			}

			public override bool Validate()
			{
				return true;
			}
		}
	}

	namespace Common
	{
		namespace Buffering
		{
			using System;
			using System.Collections.Generic;

			using Skyline.Common.Serializing;
			using Skyline.DataMiner.Scripting;

			using ThreadLocking;

			public interface IBufferItem
			{
				void Modify();

				void Parse();

				void ProtocolGets(SLProtocol protocol);

				void ProtocolSets(SLProtocol protocol);

				bool Validate();
			}

			public class InternalBuffer
			{
				private readonly string currentElement;
				private readonly bool debug;
				private readonly SLProtocol protocol;
				private readonly string separator = "/";
				private readonly ISerializer serializer;

				public InternalBuffer(SLProtocol protocol, bool debug = false)
				{
					this.protocol = protocol;
					currentElement = protocol.DataMinerID + "/" + protocol.ElementID;
					serializer = SerializerFactory.CreateSerializer(typeof(Queue<IBufferItem>));
					this.debug = debug;
				}

				public bool AddToInternalBufferAndRun(int pid, int runTriggerID, IBufferItem item)
				{
					bool startBuffer = false;
					protocol.WriteLock(
						debug,
						currentElement + separator + pid,
						() =>
						{
							Queue<IBufferItem> buffer;
							string bufferStr = Convert.ToString(protocol.GetParameter(pid));
							startBuffer = bufferStr == String.Empty;
							if (startBuffer)
							{
								buffer = new Queue<IBufferItem>();
							}
							else
							{
								buffer = serializer.DeserializeFromString<Queue<IBufferItem>>(bufferStr);
							}

							buffer.Enqueue(item);
							protocol.SetParameter(pid, serializer.SerializeToString(buffer));
							if (debug) protocol.Log("QA" + protocol.QActionID + "|AddToInternalBufferAndRun|Enqueued and set buffer with count: " + buffer.Count, LogType.DebugInfo, LogLevel.NoLogging);
							if (startBuffer)
							{
								protocol.CheckTrigger(runTriggerID);
								if (debug) protocol.Log("QA" + protocol.QActionID + "|AddToInternalBufferAndRun|Triggered Start of Buffer", LogType.DebugInfo, LogLevel.NoLogging);
							}
						});
					return startBuffer;
				}

				public IBufferItem GetFromInternalBuffer(int pid)
				{
					IBufferItem item = null;
					protocol.WriteLock(
						debug,
						currentElement + separator + pid,
						() =>
						{
							Queue<IBufferItem> buffer;
							string bufferStr = Convert.ToString(protocol.GetParameter(pid));
							if (bufferStr == String.Empty)
							{
								buffer = new Queue<IBufferItem>();
							}
							else
							{
								buffer = serializer.DeserializeFromString<Queue<IBufferItem>>(bufferStr);
							}

							if (buffer.Count > 0)
							{
								item = buffer.Dequeue();
								protocol.SetParameter(pid, serializer.SerializeToString(buffer));
							}
							else
							{
								item = null;
								protocol.SetParameter(pid, String.Empty);
							}
						});

					return item;
				}
			}
		}

		namespace InterAppSLProtocolHelper
		{
			using System;

			using Skyline.DataMiner.Scripting;

			public static class Helper
			{
				public static SLProtocol GenericToSlprotocol(object generic)
				{
					SLProtocol protocol = generic as SLProtocol;
					if (protocol == null) throw new ArgumentException("Expected argument of type SLProtocol", "generic");
					return protocol;
				}
			}
		}

		namespace Logging
		{
			using System;
			using System.Collections.Concurrent;

			using Skyline.DataMiner.Scripting;

			public static class InterAppLogging
			{
				private static readonly object LockObject = new object();
				private static ConcurrentDictionary<string, bool> logEnabled;

				public static void InterAppDebugLog(this SLProtocol protocol, string method, string message, bool debug = true)
				{
					try
					{
						if (logEnabled == null)
							Update(protocol);

						var key = String.Format("{0}/{1}", protocol.DataMinerID, protocol.ElementID);
						var enabled = false;
						if (!debug || (logEnabled.TryGetValue(key, out enabled) && enabled))
							protocol.Log(String.Format("[QACTION {0}]{1}: {2}", protocol.QActionID, method, message), LogType.DebugInfo, LogLevel.NoLogging);
					}
					catch (Exception e)
					{
						protocol.Log(string.Format("QA{0}: (LogExtensions) Logging failed: {1}", protocol.QActionID, e), LogType.Error, LogLevel.NoLogging);
					}
				}

				public static void Update(SLProtocol protocol)
				{
					int debugPid = 9000001;

					lock (LockObject)
					{
						if (logEnabled == null)
							logEnabled = new ConcurrentDictionary<string, bool>();
					}

					var key = String.Format("{0}/{1}", protocol.DataMinerID, protocol.ElementID);
					logEnabled.AddOrUpdate(
						key,
						Convert.ToBoolean(protocol.GetParameter(debugPid)),
						(k, v) =>
						{
							return Convert.ToBoolean(protocol.GetParameter(debugPid));
						});

					InterAppDebugLog(protocol, "UpdateDebugLogging", "Logging enabled for pid:" + debugPid);
				}
			}
		}

		namespace ThreadLocking
		{
			using System;
			using System.Collections.Concurrent;
			using System.Threading;

			using Skyline.DataMiner.Scripting;

			public static class DynamicThreadLocker
			{
				private static ConcurrentDictionary<string, ReaderWriterLockSlim> dic = new ConcurrentDictionary<string, ReaderWriterLockSlim>();

				public static void ReadLock(this SLProtocol protocol, bool debug, string currentLock, Action a)
				{
					protocol.TakeReadLock(debug, currentLock);
					try
					{
						a.Invoke();
					}
					catch (Exception e)
					{
						protocol.Log(string.Format("QA{0}: (Exception) Value at {1} with Exception:{2}", protocol.QActionID, "ReadLock:" + currentLock, e), LogType.Error, LogLevel.NoLogging);
					}
					finally
					{
						protocol.ReleaseReadLock(debug, currentLock);
					}
				}

				public static void ReleaseReadLock(this SLProtocol protocol, bool debug, string currentLock)
				{
					var currentLocko = dic.GetOrAdd(currentLock, new ReaderWriterLockSlim());
					currentLocko.ExitReadLock();
					if (debug) protocol.Log("QA" + protocol.QActionID + "| DBG LOCK ----- Exit Read Lock " + currentLock, LogType.DebugInfo, LogLevel.NoLogging);
				}

				public static void ReleaseWriteLock(this SLProtocol protocol, bool debug, string currentLock)
				{
					var currentLocko = dic.GetOrAdd(currentLock, new ReaderWriterLockSlim());
					currentLocko.ExitWriteLock();
					if (debug) protocol.Log("QA" + protocol.QActionID + "| DBG LOCK ----- Exit Write Lock " + currentLock, LogType.DebugInfo, LogLevel.NoLogging);
				}

				public static bool RemoveLock(this SLProtocol protocol, bool debug, string currentLock)
				{
					var currentLocko = dic.GetOrAdd(currentLock, new ReaderWriterLockSlim());
					currentLocko.EnterWriteLock();
					ReaderWriterLockSlim rws;
					bool result = dic.TryRemove(currentLock, out rws);
					currentLocko.ExitWriteLock();
					return result;
				}

				public static void TakeReadLock(this SLProtocol protocol, bool debug, string currentLock)
				{
					if (debug) protocol.Log("QA" + protocol.QActionID + "| DBG LOCK ----- Enter Read Lock " + currentLock, LogType.DebugInfo, LogLevel.NoLogging);
					var currentLocko = dic.GetOrAdd(currentLock, new ReaderWriterLockSlim());
					currentLocko.EnterReadLock();

					if (debug) protocol.Log("QA" + protocol.QActionID + "| DBG LOCK ----- Success", LogType.DebugInfo, LogLevel.NoLogging);
				}

				public static void TakeWriteLock(this SLProtocol protocol, bool debug, string currentLock)
				{
					if (debug) protocol.Log("QA" + protocol.QActionID + "| DBG LOCK ----- Enter Write Lock " + currentLock, LogType.DebugInfo, LogLevel.NoLogging);

					var currentLocko = dic.GetOrAdd(currentLock, new ReaderWriterLockSlim());
					currentLocko.EnterWriteLock();
					if (debug) protocol.Log("QA" + protocol.QActionID + "| DBG LOCK ----- Success", LogType.DebugInfo, LogLevel.NoLogging);
				}

				public static bool TryTakeWriteLock(this SLProtocol protocol, bool debug, string currentLock)
				{
					if (debug) protocol.Log("QA" + protocol.QActionID + "| DBG LOCK ----- Trying to Enter Write Lock " + currentLock, LogType.DebugInfo, LogLevel.NoLogging);
					var currentLocko = dic.GetOrAdd(currentLock, new ReaderWriterLockSlim());
					bool result = currentLocko.TryEnterWriteLock(0);
					if (result)
					{
						if (debug) protocol.Log("QA" + protocol.QActionID + "| DBG LOCK ----- Success " + currentLock, LogType.DebugInfo, LogLevel.NoLogging);
					}
					else
					{
						if (debug) protocol.Log("QA" + protocol.QActionID + "| DBG LOCK ----- Fail, lock already taken " + currentLock, LogType.DebugInfo, LogLevel.NoLogging);
					}

					return result;
				}

				public static void WriteLock(this SLProtocol protocol, bool debug, string currentLock, Action a)
				{
					protocol.TakeWriteLock(debug, currentLock);
					try
					{
						a.Invoke();
					}
					catch (Exception e)
					{
						protocol.Log(string.Format("QA{0}: (Exception) Value at {1} with Exception:{2}", protocol.QActionID, "ReadLock:" + currentLock, e), LogType.Error, LogLevel.NoLogging);
					}
					finally
					{
						protocol.ReleaseWriteLock(debug, currentLock);
					}
				}
			}
		}
	}

	namespace DeviceCommunication
	{
		using Skyline.Common.InterApp.InterAppCalls.BaseCall;
		using Skyline.DataMiner.Scripting;
		using Skyline.Protocol.Common.Buffering;

		public class HttpData : IBufferItem
		{
			public string Data { get; set; }

			public Message OriginalMessage { get; set; }

			public void Modify()
			{
				// No Modification needed.
			}

			public void Parse()
			{
				// No Parsing needed.
			}

			public void ProtocolGets(SLProtocol protocol)
			{
				// No Gets needed.
			}

			public void ProtocolSets(SLProtocol protocol)
			{
				// Set the current message if applicable.
				if (OriginalMessage != null)
				{
					OriginalMessage.Send(protocol.SLNet.RawConnection, protocol.DataMinerID, protocol.ElementID, 103);
				}

				// Simulate the HTTP Command
				protocol.Log("QA" + protocol.QActionID + "|Buffer|Simulate HTTP -" + OriginalMessage.Guid, LogType.Error, LogLevel.NoLogging);
				protocol.SetParameter(101, Data);
				protocol.CheckTrigger(1);
			}

			public bool Validate()
			{
				if (string.IsNullOrWhiteSpace(Data)) return false;
				return true;
			}
		}
	}
}