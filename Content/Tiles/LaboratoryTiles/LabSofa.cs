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
	public class LabSofa : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			TileID.Sets.CanBeSatOnForPlayers[Type] = true; 
			TileID.Sets.DisableSmartCursor[Type] = true;
			Main.tileLavaDeath[Type] = false;
			Main.tileWaterDeath[Type] = false;
			
			AdjTiles = new int[] { TileID.Chairs };
			AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.Sofa"));
			
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.CoordinateHeights = [16, 16];
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.Origin = new Point16(1, 1);
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.LavaDeath = true;
			
			
			TileObjectData.addTile(Type);
			AddMapEntry(new Color(123, 134, 145));
		}
		
		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) {
			return settings.player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance); 
		}
		
		public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info) {
			Tile tile = Framing.GetTileSafely(i, j);
			
			info.TargetDirection = -1;
			if (tile.TileFrameX != 0) {
				info.TargetDirection = 1; 
			}
			
			info.AnchorTilePosition.Y = j;
			if (tile.TileFrameY % NextStyleHeight == 0) {
				info.AnchorTilePosition.Y++; 
			}
		}

		public int NextStyleHeight { get; set; }

		public override bool RightClick(int i, int j) {
			
			Tile tile = Framing.GetTileSafely(i, j);
			
			Player player = Main.LocalPlayer;

			if (player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance) && (tile.TileFrameX == 11 || tile.TileFrameX == 54)) { 
				player.GamepadEnableGrappleCooldown();
				player.sitting.SitDown(player, i, j);
			} 
			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;

			if (!player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance))
			{
				return;
			}

			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = ModContent.ItemType<Items.Placeable.LaboratoryItems.LabSofa>();

			if (Main.tile[i, j].TileFrameX / 18 < 1)
			{
				player.cursorItemIconReversed = true;
			}
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Utilities.SimpleGlowmask(i, j, spriteBatch, Texture);
		}
	}
}
