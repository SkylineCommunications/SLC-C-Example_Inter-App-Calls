using System;

using Skyline.Common.Api.Calls.SlcSdfInterApp;
using Skyline.DataMiner.Library.Common.InterAppCalls.CallBulk;
using Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle;
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
			foreach (var message in receivedCall.Messages)
			{
				Message returnMessage;
				message.TryExecute(protocol, protocol, Shared.Mapping, out returnMessage);
				if (returnMessage != null)
				{
					returnMessage.Send(
						protocol.SLNet.RawConnection,
						message.ReturnAddress.AgentId,
						message.ReturnAddress.ElementId,
						message.ReturnAddress.ParameterId, Shared.KnownTypes);
				}
			}
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}
}