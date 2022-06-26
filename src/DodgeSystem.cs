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
	public class DodgeSystem : ModSystem
	{
		public static string MOD_ID = "dodgeroll";
		
		public override void Start(ICoreAPI api)
		{
			api.RegisterEntityBehaviorClass("dodgerollentitybehaviour", typeof (DodgeRollEntityBehaviour));
		}
		
	}
}