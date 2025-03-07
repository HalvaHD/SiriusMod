using SiriusMod.Content.Dusts;
using Microsoft.Xna.Framework;
using SiriusMod.Mechanics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Items.Tools.PreHM.PathfinderPickaxe
{
    public class PathfinderPickaxe : Overheat
    {
        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 9;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.pick = 59;
            Item.attackSpeedOnlyAffectsWeaponAnimation = true;
        }
        
        public override void MeleeEffects(Player player, Rectangle hitbox) {
            if (Main.rand.NextBool(10)) {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<LabMossDust>());
            }
        }
        
        
    }
}
