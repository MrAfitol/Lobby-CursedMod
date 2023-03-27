namespace Lobby_CursedMod
{
    using CursedMod.Events.Handlers.Player;
    using CursedMod.Loader.Modules.Enums;
    using CursedMod.Loader.Modules;
    using CursedMod.Loader;
    using CursedMod.Events.Handlers.Round;
    using CursedMod.Events.Handlers.Items;
    using CursedMod.Events.Handlers.Facility.Doors;

    public class Plugin : CursedModule
    {
        public override string ModuleName => "Lobby";
        public override string ModuleAuthor => "MrAfitol";
        public override byte LoadPriority => (byte)ModulePriority.High;
        public override string ModuleVersion => "1.0.0";
        public override string CursedModVersion => CursedModInformation.Version;

        public static Plugin Instance;

        public Config Config;
        public EventHandlers EventHandlers;

        public override void OnLoaded()
        {
            Instance = this;
            Config = GetConfig<Config>("config");
            EventHandlers = new EventHandlers();
            RoundEventsHandler.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;
            RoundEventsHandler.RoundStarted += EventHandlers.OnRoundStarted;
            PlayerEventsHandler.Joined += EventHandlers.OnPlayerJoined;
            DoorsEventsHandler.PlayerInteractingDoor += EventHandlers.OnPlayerInteractDoor;
            ItemsEventsHandler.PlayerPickingUpItem += EventHandlers.OnPlayerPickingUpItem;
            ItemsEventsHandler.PlayerDroppingItem += EventHandlers.OnPlayerDroppedItem;
            ItemsEventsHandler.PlayerThrowingItem += EventHandlers.OnPlayerThrowingItem;

            base.OnLoaded();
        }

        public override void OnUnloaded()
        {
            RoundEventsHandler.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;
            RoundEventsHandler.RoundStarted -= EventHandlers.OnRoundStarted;
            PlayerEventsHandler.Joined -= EventHandlers.OnPlayerJoined;
            DoorsEventsHandler.PlayerInteractingDoor -= EventHandlers.OnPlayerInteractDoor;
            ItemsEventsHandler.PlayerPickingUpItem -= EventHandlers.OnPlayerPickingUpItem;
            ItemsEventsHandler.PlayerDroppingItem -= EventHandlers.OnPlayerDroppedItem;
            ItemsEventsHandler.PlayerThrowingItem -= EventHandlers.OnPlayerThrowingItem;

            EventHandlers = null;
            Config = null;
            Instance = null;

            base.OnUnloaded();
        }
    }
}
