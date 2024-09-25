using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ProtoMod.Content.Pets.LitosObliterator
{
    public class LitosObliteratorDustBlue : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.4f; // Multiply the dust's start velocity by 0.4, slowing it down
            dust.noGravity = true; // Makes the dust have no gravity.
            dust.scale *= 1.5f; // Multiplies the dust's initial scale by 1.5.
            dust.alpha = 0;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return Color.Aqua;
        }

        public override bool Update(Dust dust)
        {
            // Calls every frame the dust is active
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.15f;
            dust.scale -= 0.15f;

            // float light = 0.35f * dust.scale;
            //
            // Lighting.AddLight(dust.position, light, light, light);

            if (dust.scale < 0.1f)
            {
                dust.active = false;
            }

            return false; // Return false to prevent vanilla behavior.
        }
    }
}