using System;

using QAction_990;

using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class: Example Table - ContextMenu.
/// </summary>
public static class QAction
{
	/// <summary>
	/// The QAction entry point.
	/// </summary>
	/// <param name="protocol">Link with SLProtocol process.</param>
	/// <param name="contextMenuData"><see cref="object"/> containing the table ContextMenu data.</param>
	public static void Run(SLProtocol protocol, object contextMenuData)
	{
		try
		{
			ContextMenuTableManagerExampleTable contextMenu = new ContextMenuTableManagerExampleTable(
				protocol,
				contextMenuData,
				Parameter.Mytable.tablePid);
			contextMenu.ProcessContextMenuAction();
		}
		catch (Exception ex)
		{
			protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
		}
	}
}