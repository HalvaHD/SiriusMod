using ProtoMod.Common.ItemDropRules.DropConditions;
using ProtoMod.Content.Items;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ProtoMod.Common.GlobalNPCs
{
	// This file shows numerous examples of what you can do with the extensive NPC Loot lootable system.
	// You can find more info on the wiki: https://github.com/tModLoader/tModLoader/wiki/Basic-NPC-Drops-and-Loot-1.4
	// Despite this file being GlobalNPC, everything here can be used with a ModNPC as well! See examples of this in the Content/NPCs folder.
	public class GlobalNPCLoot : GlobalNPC
	{
		public override void OnKill(NPC npc)
		{
			if (npc.boss && !ProtoMod.CheckKilledBosses.Contains(npc.type))
			{	
				ProtoMod.CheckKilledBosses.Add(npc.type);
			}
		}
		// ModifyNPCLoot uses a unique system called the ItemDropDatabase, which has many different rules for many different drop use cases.
		// Here we go through all of them, and how they can be used.
		// There are tons of other examples in vanilla! In a decompiled vanilla build, GameContent/ItemDropRules/ItemDropDatabase adds item drops to every single vanilla NPC, which can be a good resource.

		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (npc.boss)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new FirstKillDropCondition(), ModContent.ItemType<CosmicCry>()));
				npcLoot.Add(ItemDropRule.ByCondition(new NextKillsDropCondition(), ModContent.ItemType<CosmicCry>(), 10));
			}

			if (npc.rarity == 4)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new FirstKillDropCondition(), ModContent.ItemType<CosmicCry>()));
			}
		}
	}
}
