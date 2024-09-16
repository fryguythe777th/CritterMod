using System;
using CritterMod.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CritterMod.Critters
{
	public class PotMimicCritter : ModNPC
	{
        public override void SetStaticDefaults()
        {
			Main.npcFrameCount[this.Type] = 28;
			Main.npcCatchable[this.Type] = true;
			NPCID.Sets.CountsAsCritter[Type] = true;
        }

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson),
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson,
				new FlavorTextBestiaryInfoElement("In the Crimson, even the tiniest creatures want a taste of your flesh. Too bad this little bubble is neither large nor viscous enough to pose a threat...")
			});
		}

		public override void SetDefaults()
        {
			NPC.width = 32;
			NPC.height = 36;
			NPC.aiStyle = -1;
			NPC.noGravity = false;
			NPC.catchItem = ModContent.ItemType<BloodGlop>();
			NPC.lifeMax = 5;
			NPC.damage = 0;
			NPC.value = 0;
			NPC.aiStyle = -1;
			NPC.defense = 0;
			NPC.npcSlots = 0.5f;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.LocalPlayer.ZoneNormalUnderground || (Main.LocalPlayer.ZoneJungle && Main.LocalPlayer.ZoneDirtLayerHeight) || Main.LocalPlayer.ZoneUnderworldHeight || (Main.LocalPlayer.ZoneSnow && Main.LocalPlayer.ZoneDirtLayerHeight) || Main.LocalPlayer.ZoneLihzhardTemple || Main.LocalPlayer.ZoneMarble || Main.LocalPlayer.ZoneUndergroundDesert)
			{
				return 0.2f;
			}
			else
			{
				return 0f;
			}
		}

		public int locale = 0;

		/*
			0 - Normal Underground
			1 - Jungle Underground
			2 - Hell
			3 - Ice Underground
			4 - Lihzard Temple
			5 - Marble Cave
			6 - Desert Underground
		*/

        public override void OnSpawn(IEntitySource source)
        {
			if (Main.LocalPlayer.ZoneNormalUnderground)
			{
				locale = 0;
			}
			else if (Main.LocalPlayer.ZoneJungle && Main.LocalPlayer.ZoneDirtLayerHeight)
			{
                locale = 1;
            }
			else if (Main.LocalPlayer.ZoneUnderworldHeight)
			{
                locale = 2;
            }
			else if (Main.LocalPlayer.ZoneSnow && Main.LocalPlayer.ZoneDirtLayerHeight)
			{
                locale = 3;
            }
			else if (Main.LocalPlayer.ZoneLihzhardTemple)
			{
                locale = 4;
            }
			else if (Main.LocalPlayer.ZoneMarble)
			{
                locale = 5;
            }
			else if (Main.LocalPlayer.ZoneUndergroundDesert)
			{
                locale = 6;
            }
        }

        public override void AI()
        {
            
		}

        public override void FindFrame(int frameHeight)
        {
			int sit = frameHeight * locale;
			int stand = sit + 252;
			int walk1 = sit + 504;
			int walk2 = sit + 756;

			if (NPC.velocity.X != 0)
			{
				if (NPC.frame = stand || NPC.frame = walk1)
				{
					NPC.frame = walk2;
				}
				else if (NPC.frame = walk2)
				{
					NPC.frame = walk1;
				}
			}
		}
    }
}