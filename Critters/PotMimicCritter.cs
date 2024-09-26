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

				new BestiaryPortraitBackgroundProviderPreferenceInfoElement(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground),
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
				new FlavorTextBestiaryInfoElement("Some things are not as they seem...")
			});
		}

		public override void SetDefaults()
		{
			NPC.width = 32;
			NPC.height = 37;
			NPC.aiStyle = -1;
			NPC.noGravity = false;
			NPC.catchItem = ModContent.ItemType<PotMimic>();
			NPC.lifeMax = 5;
			NPC.damage = 0;
			NPC.value = 0;
			NPC.aiStyle = -1;
			NPC.defense = 0;
			NPC.npcSlots = 0.5f;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.LocalPlayer.ZoneNormalUnderground || Main.LocalPlayer.ZoneNormalCaverns || (Main.LocalPlayer.ZoneJungle && Main.LocalPlayer.ZoneDirtLayerHeight) || (Main.LocalPlayer.ZoneJungle && Main.LocalPlayer.ZoneRockLayerHeight) || Main.LocalPlayer.ZoneUnderworldHeight || (Main.LocalPlayer.ZoneSnow && Main.LocalPlayer.ZoneDirtLayerHeight) || (Main.LocalPlayer.ZoneSnow && Main.LocalPlayer.ZoneRockLayerHeight) || Main.LocalPlayer.ZoneLihzhardTemple || Main.LocalPlayer.ZoneMarble || Main.LocalPlayer.ZoneUndergroundDesert)
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
			if (Main.LocalPlayer.ZoneMarble)
			{
				locale = 5;
			}
			else if (Main.LocalPlayer.ZoneNormalUnderground || Main.LocalPlayer.ZoneNormalCaverns)
			{
				locale = 0;
			}
			else if (Main.LocalPlayer.ZoneLihzhardTemple)
			{
				locale = 4;
			}
			else if (Main.LocalPlayer.ZoneJungle && Main.LocalPlayer.ZoneDirtLayerHeight || Main.LocalPlayer.ZoneJungle && Main.LocalPlayer.ZoneRockLayerHeight)
			{
				locale = 1;
			}
			else if (Main.LocalPlayer.ZoneUnderworldHeight)
			{
				locale = 2;
			}
			else if (Main.LocalPlayer.ZoneSnow && Main.LocalPlayer.ZoneDirtLayerHeight || Main.LocalPlayer.ZoneSnow && Main.LocalPlayer.ZoneRockLayerHeight)
			{
				locale = 3;
			}
			else if (Main.LocalPlayer.ZoneUndergroundDesert)
			{
				locale = 6;
			}
		}

		public int timer = 600;
		public int state = 0; //0 - sit, 1 - stand, 2 - walk
		public bool potSit = true;
		public int direction = 1;

		public override void AI()
		{
			if (state == 0)
			{
				NPC.velocity.X = 0;

				potSit = true;

				if (timer == 0)
				{
					state = 1;
					timer = 120;
				}

				timer--;

                NPC.ShowNameOnHover = false;
                NPC.nameOver = 0f;
            }
			else if (state == 1)
			{
				NPC.velocity.X = 0;

				potSit = false;

				if (timer == 0)
				{
					state = 2;
					timer = 300;
					direction = Main.rand.NextBool().ToDirectionInt();
				}

				timer--;

                NPC.ShowNameOnHover = true;
            }
			else if (state == 2)
			{
				potSit = false;
				NPC.velocity.X = 1 * direction;

				if (timer == 0)
				{
					state = 0;
					timer = Main.rand.Next(600, 1200);
				}

				timer--;
			}
		}

        /*
			0 - Normal Underground
			1 - Jungle Underground
			2 - Hell
			3 - Ice Underground
			4 - Lihzard Temple
			5 - Marble Cave
			6 - Desert Underground
		*/

        public override void HitEffect(NPC.HitInfo hit) //MIMICS VANILLA POT BEHAVIOUR
        {
            NPC.StrikeInstantKill();
            SoundEngine.PlaySound(SoundID.Shatter, NPC.Center);

            int gore = 0;

            switch (locale)
            {
                case 0:
                    gore = 51;
                    break;
                case 1:
                    gore = 199;
                    break;
                case 2:
                    gore = 203;
                    break;
                case 3:
                    gore = 197;
                    break;
                case 4:
                    gore = 217;
                    break;
                case 5:
                    gore = 701;
                    break;
                case 6:
                    gore = 1122;
                    break;
            }

            Gore.NewGore(NPC.GetSource_Death(), NPC.Center, new Vector2(Main.rand.Next(-2, 3), Main.rand.Next(1, 3)), gore);
            Gore.NewGore(NPC.GetSource_Death(), NPC.Center, new Vector2(Main.rand.Next(-2, 3), Main.rand.Next(1, 3)), gore + 1);

			if (Main.rand.NextBool(500)) //coin portal
			{
				Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center, Vector2.Zero, ProjectileID.CoinPortal, 0, 0);
			}
			else if (Main.rand.NextBool(45)) //potions
			{
				int randPotion = Main.rand.Next(1, 11);
				int potionID = 292;

				switch (randPotion)
				{
					case 1: //ironskin
						potionID = 292;
						break;
					case 2: //shine
						potionID = 298;
						break;
					case 3: //night owl
						potionID = 299;
						break;
					case 4: //swiftness
						potionID = 290;
						break;
					case 5: //mining
						potionID = 2322;
						break;
					case 6: // calming
						potionID = 2324;
						break;
					case 7: //builder
						potionID = 2325;
						break;
					case 8: //recall
						potionID = 2350;
						break;
                    case 9: //recall
                        potionID = 2350;
                        break;
                    case 10: //recall
                        potionID = 2350;
                        break;
                }

				Item.NewItem(NPC.GetSource_Death(), NPC.Hitbox, potionID);
			}
			else
			{
				int reward = Main.rand.Next(0, 5);

				if (reward == 0) //torch
				{
					if (locale == 1) //jungle
					{
						Item.NewItem(NPC.GetSource_Death(), NPC.Hitbox, ItemID.JungleTorch, Main.rand.Next(4, 13)); //jungle torch
					}
					else if (locale == 3) //ice
					{
                        Item.NewItem(NPC.GetSource_Death(), NPC.Hitbox, ItemID.IceTorch, Main.rand.Next(2, 7)); //ice torch
                    }
					else if (locale == 6) //desert
					{
                        Item.NewItem(NPC.GetSource_Death(), NPC.Hitbox, ItemID.DesertTorch, Main.rand.Next(4, 13)); //desert torch
                    }
					else //anywhere else
					{
                        Item.NewItem(NPC.GetSource_Death(), NPC.Hitbox, ItemID.Torch, Main.rand.Next(4, 13)); //normal torch
                    }
				}

				if (reward == 1) //ammo
				{
					int ammoType = ItemID.WoodenArrow;

					if (Main.hardMode) //hardmode ammo
					{
						switch (Main.rand.NextBool())
						{
							case true:
								ammoType = ItemID.UnholyArrow;
								break;
							case false:
								ammoType = ItemID.SilverBullet;
								break;
						}
					}

					if (locale != 2) //shurikens or grenades
					{
						if (Main.rand.NextBool())
						{
							switch (Main.hardMode)
							{
								case true:
									ammoType = ItemID.Grenade;
									break;
								case false:
									ammoType = ItemID.Shuriken;
									break;
							}
						}
					}

					if (locale == 2) //hellfire arrows
					{
						ammoType = ItemID.HellfireArrow;
					}

                    Item.NewItem(NPC.GetSource_Death(), NPC.Hitbox, ammoType, Main.rand.Next(10, 21)); //spawn the ammo
                }

				if (reward == 2) //potion
				{
					switch (Main.hardMode)
					{
						case true:
                            Item.NewItem(NPC.GetSource_Death(), NPC.Hitbox, ItemID.HealingPotion); //healing potion
							break;
						case false:
                            Item.NewItem(NPC.GetSource_Death(), NPC.Hitbox, ItemID.LesserHealingPotion); //lesser healing potion
							break;
                    }
				}

				if (reward == 3) //bombs
				{
					if (locale == 6) //desert
					{
                        Item.NewItem(NPC.GetSource_Death(), NPC.Hitbox, ItemID.ScarabBomb, Main.rand.Next(1, 5)); //scarab bombs
                    }
					else
					{
                        Item.NewItem(NPC.GetSource_Death(), NPC.Hitbox, ItemID.Bomb, Main.rand.Next(1, 5)); //normal bombs
                    }
				}

				if (reward == 4) //ropes
				{
                    Item.NewItem(NPC.GetSource_Death(), NPC.Hitbox, ItemID.Rope, Main.rand.Next(20, 41)); //drop them
                }
			}
        }

		public int frameCounter = 0;

		public override void FindFrame(int frameHeight)
		{
			int sit = 37 * locale;
			int stand = sit + 259;
			int walk1 = sit + 518;
			int walk2 = sit + 777;

			if (NPC.velocity.X != 0)
			{
				if (frameCounter % 4 == 0 && (NPC.frame.Y == stand || NPC.frame.Y == walk1))
				{
					NPC.frame.Y = walk2;
				}
				else if (frameCounter % 4 == 0 && NPC.frame.Y == walk2)
				{
					NPC.frame.Y = walk1;
				}
			}
			else if (potSit)
			{
				NPC.frame.Y = sit;
			}
			else
			{
				NPC.frame.Y = stand;
			}

			frameCounter++;
		}
	}
}

		