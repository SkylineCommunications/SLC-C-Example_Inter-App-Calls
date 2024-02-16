// Ignore Spelling: Pid

namespace QAction_990
{
	using System;

	using Skyline.DataMiner.ConnectorAPI.SkylineCommunications.ExampleInterAppCalls.Messages.MyTable;
	using Skyline.DataMiner.Scripting;
	using Skyline.DataMiner.Utils.Table.ContextMenu;
	using Skyline.Protocol.InterApp;

	public enum ExampleTableAction
	{
		SimpleAdd = 1,
		AdvancedAdd = 2,
		AdvancedAddWrong = 3,
		DelayedAdd = 4,
	}

	internal class ContextMenuTableManagerExampleTable : ContextMenu<ExampleTableAction>
	{
		public ContextMenuTableManagerExampleTable(SLProtocol protocol, object contextMenuData, int tablePid)
			: base(protocol, contextMenuData, tablePid)
		{
		}

		public override void ProcessContextMenuAction()
		{
			switch (this.Action)
			{
				case ExampleTableAction.SimpleAdd:
					SimpleCreate();
					break;

				case ExampleTableAction.AdvancedAdd:
					AdvancedCreate();
					break;

				case ExampleTableAction.AdvancedAddWrong:
					AdvancedCreateWrong();
					break;

				case ExampleTableAction.DelayedAdd:
					DelayedCreate();
					break;

				default:
					Protocol.Log($"QA{Protocol.QActionID}|ContextMenuTableManagerExampleTable|ProcessContextMenuAction|Unknown action.", LogType.Error, LogLevel.NoLogging);
					return;
			}
		}

		protected void SimpleCreate()
		{
			// Prepare
			var value1 = Convert.ToDouble(Data[0]);
			var value2 = Convert.ToString(Data[1]);
			var value3 = (DiscreetColumnOption)Convert.ToInt32(Data[2]);

			// Create the InterApp Message
			var message = new SimpleCreateExampleRow
			{
				ExampleData = new MyTableData
				{
					MyNumericColumn = value1,
					MyStringColumn = value2,
					MyDiscreetColumn = value3,
				},
			};

			// Since the InterApp message is for the current element can't use the InterAppFactory to build our message.
			// We can execute it immediately, without going through SLNet
			message.TryExecute(Protocol, Protocol, Mapping.MessageToExecutorMapping, out var response);

			// Log the result
			var result = (SimpleCreateExampleRowResult)response;
			Protocol.ShowInformationMessage(result?.Description);
		}

		protected void AdvancedCreate()
		{
			// Prepare
			var value1 = Convert.ToDouble(Data[0]);
			var value2 = Convert.ToString(Data[1]);
			var value3 = (DiscreetColumnOption)Convert.ToInt32(Data[2]);

			// Create the InterApp Message
			var message = new AdvancedCreateExampleRow
			{
				ExampleData = new MyTableData
				{
					MyNumericColumn = value1,
					MyStringColumn = value2,
					MyDiscreetColumn = value3,
				},
			};

			// Since the InterApp message is for the current element can't use the InterAppFactory to build our message.
			// We can execute it immediately, without going through SLNet
			message.TryExecute(Protocol, Protocol, Mapping.MessageToExecutorMapping, out var response);

			// Log the result
			var result = (AdvancedCreateExampleRowResult)response;
			Protocol.ShowInformationMessage(result?.Description);
		}

		protected void AdvancedCreateWrong()
		{
			// Prepare
			var value1 = 110;
			var value2 = Convert.ToString(Data[0]);
			var value3 = (DiscreetColumnOption)Convert.ToInt32(Data[1]);

			// Create the InterApp Message
			var message = new AdvancedCreateExampleRow
			{
				ExampleData = new MyTableData
				{
					MyNumericColumn = value1,
					MyStringColumn = value2,
					MyDiscreetColumn = value3,
				},
			};

			// Since the InterApp message is for the current element can't use the InterAppFactory to build our message.
			// We can execute it immediately, without going through SLNet
			message.TryExecute(Protocol, Protocol, Mapping.MessageToExecutorMapping, out var response);

			// Log the result
			var result = (AdvancedCreateExampleRowResult)response;
			Protocol.ShowInformationMessage(result?.Description);
		}

		protected void DelayedCreate()
		{
			// Prepare
			var value1 = Convert.ToDouble(Data[0]);
			var value2 = Convert.ToString(Data[1]);
			var value3 = (DiscreetColumnOption)Convert.ToInt32(Data[2]);

			// Create the InterApp Message
			var message = new DelayedCreateExampleRow
			{
				ExampleData = new MyTableData
				{
					MyNumericColumn = value1,
					MyStringColumn = value2,
					MyDiscreetColumn = value3,
				},
			};

			// Since the InterApp message is for the current element can't use the InterAppFactory to build our message.
			// We can execute it immediately, without going through SLNet
			message.TryExecute(Protocol, Protocol, Mapping.MessageToExecutorMapping, out _);
		}
	}
}