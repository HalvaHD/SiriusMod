using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using SiriusMod.Mechanics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SiriusMod.Content.Items.Weapons;
using SiriusMod.Content.Items.Tools.PreHM.PathfinderPickaxe;
using SiriusMod.Core;
using Terraria.DataStructures;

namespace SiriusMod.Content.Items.Armor.PreHM.PathfinderArmor
{
    [AutoloadEquip(EquipType.Head)]
    public class PathfinderHelmMelee : ModItem, IHeadArmorGlowing
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 9;
        }
        
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<PathfinderBreastplate>() && legs.type == ModContent.ItemType<PathfinderLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            SiriusModPlayer siriusPlayer = player.GetModPlayer<SiriusModPlayer>();

            player.setBonus = this.GetLocalizedValue("SetBonus");
            player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
            
            siriusPlayer.MaxOverheat = 1500f;
            siriusPlayer.swordDMG = 60f;
        }
        
        public void DrawGlowmask(PlayerDrawSet drawInfo)
        {
            if (drawInfo.drawPlayer.invis)
                return;
            
            Player armorOwner = drawInfo.drawPlayer;
            Texture2D glowmaskTexture = ModContent.Request<Texture2D>(Texture + "_Head_Glow", AssetRequestMode.ImmediateLoad).Value;
            Vector2 weirdGravityOffset = new Vector2(0, armorOwner.gravDir == -1 ? 6 : 0);
            Vector2 drawPosition = armorOwner.MountedCenter - Main.screenPosition - new Vector2(0, 3 - armorOwner.gfxOffY) + weirdGravityOffset;
            Vector2 origin = new Vector2(armorOwner.bodyFrame.Width / 2, armorOwner.bodyFrame.Height / 2);

            DrawData drawData = new DrawData(glowmaskTexture, drawPosition, armorOwner.bodyFrame, Color.White, armorOwner.headRotation, origin, 1f, drawInfo.playerEffect)
            {
                shader = drawInfo.cHead
            };
            drawInfo.DrawDataCache.Add(drawData);
        }
    }
}
