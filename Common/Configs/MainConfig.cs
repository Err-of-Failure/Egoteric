﻿using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Egoteric.Common.Configs
{
    /// <summary>
    /// The main config file for Egoteric
    /// </summary>
    public class MainConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        //For Later Use
        //https://github.com/tModLoader/tModLoader/blob/1.4/ExampleMod/Common/Configs/ExampleModConfig.cs
    }
}