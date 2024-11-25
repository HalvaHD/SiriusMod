using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Content.Dusts;
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
    public class LabTV : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Width = 4;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.LavaPlacement = LiquidPlacement.Allowed; 
            TileObjectData.addTile(Type);

            AnimationFrameHeight = 30;
            
            AddMapEntry(new Color(71, 95, 114));
        }
        
        public override void NumDust(int i, int j, bool fail, ref int num) => num = 1;

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frame = 0;
        }


        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            int yOffset = TileObjectData.GetTileData(tile).DrawYOffset;
            Texture2D glowmask = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Vector2 drawOffset = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);
            Vector2 drawPosition = new Vector2(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y + yOffset) + drawOffset;
            Color drawColour = Color.White;
            Main.spriteBatch.Draw(glowmask, drawPosition, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), drawColour, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
