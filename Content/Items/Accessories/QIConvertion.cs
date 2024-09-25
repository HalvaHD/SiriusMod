using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Content.Projectiles;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items.Accessories
{
	//[AutoloadEquip(EquipType.Shield)] // Load the spritesheet you create as a shield for the player when it is equipped.
	public class QIConvertion : ModItem
	{
		public static int number;
		public static bool IsActive = false;
		public static Terraria.NPC targett;
		public float sqrDistanceToTarget;

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.buyPrice(1);
			Item.rare = ItemRarityID.LightRed;
			Item.accessory = true;
			Item.defense = 10;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<QIConvertionPlayer>().DashAccessoryEquipped = true;
			player.GetModPlayer<QIConvertionPlayer>().QIKaboom = true;
			if (QIConvertionPlayer.DashTimer > 0 && FindClosestNPC(32) != null && IsActive == false)
			{
				targett = FindClosestNPC(50);
					// Main.NewText("ПРОВЕРКА УДАЛАСЬ");
				SoundStyle style = new SoundStyle("Twig/Assets/Sounds/QIKaboomActivation");
				SoundEngine.PlaySound(style);
				Projectile.NewProjectile(new EntitySource_Misc("QIKaboom"), targett.Center, Vector2.Zero,
					ModContent.ProjectileType<QIKaboom>(), 0, 0, Main.LocalPlayer.whoAmI);
				IsActive = true;
			}
			if (QIConvertionPlayer.DashTimer > 0)
			{
				player.immune = true;
			}
		}
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture = ModContent.Request<Texture2D>("Twig/Content/Items/Accessories/QIConvertion_Glowmask", AssetRequestMode.ImmediateLoad).Value;
			spriteBatch.Draw
			(
				texture,
				new Vector2
				(
					Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
					Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
				),
				new Rectangle(0, 0, texture.Width, texture.Height),
				Color.Cyan,
				rotation,
				texture.Size() * 0.5f,
				scale, 
				SpriteEffects.None, 
				0f
			);
		}
		public Terraria.NPC FindClosestNPC(float maxDetectDistance)
		{
			Terraria.NPC closestNPC = null;

			// Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			// Loop through all NPCs(max always 200)
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				Terraria.NPC target = Main.npc[k];
				// Check if NPC able to be targeted. It means that NPC is
				// 1. active (alive)
				// 2. chaseable (e.g. not a cultist archer)
				// 3. max life bigger than 5 (e.g. not a critter)
				// 4. can take damage (e.g. moonlord core after all it's parts are downed)
				// 5. hostile (!friendly)
				// 6. not immortal (e.g. not a target dummy)
				if (target.CanBeChasedBy())
				{
					// The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
					if (Main.LocalPlayer.Center.Y <= target.Bottom.Y)
					{
						 sqrDistanceToTarget = Vector2.DistanceSquared(target.Bottom, Main.LocalPlayer.Center);
					}
					else if (Main.LocalPlayer.Center.Y > target.Bottom.Y && Main.LocalPlayer.Center.Y < target.Top.Y)
					{
						 sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Main.LocalPlayer.Center);
					}
					else if (Main.LocalPlayer.Center.Y >= target.Top.Y)
					{
						 sqrDistanceToTarget = Vector2.DistanceSquared(target.Top, Main.LocalPlayer.Center);
					}
					

					// Check if it is within the radius
					if (sqrDistanceToTarget < sqrMaxDetectDistance)
					{
						sqrMaxDetectDistance = sqrDistanceToTarget;
						closestNPC = target;
					}
				}
			}

			return closestNPC;
		}
	}

	public class QIConvertionPlayer : ModPlayer
	{
		// These indicate what direction is what in the timer arrays used
		public const int DashRight = 2;
		public const int DashLeft = 3;

		public const int DashCooldown = 300; // Time (frames) between starting dashes. If this is shorter than DashDuration you can start a new dash before an old one has finished
		public const int DashDuration = 60; // Duration of the dash afterimage effect in frames

		// The initial velocity.  10 velocity is about 37.5 tiles/second or 50 mph
		public const float DashVelocity = 10f;

		// The direction the player has double tapped.  Defaults to -1 for no dash double tap
		public int DashDir = -1;

		// The fields related to the dash accessory
		public bool DashAccessoryEquipped;
		public bool QIKaboom;
		public int DashDelay; // frames remaining till we can dash again
		public static int DashTimer; // frames remaining in the dash

		public override void ResetEffects() {
			// Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
			DashAccessoryEquipped = false;
			QIKaboom = false;

			// ResetEffects is called not long after player.doubleTapCardinalTimer's values have been set
			// When a directional key is pressed and released, vanilla starts a 15 tick (1/4 second) timer during which a second press activates a dash
			// If the timers are set to 15, then this is the first press just processed by the vanilla logic.  Otherwise, it's a double-tap
			 if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15) {
				DashDir = DashRight;
			 }
			else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15) {
				DashDir = DashLeft;
			}
			else {
				DashDir = -1;
			}
		}
		// This is the perfect place to apply dash movement, it's after the vanilla movement code, and before the player's position is modified based on velocity.
		// If they double tapped this frame, they'll move fast this frame
		public override void PreUpdateMovement() {
			// if the player can use our dash, has double tapped in a direction, and our dash isn't currently on cooldown
			if (CanUseDash() && DashDir != -1 && DashDelay == 0) {
				Vector2 newVelocity = Player.velocity;

				switch (DashDir) {
					// Only apply the dash velocity if our current speed in the wanted direction is less than DashVelocity
					case DashLeft when Player.velocity.X > -DashVelocity:
					case DashRight when Player.velocity.X < DashVelocity: {
							// X-velocity is set here
							float dashDirection = DashDir == DashRight ? 1 : -1;
							newVelocity.X = dashDirection * DashVelocity;
							break;
						}
					default:
						return; // not moving fast enough, so don't start our dash
				}

				// start our dash
				DashDelay = DashCooldown;
				DashTimer = DashDuration;
				QIConvertion.IsActive = false;
				Player.velocity = newVelocity;

				// Here you'd be able to set an effect that happens when the dash first activates
				// Some examples include:  the larger smoke effect from the Master Ninja Gear and Tabi
			}

			if (DashDelay > 0)
				DashDelay--;

			if (DashTimer > 0) { // dash is active
				// This is where we set the afterimage effect.  You can replace these two lines with whatever you want to happen during the dash
				// Some examples include:  spawning dust where the player is, adding buffs, making the player immune, etc.
				// Here we take advantage of "player.eocDash" and "player.armorEffectDrawShadowEOCShield" to get the Shield of Cthulhu's afterimage effect
				Player.eocDash = DashTimer;
				Player.armorEffectDrawShadowEOCShield = true;

				// count down frames remaining
				DashTimer--;
			}
		}
		private bool CanUseDash() {
			return DashAccessoryEquipped
				&& Player.dashType == DashID.None // player doesn't have Tabi or EoCShield equipped (give priority to those dashes)
				&& !Player.setSolar // player isn't wearing solar armor
				&& !Player.mount.Active; // player isn't mounted, since dashes on a mount look weird
		}
	}
}

