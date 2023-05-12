using System;

using Skyline.DataMiner.Scripting;
using Skyline.Protocol.Common.Buffering;

/// <summary>
/// DataMiner QAction Class: RunNextInternalBuffer.
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
			InternalBuffer buffer = new InternalBuffer(protocol, true);
			var httpRequest = buffer.GetFromInternalBuffer(104);

			if (httpRequest != null)
			{
				if (httpRequest.Validate())
				{
					httpRequest.ProtocolSets(protocol);
				}
			}
			else
			{
				protocol.Log("QA" + protocol.QActionID + "|Run|Buffer Finished.", LogType.Error, LogLevel.NoLogging);
			}
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}
}