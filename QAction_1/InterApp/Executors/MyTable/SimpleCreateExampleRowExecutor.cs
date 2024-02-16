// Ignore Spelling: App

namespace Skyline.Protocol.InterApp.Executors.MyTable
{
	using System;

	using Newtonsoft.Json;

	using Skyline.DataMiner.ConnectorAPI.SkylineCommunications.ExampleInterAppCalls.Messages.MyTable;
	using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;
	using Skyline.DataMiner.Core.InterAppCalls.Common.MessageExecution;
	using Skyline.DataMiner.Scripting;

	public class SimpleCreateExampleRowExecutor : SimpleMessageExecutor<SimpleCreateExampleRow>
	{
		public SimpleCreateExampleRowExecutor(SimpleCreateExampleRow message) : base(message)
		{
		}

		public override bool TryExecute(object dataSource, object dataDestination, out Message optionalReturnMessage)
		{
			var protocol = (SLProtocol)dataSource;

			var returnMessage = new SimpleCreateExampleRowResult
			{
				Request = Message,
			};

			var newId = Guid.NewGuid().ToString();
			if (!protocol.Exists(Parameter.Mytable.tablePid, newId))
			{
				// Mimic for example setting a http body and triggering a group.
				protocol.SetParameter(Parameter.Mytable.tablePid, JsonConvert.SerializeObject(Message.ExampleData));
				protocol.CheckTrigger(11);

				returnMessage.Description = "Successfully send a create new MyTable row message to the simulated device.";
				returnMessage.RowKey = newId;
				returnMessage.Success = true;
			}
			else
			{
				returnMessage.Description = $"An error occurred while trying to add a new row. There is already a row with ID '{newId}' in the Example Table.";
				returnMessage.RowKey = string.Empty;
				returnMessage.Success = false;
			}

			optionalReturnMessage = returnMessage;
			return true;
		}
	}
}
