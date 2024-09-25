using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items
{
    internal class TwigGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item entity)
        {
            if (entity.type is ItemID.GlommerPetItem or ItemID.BerniePetItem or ItemID.ParrotCracker or ItemID.UnluckyYarn or ItemID.DogWhistle or ItemID.CompanionCube or ItemID.DD2PetGato or ItemID.BambooLeaf or ItemID.BallOfFuseWire or ItemID.ExoticEasternChewToy or ItemID.EucaluptusSap or ItemID.PigPetItem or ItemID.LightningCarrot or ItemID.BirdieRattle or ItemID.DirtiestBlock or ItemID.SpiderEgg or ItemID.BlueEgg)
            { entity.value = Item.buyPrice(0, 5, 0, 0); }
        }
    }
}
