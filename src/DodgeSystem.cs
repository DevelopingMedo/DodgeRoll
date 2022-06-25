using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

[assembly: ModInfo("DodgeRoll",
	Description = "Adds the capability to dodge in combat.",
	Website     = "https://github.com/SamoMedo/DodgeRoll",
	Authors     = new []{ "SamoMedo" })]
[assembly: ModDependency("game", "1.16.5")]

namespace DodgeRoll
{
	/// <summary> Main system for the "DodgeCapacity" mod, which allows certain
	///           blocks such as chests to be picked up and carried around. </summary>
	public class DodgeSystem : ModSystem
	{
		public static string MOD_ID = "dodgeroll";
		
		public override bool AllowRuntimeReload => true;
		
		// Client
		public ICoreClientAPI ClientAPI { get; private set; }
		public IClientNetworkChannel ClientChannel { get; private set; }
		public HudOverlayRenderer HudOverlayRenderer { get; private set; }
		
		// Server
		public ICoreServerAPI ServerAPI { get; private set; }
		public IServerNetworkChannel ServerChannel { get; private set; }
		public DeathHandler DeathHandler { get; private set; }
		public BackwardCompatHandler BackwardCompatHandler { get; private set; }
		
		// Common
		public DodgeHandler DodgeHandler { get; private set; }
		
		
		public override void Start(ICoreAPI api)
		{
			DodgeHandler = new DodgeHandler(this);
		}
		
		public override void StartClientSide(ICoreClientAPI api)
		{
			ClientAPI     = api;
			ClientChannel = api.Network.RegisterChannel(MOD_ID)
				.RegisterMessageType<LockSlotsMessage>()
				.RegisterMessageType<PickUpMessage>()
				.RegisterMessageType<PlaceDownMessage>()
				.RegisterMessageType<SwapSlotsMessage>();
			
			EntityDodgeRenderer = new EntityDodgeRenderer(api);
			HudOverlayRenderer  = new HudOverlayRenderer(api);
			
			DodgeHandler.InitClient();
		}
		
		
		public override void StartServerSide(ICoreServerAPI api)
		{
			api.Register<EntityBehaviorDropCarriedOnDamage>();
			
			ServerAPI     = api;
			ServerChannel = api.Network.RegisterChannel(MOD_ID)
				.RegisterMessageType<LockSlotsMessage>()
				.RegisterMessageType<PickUpMessage>()
				.RegisterMessageType<PlaceDownMessage>()
				.RegisterMessageType<SwapSlotsMessage>();
			
			DeathHandler          = new DeathHandler(api);
			BackwardCompatHandler = new BackwardCompatHandler(api);
			
			DodgeHandler.InitServer();
		}
	}
}