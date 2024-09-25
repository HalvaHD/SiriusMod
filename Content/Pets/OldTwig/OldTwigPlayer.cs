using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Twig.Content.Items;

namespace Twig.Content.Pets.OldTwig
{
    internal class OldTwigPlayer : ModPlayer
    {
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            return new[]
        {
            new Item(ModContent.ItemType<Aut>()),
        };
        }
    }
}
