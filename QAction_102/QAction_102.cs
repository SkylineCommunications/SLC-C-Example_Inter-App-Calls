using System;
using System.Threading;
using Skyline.Common.Api.Calls.SlcSdfInterApp;
using Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle;
using Skyline.DataMiner.Library.Common.InterAppCalls.Shared;
using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class: DeviceResponseCreateLineup.
/// </summary>
public class QAction
{
	/// <summary>
	/// The QAction entry point.
	/// </summary>
	/// <param name="protocol">Link with SLProtocol process.</param>
	public static void Run(SLProtocol protocol)
	{
		try
		{
			// Simulate the delay in device communication.
			protocol.Log("QA" + protocol.QActionID + "|Run|Communicating to Device...", LogType.Error, LogLevel.NoLogging);
			int secondsToSleep = Convert.ToInt32(protocol.GetParameter(Parameter.devicedelay_10));
			if (secondsToSleep != 0) Thread.Sleep(secondsToSleep * 1000);
			protocol.Log("QA" + protocol.QActionID + "|Run|Device Communication Finished", LogType.Error, LogLevel.NoLogging);

			// Parsing Response would happen here. But we only have one in the example. So we'll just parse this one.
			// Returned Response should be a new ID for Lineup, we simulate this with the random.
			// This would then be set into the tables.
			Random r = new Random();
			int newLineUpId = r.Next(0, 100);

			// Now we check if we had an external call triggering this device communication.
			string currentActiveMessage = Convert.ToString(protocol.GetParameter(Parameter.currentactivemessage_103));
			if (!String.IsNullOrWhiteSpace(currentActiveMessage))
			{
				Message currentMessage = MessageFactory.CreateFromRaw(currentActiveMessage);

				if (currentMessage == null)
				{
					protocol.Log("QA" + protocol.QActionID + "|Run|Message was null in CurrentActiveCall.", LogType.Error, LogLevel.NoLogging);
					return;
				}

				// Did the external call want a returned message?
				var returnDestination = currentMessage.ReturnAddress;
				if (returnDestination != null)
				{
					protocol.Log("QA" + protocol.QActionID + "|Run|Return Message Needed", LogType.Error, LogLevel.NoLogging);
					var returnMessage = new CreateLineup.ExpectedReturn();
					returnMessage.Guid = currentMessage.Guid;
					returnMessage.LineUpId = newLineUpId;
					returnMessage.Source = new Source("SLC SDF Inter App", protocol.DataMinerID, protocol.ElementID);
					protocol.Log("QA" + protocol.QActionID + "|Return|returnDestination.ElementId" + returnDestination.ElementId + "returnDestination.AgentId " + returnDestination.AgentId + ", currentMessage.ReturnAddress.ParameterId: " + currentMessage.ReturnAddress.ParameterId, LogType.Error, LogLevel.NoLogging);
					protocol.Log("QA" + protocol.QActionID + "|Run|Sending Return Message :" + returnMessage.Guid, LogType.Error, LogLevel.NoLogging);
					returnMessage.Send(protocol.SLNet.RawConnection, returnDestination.AgentId, returnDestination.ElementId, currentMessage.ReturnAddress.ParameterId);
				}
			}
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}
}