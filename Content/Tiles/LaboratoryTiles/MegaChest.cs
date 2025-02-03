using System;
using CalamityMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Common.Utilities;
using ProtoMod.Content.Items;
using ProtoMod.Content.NPC.Bosses.Protector;
using ProtoMod.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ProtoMod.Content.Tiles.LaboratoryTiles
{
	public class MegaChest : ModTile
	{
		public override string Texture => "ProtoMod/Content/Tiles/LaboratoryTiles/MegaChestAtlas";
		
		public override void SetStaticDefaults() 
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileContainer[Type] = true;
			TileID.Sets.BasicChest[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileID.Sets.AvoidedByNPCs[Type] = true;
			TileID.Sets.InteractibleByNPCs[Type] = true;
			TileID.Sets.IsAContainer[Type] = true;
			TileID.Sets.FriendlyFairyCanLureTo[Type] = true;

			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(Chest.FindEmptyChest, -1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(Chest.AfterPlacement_Hook, -1, 0, false);

			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Width = 7;
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, };
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.AnchorInvalidTiles = new int[] {
				TileID.MagicalIceBlock,
				TileID.Boulder,
				TileID.BouncyBoulder,
				TileID.LifeCrystalBoulder,
				TileID.RollingCactus
			};
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);

			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleHorizontal = true;
	        
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, TileObjectData.newTile.Width, 0);
			TileObjectData.newSubTile.AnchorWall = true;
			TileObjectData.addSubTile(1);
	        
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.addSubTile(2);
	        
			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, TileObjectData.newTile.Width, 0);
			TileObjectData.newSubTile.AnchorWall = true;
			TileObjectData.addSubTile(3);
			
			
			TileObjectData.addTile(Type);
			
			AddMapEntry(new Color(88,94,107));
			
		}
		
		public override LocalizedText DefaultContainerName(int frameX, int frameY) => this.GetLocalization("MapEntry");
		
		
        public override void MouseOver(int i, int j)
        {
	        Player player = Main.LocalPlayer;
	        Tile tile = Main.tile[i, j];
	        string chestName = TileLoader.DefaultContainerName(tile.TileType, tile.TileFrameX, tile.TileFrameY);
	        int left = i;
	        int top = j;
	        if (tile.TileFrameX % 36 != 0)
	        {
		        left--;
	        }
	        if (tile.TileFrameY != 0)
	        {
		        top--;
	        }
	        int chest = Chest.FindChest(left, top);
	        player.cursorItemIconID = -1;
	        if (chest < 0)
	        {
		        player.cursorItemIconText = Language.GetTextValue("LegacyChestType.0");
	        }
	        else
	        {
		        player.cursorItemIconText = Main.chest[chest].name.Length > 0 ? Main.chest[chest].name : chestName;
		        if (player.cursorItemIconText == chestName)
		        {
			        player.cursorItemIconID = ModContent.ItemType<Items.Placeable.LaboratoryItems.MegaChest>();
			        player.cursorItemIconText = "";
		        }
	        }
	        player.noThrow = 2;
	        player.cursorItemIconEnabled = true;
        }
        
        public override void MouseOverFar(int i, int j)
        {
	        Player player = Main.LocalPlayer;
	        Tile tile = Main.tile[i, j];
	        string chestName = TileLoader.DefaultContainerName(tile.TileType, tile.TileFrameX, tile.TileFrameY);
	        int left = i;
	        int top = j;
	        if (tile.TileFrameX % 36 != 0)
	        {
		        left--;
	        }
	        if (tile.TileFrameY != 0)
	        {
		        top--;
	        }
	        int chest = Chest.FindChest(left, top);
	        player.cursorItemIconID = -1;
	        if (chest < 0)
	        {
		        player.cursorItemIconText = Language.GetTextValue("LegacyChestType.0");
	        }
	        else
	        {
		        player.cursorItemIconText = Main.chest[chest].name.Length > 0 ? Main.chest[chest].name : chestName;
		        if (player.cursorItemIconText == chestName)
		        {
			        player.cursorItemIconID = ModContent.ItemType<Items.Placeable.LaboratoryItems.MegaChest>();
			        player.cursorItemIconText = "";
		        }
	        }
	        player.noThrow = 2;
	        player.cursorItemIconEnabled = true;
	        if (player.cursorItemIconText == "")
	        {
		        player.cursorItemIconEnabled = false;
		        player.cursorItemIconID = 0;
	        }
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY) => Chest.DestroyChest(i, j);
        
        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Tile tile = Main.tile[i, j];
            Main.mouseRightRelease = false;
            int left = i;
            int top = j;
            if (tile.TileFrameX % 126 != 0)
            {
                left--;
            }
            if (tile.TileFrameY != 54)
            {
                top--;
            }
            if (player.sign >= 0)
            {
                SoundEngine.PlaySound(SoundID.MenuClose);
                player.sign = -1;
                Main.editSign = false;
                Main.npcChatText = "";
            }
            if (Main.editChest)
            {
                SoundEngine.PlaySound(SoundID.MenuTick);
                Main.editChest = false;
                Main.npcChatText = "";
            }
            if (player.editedChestName)
            {
                NetMessage.SendData(MessageID.SyncPlayerChest, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f, 0f, 0f, 0, 0, 0);
                player.editedChestName = false;
            }

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                if (left == player.chestX && top == player.chestY && player.chest >= 0)
                {
                    player.chest = -1;
                    Recipe.FindRecipes();
                    SoundEngine.PlaySound(SoundID.MenuClose);
                }
                else
                {
                    NetMessage.SendData(MessageID.RequestChestOpen, -1, -1, null, left, (float)top, 0f, 0f, 0, 0, 0);
                    Main.stackSplit = 600;
                }
            }
            else
            {
                int chest = Chest.FindChest(left, top);
                if (chest >= 0)
                {
                    Main.stackSplit = 600;
                    if (chest == player.chest)
                    {
                        player.chest = -1;
                        SoundEngine.PlaySound(SoundID.MenuClose);
                    }
                    else
                    {
                        player.chest = chest;
                        Main.playerInventory = true;
                        Main.recBigList = false;
                        player.chestX = left;
                        player.chestY = top;
                        SoundEngine.PlaySound(player.chest < 0 ? SoundID.MenuOpen : SoundID.MenuTick);
                    }
                    Recipe.FindRecipes();
                }
            }
            return true;
        }
        
		
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			int xFrameOffset = Main.tile[i, j].TileFrameX;
			int yFrameOffset = Main.tile[i, j].TileFrameY;
			Texture2D glowmask = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
			Vector2 drawOffest = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
			Vector2 drawPosition = new Vector2(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y) + drawOffest;
			Color drawColour = Color.White;
			Tile trackTile = Main.tile[i, j];
			if (!trackTile.IsHalfBlock && trackTile.Slope == 0)
				spriteBatch.Draw(glowmask, drawPosition, new Rectangle(xFrameOffset, yFrameOffset, 16, 16), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
			else if (trackTile.IsHalfBlock)
				spriteBatch.Draw(glowmask, drawPosition + new Vector2(0f, 8f), new Rectangle(xFrameOffset, yFrameOffset, 18, 8), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
		}
		
		
	}

	
	
}
