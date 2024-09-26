using ProtoMod.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items
{
	public class StarTrainTicket : ModItem
	{
		public static int DamageReceiveTime;
		
		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.width = 40;
			Item.height = 40;
			Item.scale = 1f;
			Item.UseSound = SoundID.Item1;
			Item.rare = ItemRarityID.Cyan;
			Item.value = Item.buyPrice(gold: 23);
		}
		
		public override bool? UseItem(Player player)
		{
			TicketTeleport(player);
			player.GetModPlayer<ProtoModPlayer>().StarTrainTicketCD = 4 * 3600;
			DamageReceiveTime = 600;
			return true;
		}

		public override bool CanUseItem(Player player) => player.GetModPlayer<ProtoModPlayer>().StarTrainTicketCD == 0;

		public static void TicketTeleport(Player player)
		{
			new Player.RandomTeleportationAttemptSettings().avoidLava = false;
			new Player.RandomTeleportationAttemptSettings().avoidHurtTiles = false;
			player.TeleportationPotion();
		}

	}
}