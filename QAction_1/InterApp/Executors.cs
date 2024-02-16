// Ignore Spelling: App

namespace Skyline.Protocol.InterApp
{
	using System;
	using System.Collections.Generic;

	using Skyline.DataMiner.ConnectorAPI.SkylineCommunications.ExampleInterAppCalls.Messages.MyTable;
	using Skyline.Protocol.InterApp.Executors.MyTable;

	public static class Mapping
	{
		private static readonly IDictionary<Type, Type> InternalMessageToExecutorMapping = new Dictionary<Type, Type>
		{
			{ typeof(SimpleCreateExampleRow), typeof(SimpleCreateExampleRowExecutor) },
			{ typeof(AdvancedCreateExampleRow), typeof(AdvancedCreateExampleRowExecutor) },
			{ typeof(DelayedCreateExampleRow), typeof(DelayedCreateExampleRowExecutor) },
		};

		public static IDictionary<Type, Type> MessageToExecutorMapping => InternalMessageToExecutorMapping;
	}
}
