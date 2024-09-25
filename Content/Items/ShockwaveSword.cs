using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Filters = Terraria.Graphics.Effects.Filters;

namespace Twig.Content.Items
{
		
	// This is a copy of the Excalibur
	public class ShockwaveSword : ModItem
	{
		internal Effect screenRef;
		private readonly int rippleCount = 5;
		private readonly int rippleSize = 20;
		private readonly int rippleSpeed = 15;
		public static float distortStrength = 100f;
		public static float ShockwaveTimeLeft = 180;

		[Obsolete("Obsolete")]
		public override void Load()
		{
			screenRef = Mod.Assets.Request<Effect>("Assets/Effects/ShockwaveEffect", AssetRequestMode.ImmediateLoad).Value;

			// ...other Load stuff goes here

			if (Main.netMode != NetmodeID.Server)
			{
				// Ref<Effect> screenRef = new Ref<Effect>(GetEffect("Effects/ShockwaveEffect")); // The path to the compiled shader file.
				Filters.Scene["Shockwave"] =
					new Filter(new ScreenShaderData(new Ref<Effect>(screenRef), "Shockwave"), EffectPriority.VeryHigh);
				Filters.Scene["Shockwave"].Load();
			}
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.damage = 72;
			Item.knockBack = 4.5f;
			Item.width = 40;
			Item.height = 40;
			Item.scale = 1f;
			Item.UseSound = SoundID.Item1;
			Item.rare = ItemRarityID.Master;
			Item.value = Item.buyPrice(gold: 23); // Sell price is 5 times less than the buy price.
			Item.DamageType = DamageClass.Melee;
			// Item.shoot = ModContent.ProjectileType<ExampleSwingingEnergySwordProjectile>();
			Item.autoReuse = true;
		}

		// public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
		// 	float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
		// 	Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
		// 	NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.
		//
		// 	return base.Shoot(player, source, position, velocity, type, damage, knockback);
		// }
		public override bool? UseItem(Player player)
		{
			player.TeleportationPotion();
			Main.NewText(Filters.Scene["Shockwave"].IsActive());
			if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
			{
				Filters.Scene.Activate("Shockwave", player.Center).GetShader()
					.UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(player.Center);
			}

			if (player.HasItem(ItemID.RottenEgg))
			{
			}
		
			return true;
		}
		
	}
}