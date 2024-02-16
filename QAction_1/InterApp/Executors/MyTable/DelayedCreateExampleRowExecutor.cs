// Ignore Spelling: App

namespace Skyline.Protocol.InterApp.Executors.MyTable
{
	using System;

	using Newtonsoft.Json;

	using Skyline.DataMiner.ConnectorAPI.SkylineCommunications.ExampleInterAppCalls.Messages.MyTable;
	using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;
	using Skyline.DataMiner.Core.InterAppCalls.Common.MessageExecution;
	using Skyline.DataMiner.Scripting;
	using Skyline.Protocol.Tables;

	public class DelayedCreateExampleRowExecutor : SimpleMessageExecutor<DelayedCreateExampleRow>
	{
		private readonly DelayedCreateExampleRowResult result;

		public DelayedCreateExampleRowExecutor(DelayedCreateExampleRow message) : base(message)
		{
			result = new DelayedCreateExampleRowResult
			{
				Guid = message.Guid,
				Request = message,
			};
		}

		public override bool TryExecute(object dataSource, object dataDestination, out Message optionalReturnMessage)
		{
			var protocol = (SLProtocol)dataSource;

			// Create a new unique primary key to be added
			var id = Guid.NewGuid().ToString();
			Message.ExampleData.Instance = id;
			result.RowKey = id;

			// Add the delayed InterApp Message to an internal buffer
			new IAC_MessagesTableRow
			{
				Guid = Guid.Parse(Message.Guid),
				Status = IAC_MessageStatus.Bufferred,
				Request = Message,
				RequestType = typeof(DelayedCreateExampleRow),
				Response = result,
				ResponseType = typeof(DelayedCreateExampleRowResult),
			}.SaveToProtocol(protocol);

			// Mimic for example setting a http body and triggering a group.
			protocol.SetParameter(Parameter.commandbody, JsonConvert.SerializeObject(Message.ExampleData));
			protocol.CheckTrigger(11);

			optionalReturnMessage = null;
			return true;
		}
	}
}
