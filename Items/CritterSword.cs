using CritterMod.Critters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CritterMod.Items
{
	public class CritterSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("CritterSword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			// Tooltip.SetDefault("This is a basic modded sword.");
		}

		public override void SetDefaults()
		{
			Item.damage = 450;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
	}
}