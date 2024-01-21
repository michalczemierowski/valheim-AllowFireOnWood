using BepInEx;
using HarmonyLib;

namespace AllowFireOnWood
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private Harmony harmony;

        private void Awake()
        {
            harmony = new Harmony("main");
            harmony.PatchAll(typeof(Plugin));
        }

        [HarmonyPatch(typeof(PieceTable), nameof(PieceTable.GetSelectedPiece))]
        [HarmonyPostfix]
        private static void GetSelectedPieceSuffix(ref Piece __result)
        {
            if (__result == null)
                return;

            if (__result.m_notOnWood && __result.m_comfortGroup == Piece.ComfortGroup.Fire)
            {
                __result.m_notOnWood = false;
            }
        }
    }
}