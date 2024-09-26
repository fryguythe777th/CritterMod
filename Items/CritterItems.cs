using CritterMod.Critters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CritterMod.Items
{
	public class Shrimp : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToCapturedCritter((short)ModContent.NPCType<ShrimpCritter>());
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 5);
        }

        public override void AddRecipes()
        {
            Recipe shrimpRecipe = Recipe.Create(ItemID.CookedShrimp);
            shrimpRecipe.AddIngredient(ModContent.ItemType<Shrimp>());
            shrimpRecipe.AddTile(TileID.CookingPots);
            shrimpRecipe.Register();
        }
    }

    public class Shrumeling : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToCapturedCritter((short)ModContent.NPCType<ShrumelingCritter>());
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 5);
        }
    }

    public class Pipspook : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToCapturedCritter((short)ModContent.NPCType<PipspookCritter>());
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 5);
            Item.alpha = 125;
        }
    }

    public class BloodGlop : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToCapturedCritter((short)ModContent.NPCType<BloodGlopCritter>());
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 5);
        }
    }

    public class PotMimic : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToCapturedCritter((short)ModContent.NPCType<PotMimicCritter>());
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 5);
        }
    }
}