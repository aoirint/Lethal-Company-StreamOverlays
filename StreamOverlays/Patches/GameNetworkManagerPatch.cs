using com.aoirint.StreamOverlaysHqolPatched.Managers;
using com.aoirint.StreamOverlaysHqolPatched.Server;
using HarmonyLib;

namespace com.aoirint.StreamOverlaysHqolPatched.Patches;

[HarmonyPatch(typeof(GameNetworkManager))]
internal static class GameNetworkManagerPatch
{
    [HarmonyPatch(nameof(GameNetworkManager.SaveGame))]
    [HarmonyPostfix]
    private static void SaveGamePatch()
    {
        DayManager.SaveDayData();
        LootManager.UpdateLootTotal();
        WebServer.UpdateOverlaysData();
    }

    [HarmonyPatch(nameof(GameNetworkManager.ResetSavedGameValues))]
    [HarmonyPostfix]
    private static void ResetSavedGameValuesPatch()
    {
        DayManager.ResetSavedDayData();
        WebServer.UpdateOverlaysData();

        Logger.LogInfo("Reset saved data.", extended: true);
    }
}
