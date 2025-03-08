using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SiriusMod.Helpers;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SiriusMod.Content.Tiles.LaboratoryTiles
{
	public class LabBigToilet : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			
			TileID.Sets.CanBeSatOnForPlayers[Type] = true; // Facilitates calling ModifySittingTargetInfo for Players
			TileID.Sets.DisableSmartCursor[Type] = true;
			
			AdjTiles = new int[] { TileID.Toilets };
			AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.Toilet"));
			
			Main.tileLavaDeath[Type] = false;
			Main.tileWaterDeath[Type] = false;

			TileObjectData.newTile.Height = 5;
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16, 16];
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.Origin = new Point16(1, 4);
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width - 1, 0);
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = true;

			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width - 1, 1);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			
			TileObjectData.addTile(Type);
			AddMapEntry(new Color(123, 134, 145));
		}
		
		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) {
			return settings.player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance); // Avoid being able to trigger it from long range
		}

		public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info) {
			// It is very important to know that this is called on both players and NPCs, so do not use Main.LocalPlayer for example, use info.restingEntity
			Tile tile = Framing.GetTileSafely(i, j);
			
			int top = j - tile.TileFrameY / 16 % 5;
			int left = i - tile.TileFrameX / 16 % 4;
			
			info.TargetDirection = -1;
			if (tile.TileFrameX != 18) 
				info.TargetDirection = 1; // Facing right if sat down on the right alternate (added through addAlternate in SetStaticDefaults earlier)

			if (tile.TileFrameX != 18)
				info.VisualOffset.X += 10;
			else
				info.VisualOffset.X += 2;
			
			info.AnchorTilePosition.X =  left + 1;
			info.AnchorTilePosition.Y = top + 4;
			info.VisualOffset.Y = info.RestingEntity is Player ? -8 : -16;
		}

		public override bool RightClick(int i, int j) {
			
			Tile tile = Framing.GetTileSafely(i, j);
			
			Player player = Main.LocalPlayer;

			if (player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance) && (tile.TileFrameX == 18 || tile.TileFrameX == 90)) { // Avoid being able to trigger it from long range
				player.GamepadEnableGrappleCooldown();
				player.sitting.SitDown(player, i, j);
			} 
			return true;
		}
		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.cursorItemIconID = ModContent.ItemType<Items.Placeable.LaboratoryItems.LabBigToilet>();
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Utilities.SimpleGlowmask(i, j, spriteBatch, Texture);
		}
	}
}
