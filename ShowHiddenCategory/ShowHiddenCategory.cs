using HarmonyLib;
using NeosModLoader;
using FrooxEngine;
using System.Collections.Generic;
using System.Reflection.Emit;
using System;
namespace ShowHiddenCategory
{
	public class ShowHiddenCategory : NeosMod
	{
		public override string Name => "ShowHiddenCategory";
		public override string Author => "eia485";
		public override string Version => "1.0.0";
		public override string Link => "https://github.com/EIA485/NeosShowHiddenCategory/";
		public override void OnEngineInit()
		{
			Harmony harmony = new Harmony("net.eia485.ShowHiddenCategory");
			harmony.PatchAll();
		}

		[HarmonyPatch(typeof(WorkerInitializer), "Initialize", new Type[]{ typeof(List<Type>) })]
		class ShowHiddenCategoryPatch
		{
			static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
			{
				var codes = new List<CodeInstruction>(instructions);
				for (int i = 0; i < codes.Count; i++)
				{
					if (codes[i].opcode.OperandType == OperandType.InlineString & codes[i].operand?.ToString() == "Hidden")
					{
						for (int si = i - 1; si <i+3; si++)
						{
							codes[si].opcode = OpCodes.Nop;
						}
						break;
						
					}
				}
				return codes;
			}
		}
	}
}