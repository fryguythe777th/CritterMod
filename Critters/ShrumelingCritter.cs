using System;
using CritterMod.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CritterMod.Critters
{
	public class ShrumelingCritter : ModNPC
	{
        public override void SetStaticDefaults()
        {
			Main.npcFrameCount[this.Type] = 8;
			Main.npcCatchable[this.Type] = true;
			NPCID.Sets.CountsAsCritter[Type] = true;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom),
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom,
				new FlavorTextBestiaryInfoElement("A young fungal creature. It spends most of its time in the ground, but it can uproot and flee if its hyphae sense danger.")
			});
		}

		public override void SetDefaults()
        {
			NPC.width = 28;
			NPC.height = 34;
			NPC.aiStyle = -1;
			NPC.noGravity = false;
			NPC.catchItem = ModContent.ItemType<Shrumeling>();
			NPC.lifeMax = 5;
			NPC.damage = 0;
			NPC.value = 0;
			NPC.aiStyle = -1;
			NPC.defense = 0;
			NPC.npcSlots = 0.5f;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.LocalPlayer.ZoneGlowshroom)
			{
				return 0.1f;
			}
			else
			{
				return 0f;
			}
		}

		public int frameRef = 0;
		public bool Bool;
		public int runTimer = 120;

        public override void OnSpawn(IEntitySource source)
        {
			frameRef = 0;
        }

        public override void AI()
        {
			if (NPC.FindClosestPlayer(out float targetDistance) != -1 && targetDistance < 300.0)
            {
				if (runTimer == 0)
				{
                    //Main.NewText("Targeting");
                    if (Bool == true)
                    {
                        frameRef = 1;
                        Bool = false;
                    }

                    if (Main.player[NPC.FindClosestPlayer()].Center.X >= NPC.Center.X)
                    {
						if (NPC.velocity.X > -3)
						{
							NPC.velocity.X -= 0.1f;
						}
                        NPC.spriteDirection = -1;
                    }
                    else
                    {
                        if (NPC.velocity.X < 3)
                        {
                            NPC.velocity.X += 0.1f;
                        }
                        NPC.spriteDirection = 1;
                    }

                    if (++NPC.frameCounter == 5)
                    {
                        NPC.frameCounter = 0;
                        if (++frameRef >= 6)
                        {
                            frameRef = 1;
                        }
                        else
                        {
                            frameRef++;
                        }
                    }

					if (NPC.collideX && NPC.spriteDirection == -1)
					{
						NPC.velocity = new Vector2(2, -3);
					}
					else if (NPC.collideX && NPC.spriteDirection == 1)
					{
						NPC.velocity = new Vector2(-2, -3);
					}
				}
				else
				{
					runTimer--;
					frameRef = 0;
				}
            }
			else
            {
				Bool = true;
				frameRef = 7;
				NPC.velocity.X = 0;
				//Main.NewText("Please for the love of god");

				if (runTimer < 120)
				{
					runTimer++;
				}
            }

			//NPC.velocity += new Vector2(0, 6);

			if (Main.rand.NextBool(10))
            {
				int num13 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 165, NPC.velocity.X, NPC.velocity.Y, 50);
				Dust obj = Main.dust[num13];
				obj.velocity *= 0.1f;
				Main.dust[num13].noGravity = true;
			}

			float num6 = (float)Main.rand.Next(28, 42) * 0.005f;
			num6 += (float)(270 - Main.mouseTextColor) / 500f;
			float num7 = 0.1f;
			float num8 = 0.3f + num6 / 2f;
			float num9 = 0.6f + num6;
			float num10 = 0.65f;
			num7 *= num10;
			num8 *= num10;
			num9 *= num10;
			Lighting.AddLight((int)NPC.Center.X / 16, (int)NPC.Center.Y / 16, num7, num8, num9);
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
			else if (frameRef == 6)
			{
				NPC.frame.Y = frameHeight * 6;
			}
			else if (frameRef == 7)
			{
				NPC.frame.Y = frameHeight * 7;
			}
		}
    }
}