namespace Skyline.Common
{
	namespace Api.Calls.SlcSdfInterApp
	{
		using System.Collections.Generic;

		using Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle;

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

		public class DeleteLineupResult : Message
		{
			public bool Success { get; set; }
		}

		public class DuplicateLineup : Message
		{
			public string FromLineupId { get; set; }

			public string ToLineupName { get; set; }
		}
	}
}

namespace Skyline.Protocol
{
	namespace API.Executors.SlcSdfInterApp
	{
		using DeviceCommunication;

		using Skyline.Common.Api.Calls.SlcSdfInterApp;
		using Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle;
		using Skyline.DataMiner.Library.Common.InterAppCalls.MessageExecution;
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

			using Skyline.DataMiner.Library.Common.Serializing;
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
					serializer = SerializerFactory.CreateInterAppSerializer(typeof(Queue<IBufferItem>));
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

			public static class DebugLogging
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
					int debugPid = 300;

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
		using Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle;
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