using System;
using Harmony;

namespace AlienRifle2
{
	// Token: 0x02000006 RID: 6
	[HarmonyPatch(typeof(PDAScanner))]
	[HarmonyPatch("Unlock")]
	public static class ScannerPatch
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000029D0 File Offset: 0x00000BD0
		public static bool Prefix(ref PDAScanner.EntryData entryData, ref bool unlockBlueprint)
		{
			bool flag = entryData.key == 4226;
			if (flag)
			{
				unlockBlueprint = true;
				entryData.blueprint = MainPatch.rifleTech;
				string text = "EncyDesc_PrecursorPrisonArtifact7";
				string str = Language.main.Get(text);
				bool flag2 = Language.main.currentLanguage == "English";
				if (flag2)
				{
					Language.main.strings[text] = str + "\n\nDespite this, it was possible to synthesise a blueprint for the rifle, but it will require a Stasis Rifle to use as a base.";
				}
			}
			return true;
		}
	}
}
