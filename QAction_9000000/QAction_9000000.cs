using System;

using Skyline.Common.Api.Calls.SlcSdfInterApp;
using Skyline.DataMiner.Core.InterAppCalls.Common.CallBulk;
using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;
using Skyline.DataMiner.Core.InterAppCalls.Common.Serializing;
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
			protocol.InterAppDebugLog("Run", "Raw:" + raw);
			IInterAppCall receivedCall = InterAppCallFactory.CreateFromRaw(raw, Shared.KnownTypes);
			protocol.InterAppDebugLog("Run", "Deserialized");
			if (receivedCall == null)
			{
				protocol.Log("QA" + protocol.QActionID + "|Run|ERR: Value in Parameter was empty.", LogType.Error, LogLevel.NoLogging);
				return;
			}

			protocol.InterAppDebugLog("Run", "Extracted Call with Guid:" + receivedCall.Guid + " Sent Date:" + receivedCall.SendingTime);
			protocol.InterAppDebugLog("Run", "Found " + receivedCall.Messages.Count + " Messages.");

			foreach (var receivedMessage in receivedCall.Messages)
			{
				Message response;
				receivedMessage.TryExecute(protocol, protocol, Shared.Mapping, out response);
				if (response != null)
				{
					receivedMessage.Reply(protocol.SLNet.RawConnection, response, Shared.KnownTypes);
				}
			}
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}
}