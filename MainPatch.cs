using System;
using System.Collections.Generic;
using Harmony;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using UnityEngine;

namespace AlienRifle2
{
	// Token: 0x02000003 RID: 3
	public static class MainPatch
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002144 File Offset: 0x00000344
		public static void Patch()
		{
			MainPatch.bundle = AssetBundle.LoadFromFile("QMods/AlienRifle2/alienrifle");
			MainPatch.rifleTech = TechTypeHandler.AddTechType("AlienRifleWeapon", "Alien Rifle", "An ancient weapon found in an alien facility", ImageUtils.LoadSpriteFromFile("QMods/AlienRifle2/Assets/alienrifle.png", 25), false);
			PrefabHandler.RegisterPrefab(new RiflePrefab("AlienRifleWeapon", "WorldEntities/Tools/AlienRifle", MainPatch.rifleTech));
			CraftDataHandler.SetEquipmentType(MainPatch.rifleTech, 1);
			TechData techData = new TechData();
			techData.Ingredients = new List<Ingredient>
			{
				new Ingredient(755, 1),
				new Ingredient(54, 3),
				new Ingredient(41, 2),
				new Ingredient(66, 1)
			};
			techData.craftAmount = 1;
			CraftDataHandler.SetTechData(MainPatch.rifleTech, techData);
			CraftDataHandler.SetItemSize(MainPatch.rifleTech, 2, 2);
			CraftTreeHandler.AddTabNode(3, "StasisRifleMods", "Stasis Rifle Upgrades", ImageUtils.LoadSpriteFromFile("QMods/AlienRifle2/Assets/stasisrifleupgrades.png", 25));
			CraftTreeHandler.AddCraftingNode(3, MainPatch.rifleTech, new string[]
			{
				"StasisRifleMods",
				"Alien Rifle"
			});
			HarmonyInstance harmonyInstance = HarmonyInstance.Create("Kylinator25.AlienRifle.V2");
			harmonyInstance.PatchAll(typeof(MainPatch).Assembly);
		}

		// Token: 0x04000002 RID: 2
		public static TechType rifleTech;

		// Token: 0x04000003 RID: 3
		public static AssetBundle bundle;
	}
}
