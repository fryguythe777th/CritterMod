using CritterMod.Critters;
using CritterMod.Common;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace CritterMod
{
    public class ServerConfig : ConfigurationBase
    {
        public static ServerConfig Instance;

        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Name("BloodGlopDisable")]
        [Desc("BloodGlopDisable")]
        [MemberBGColor_Secondary]
        [DefaultValue(false)]
        public bool BloodGlopDisable;

        [Name("PipspookDisable")]
        [Desc("PipspookDisable")]
        [MemberBGColor_Secondary]
        [DefaultValue(false)]
        public bool PipspookDisable;

        [Name("PotMimicDisable")]
        [Desc("PotMimicDisable")]
        [MemberBGColor_Secondary]
        [DefaultValue(false)]
        public bool PotMimicDisable;

        [Name("ShrimpDisable")]
        [Desc("ShrimpDisable")]
        [MemberBGColor_Secondary]
        [DefaultValue(false)]
        public bool ShrimpDisable;

        [Name("ShrumelingDisable")]
        [Desc("ShrumelingDisable")]
        [MemberBGColor_Secondary]
        [DefaultValue(false)]
        public bool ShrumelingDisable;
    }
}
