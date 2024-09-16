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
	public class BloodGlopCritter : ModNPC
	{
        public override void SetStaticDefaults()
        {
			Main.npcFrameCount[this.Type] = 5;
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
			NPC.width = 10;
			NPC.height = 10;
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
			if (Main.LocalPlayer.ZoneCrimson)
			{
				return 0.2f;
			}
			else
			{
				return 0f;
			}
		}

		public int hangTimer = 360;
		public int kysTimer = 360;

        public override void AI()
        {
            if (NPC.collideY)
            {
                NPC.velocity.X = 0;
            }

            if (NPC.collideX || NPC.collideY)
			{
				kysTimer = 360;
				if (hangTimer > 0)
				{
					hangTimer--;
				}
				else
				{
					hangTimer = 60 * Main.rand.Next(3, 8);
					NPC.velocity = new Vector2(Main.rand.Next(-7, 8), Main.rand.Next(-8, -4));
				}
			}
			else
			{
				if (kysTimer == 0)
				{
                    NPC.life = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        Dust.NewDust(NPC.Center, 1, 1, DustID.Blood, Main.rand.Next(-1, 1), Main.rand.Next(-1, 1), 100);
                    }
                    SoundEngine.PlaySound(SoundID.NPCDeath1.WithVolumeScale(0.25f), NPC.Center);
                }
				kysTimer--;
			}

			if (NPC.Hitbox.Intersects(new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 1, 1)))
			{
                NPC.life = 0;
                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDust(NPC.Center, 1, 1, DustID.Blood, Main.rand.Next(-1, 1), Main.rand.Next(-1, 1), 100);
                }
                SoundEngine.PlaySound(SoundID.NPCDeath1.WithVolumeScale(0.25f), NPC.Center);
				//Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ProjectileID.VampireHeal, 0, 0);
            }
		}

        public override void FindFrame(int frameHeight)
        {
			if (NPC.collideX || NPC.collideY)
			{
				NPC.frame.Y = frameHeight;
			}
			else
			{
				NPC.frame.Y = 0;
			}
		}
    }
}