using System;
using CritterMod.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CritterMod.Critters
{
	public class PipspookCritter : ModNPC
	{
        public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Pipspook");

			Main.npcFrameCount[this.Type] = 6;
			Main.npcCatchable[this.Type] = true;
			NPCID.Sets.CountsAsCritter[Type] = true;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Graveyard),
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Graveyard,
				new FlavorTextBestiaryInfoElement("A grim reminder of one's own mortality. Rather cute for what it is.")
			});
		}

		public override void SetDefaults()
        {
			NPC.width = 30;
			NPC.height = 34;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
			NPC.catchItem = ModContent.ItemType<Pipspook>();
			NPC.lifeMax = 5;
			NPC.damage = 0;
			NPC.value = 0;
			NPC.aiStyle = -1;
			NPC.defense = 0;
			NPC.npcSlots = 0.5f;
			NPC.alpha = 125;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.LocalPlayer.ZoneGraveyard && !ServerConfig.Instance.PipspookDisable)
			{
				return 0.2f;
			}
			else
			{
				return 0f;
			}
		}

		public int frameRef;
		public int frameCount;
		public bool Slag = false;

        public override void OnSpawn(IEntitySource source)
        {
			frameCount = 3;
			frameRef = 0;
			NPC.spriteDirection = Main.rand.NextBool().ToDirectionInt();
			NPC.position.Y += Main.rand.Next(15, 19);
        }

        public override void AI()
        {
			if (Slag == false)
				NPC.gfxOffY = (float)Math.Sin(Main.GameUpdateCount * 0.08) * 1.3f; //SINE WAVE, regular sine wave is just Math.Sin(*regularly updating variable*), 0.08 is to make it slower, 1.3 is to make it taller

			if (NPC.FindClosestPlayer(out float targetDistance) != -1 && targetDistance < 215.0)
            {
				if (Slag == false)
                {
					frameRef = 1;

					if (Main.rand.NextBool(1000))
                    {
						Slag = true;
                    }
                }
				else
                {
					if (++NPC.frameCounter == 3)
                    {
						NPC.frameCounter = 0;
						if (frameRef == 5)
                        {
							NPC.life = 0;
							for (int i = 0; i < 3; i++)
                            {
								Dust.NewDust(NPC.Center, 3, 3, DustID.Cloud, Main.rand.Next(-1, 1), Main.rand.Next(-1, 1), 100);
                            }
                        }
						frameRef++;
                    }
                }
            }
			else
            {
				frameRef = 0;
            }
		}

        public override void FindFrame(int frameHeight)
        {
			if (frameRef == 0)
            {
				NPC.frame.Y = 0;
            }
			else if (frameRef == 1)
            {
				NPC.frame.Y = frameHeight;
            }
			else if (frameRef == 2)
            {
				NPC.frame.Y = frameHeight * 2;
            }
			else if (frameRef == 3)
            {
				NPC.frame.Y = frameHeight * 3;
            }
			else if (frameRef == 4)
            {
				NPC.frame.Y = frameHeight * 4;
            }
			else if (frameRef == 5)
            {
				NPC.frame.Y = frameHeight * 5;
            }
		}
    }
}