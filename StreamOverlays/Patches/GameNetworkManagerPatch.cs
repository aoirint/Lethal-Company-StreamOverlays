using com.github.zehsteam.StreamOverlays.Server;
using HarmonyLib;

namespace com.github.zehsteam.StreamOverlays.Patches;

[HarmonyPatch(typeof(GameNetworkManager))]
internal static class GameNetworkManagerPatch
{
    [HarmonyPatch(nameof(GameNetworkManager.SaveGame))]
    [HarmonyPostfix]
    private static void SaveGamePatch()
    {
        // True if disconnected before saving (save/load retry)
        bool isDisconnectionSave = StartOfRound.Instance == null || !StartOfRound.Instance.inShipPhase;
        if (! isDisconnectionSave)
        {
            DayManager.SaveDayData();
        }

        LootManager.UpdateLootTotal();
        WebServer.UpdateOverlaysData();
    }

    [HarmonyPatch(nameof(GameNetworkManager.ResetSavedGameValues))]
    [HarmonyPostfix]
    private static void ResetSavedGameValuesPatch()
    {
        DayManager.ResetSavedDayData();
        WebServer.UpdateOverlaysData();
    }
}
