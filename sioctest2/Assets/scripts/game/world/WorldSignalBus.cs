using strange.extensions.signal.impl;
using strange.extensions.command.api;


namespace game.world
{
	public class WorldSignalBus
	{
		public readonly Signal<string> setupWorld = new Signal<string>();
		
		private ICommandBinder commandBinder;
		
		public WorldSignalBus(ICommandBinder commandBinder)
		{
			this.commandBinder = commandBinder;
			mapSignals();
		}
		
		private void mapSignals()
		{
			commandBinder.Bind(setupWorld).To<SetupWorldCmd>();	
		}
	}
}

