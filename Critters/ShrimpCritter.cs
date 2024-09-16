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
	public class ShrimpCritter : ModNPC
	{
        public override void SetStaticDefaults()
        {
			if (System.DateTime.Now.Month == 4 && System.DateTime.Now.Day == 1)
			{
				// DisplayName.SetDefault("Shitter");
			}
			else
			{
				// DisplayName.SetDefault("Shrimp");
			}

			Main.npcFrameCount[this.Type] = 6;
			Main.npcCatchable[this.Type] = true;
			NPCID.Sets.CountsAsCritter[Type] = true;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean),
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
				new FlavorTextBestiaryInfoElement("Quite a shrimple fellow. It may seem clampicated, but it's really not.")
			});
		}

		public override void SetDefaults()
        {
			NPC.width = 30;
			NPC.height = 17;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
			NPC.catchItem = ModContent.ItemType<Shrimp>();
			NPC.lifeMax = 5;
			NPC.damage = 0;
			NPC.value = 0;
			NPC.aiStyle = -1;
			NPC.defense = 0;
			NPC.npcSlots = 0.5f;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.LocalPlayer.ZoneBeach)
			{
				return 0.1f;
			}
			else
			{
				return 0f;
			}
		}

		public int State = 0;
		public bool DeterminingDirection = true;
		public Vector2 swimVelocity;
		public int swimCounter = 150;
		public int frameCount = 0;
		public int frameRef = 0;

        public override void OnSpawn(IEntitySource source)
        {
			State = 0;
			frameCount = 3;
			frameRef = 0;
        }

        public override void AI()
        {
			if (NPC.wet)
            {
				int frame = NPC.frame.Y;
				if (State == 0)
				{
					NPC.velocity = Vector2.Zero;

					if (++frameCount == 30)
					{
						frameCount = 0;

						if (frameRef == 1)
						{
							frameRef = 0;
						}
						else
						{
							frameRef = 1;
						}
					}

					if (Main.rand.NextBool(100))
					{
						State = 1;
						//Main.NewText("Switching to Unfurl");
					}
				}
				else if (State == 1)
				{
					NPC.velocity = Vector2.Zero;

					//Main.NewText("Debug 4");
					if (++frameCount == 30)
					{
						frameCount = 0;
						//Main.NewText("Debug 1");

						if (frameRef == 0)
						{
							//Main.NewText("Debug 2");
							frameRef = 2;
						}
						else if (frameRef == 2)
						{
							frameRef = 3;
						}
						else if (frameRef == 3)
						{
							frameRef = 4;
						}
						else if (frameRef == 4)
						{
							State = 3;
							//Main.NewText("Switching to Movement");
							frameRef = 4;
						}
						else
						{
							//Main.NewText("Debug 3");
							frameRef = 0;
						}
					}
				}
				else if (State == 2)
				{
					//Main.NewText("Debug 5");
					NPC.velocity = Vector2.Zero;

					if (++frameCount == 30)
					{
						frameCount = 0;
						//Main.NewText("Debug 6");

						if (frameRef == 4 || frameRef == 5)
						{
							//Main.NewText("Debug 7");
							frameRef = 3;
						}
						else if (frameRef == 3)
						{
							frameRef = 2;
						}
						else if (frameRef == 2)
						{
							frameRef = 0;
							State = 0;
							//Main.NewText("Switching to Idle");
						}
						else
						{
							//Main.NewText("Debug 8");
							frameRef = 4;
						}
					}
				}
				else if (State == 3)
				{
					if (++frameCount == 30)
					{
						frameCount = 0;
						if (frameRef == 4)
						{
							frameRef = 5;
						}
						else
						{
							frameRef = 4;
						}
					}

					if (DeterminingDirection == true)
					{
						int direction = Main.rand.NextBool().ToInt();
						NPC.spriteDirection = direction;

						swimVelocity = new Vector2(0, 1).RotatedBy(MathHelper.ToRadians(Main.rand.Next(80, 100) + (180 * direction)));

						DeterminingDirection = false;
					}
					else
					{
						NPC.velocity = swimVelocity;
						swimCounter--;
						if (swimCounter == 0)
						{
							//Main.NewText("Swim over");
							swimCounter = 150;
							if (Main.rand.NextBool() == true)
							{
								DeterminingDirection = true;
							}
							else
							{
								State = 2;
								DeterminingDirection = true;
								//Main.NewText("Switching to Furl");
							}
						}
					}
				}

				frameCount++;
			}
			else
            {
				if (++frameCount == 30)
				{
					frameCount = 0;
					if (frameRef == 4)
					{
						frameRef = 5;
					}
					else
					{
						frameRef = 4;
					}
				}
				NPC.velocity = new Vector2((1 * NPC.spriteDirection), 7);
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