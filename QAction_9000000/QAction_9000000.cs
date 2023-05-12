using System;
using System.Collections.Generic;

using Skyline.Common.InterApp.InterAppCalls.BaseCall;
using Skyline.DataMiner.Scripting;
using Skyline.Protocol.Common.Logging;

/// <summary>
/// DataMiner QAction Class: ProcessInterAppReceived.
/// </summary>
public class QAction
{
	/// <summary>
	/// The QAction entry point.
	/// </summary>
	/// <param name="protocol">Link with SLProtocol process.</param>
	public static void Run(SLProtocolExt protocol)
	{
		try
		{
			protocol.InterAppDebugLog("Run", "Received a Call");
			string raw = Convert.ToString(protocol.GetParameter(protocol.GetTriggerParameter()));
			var receivedCall = InterAppCallFactory.CreateFromRaw(raw);
			protocol.InterAppDebugLog("Run", "Raw:" + raw);
			if (receivedCall == null)
			{
				protocol.Log("QA" + protocol.QActionID + "|Run|ERR: Value in Parameter was empty.", LogType.Error, LogLevel.NoLogging);
				return;
			}

			protocol.InterAppDebugLog("Run", "Extracted Call with Guid:" + receivedCall.Guid + " Sent Date:" + receivedCall.SendingTime);

			List<Message> returnMessages = new List<Message>();
			protocol.InterAppDebugLog("Run", "Found " + receivedCall.Messages.Count + " Messages.");
			foreach (var message in receivedCall.Messages)
			{
				ParseMessage(protocol, returnMessages, message);
			}

			if (receivedCall.ReturnAddress != null && returnMessages.Count > 0)
			{
				protocol.InterAppDebugLog("Run", "Creating Return Call...");
				InterAppCall returnCall = new InterAppCall(receivedCall.Guid);
				returnCall.ReturnAddress = null;
				returnCall.Messages.AddMessage(returnMessages.ToArray());
				returnCall.Source = new Source(protocol.ElementName, protocol.DataMinerID, protocol.ElementID);
				protocol.InterAppDebugLog("Run", "Sending Return Call...");
				try
				{
					returnCall.Send(protocol.SLNet.RawConnection, receivedCall.ReturnAddress.DmaId, receivedCall.ReturnAddress.ElementId, receivedCall.ReturnAddress.ParameterId);
				}
				catch (Exception e)
				{
					protocol.Log("QA" + protocol.QActionID + "|ERR|Exception Sending Return Call: " + e, LogType.Error, LogLevel.NoLogging);
				}
			}
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}

	private static void ParseMessage(SLProtocolExt protocol, List<Message> returnMessages, Message message)
	{
		protocol.InterAppDebugLog("Run", "Execute Message (" + message.GetType() + "): " + message.Guid);
		try
		{
			var executor = message.CreateExecutor();

			protocol.InterAppDebugLog("Run", "Message Data Gets ...");
			executor.DataGets(protocol);
			protocol.InterAppDebugLog("Run", "Message Validation ...");
			if (!executor.Validate())
			{
				protocol.Log("QA" + protocol.QActionID + "|ParseMessage|Validation Failed on Message: "+message.Guid, LogType.Error, LogLevel.NoLogging);
				return;
			}

			protocol.InterAppDebugLog("Run", "Message Parsing...");
			executor.Parse();
			protocol.InterAppDebugLog("Run", "Message Modification...");
			executor.Modify();
			protocol.InterAppDebugLog("Run", "Message Data Sets ...");
			executor.DataSets(protocol);
			protocol.InterAppDebugLog("Run", "Creating Possible Return Message ...");
			Message returnMessage = executor.CreateReturnMessage();
			if (returnMessage != null) returnMessages.Add(returnMessage);
		}
		catch (Exception e)
		{
			protocol.Log("QA" + protocol.QActionID + "|ERR|Exception with message:" + message.Guid + "with error: " + e, LogType.Error, LogLevel.NoLogging);
		}
	}
}