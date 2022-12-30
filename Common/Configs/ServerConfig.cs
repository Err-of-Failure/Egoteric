using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Egoteric.Common.Configs
{
    /// <summary>
    /// The main config file for Egoteric
    /// </summary>
    public class ServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("$Mods.Egoteric.ExpValues.MainHeader")]

        [Label("$Mods.Egoteric.ExpValues.BossExp")]
        [Tooltip("$Mods.Egoteric.ExpValues.BossTooltip")]
        [DefaultValue(1.5f)]
        [ReloadRequired]
        public float BossMultiplier;

        [Label("$Mods.Egoteric.ExpValues.NormalExp")]
        [Tooltip("$Mods.Egoteric.ExpValues.NormalTooltip")]
        [DefaultValue(1f)]
        [ReloadRequired]
        public float NormalMultiplier;

        [Label("$Mods.Egoteric.ExpValues.EvilExp")]
        [Tooltip("$Mods.Egoteric.ExpValues.EvilTooltip")]
        [DefaultValue(-1f)]
        [ReloadRequired]
        public float EvilMultiplier;

        //For Later Use
        //https://github.com/tModLoader/tModLoader/blob/1.4/ExampleMod/Common/Configs/ExampleModConfig.cs
    }
}
