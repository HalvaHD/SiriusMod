using System.Collections.Generic;
using ProtoMod.Content.Items;
using Terraria;
using Terraria.ModLoader;

namespace ProtoMod.Content.Pets.OldTwig
{
    internal class OldTwigPlayer : ModPlayer
    {
        public override IEnumerable<Terraria.Item> AddStartingItems(bool mediumCoreDeath)
        {
            return new[]
        {
            new Terraria.Item(ModContent.ItemType<Aut>()),
        };
        }
    }
}
