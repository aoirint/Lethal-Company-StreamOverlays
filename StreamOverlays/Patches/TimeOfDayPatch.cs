using com.aoirint.StreamOverlaysHqolPatched.Managers;
using com.aoirint.StreamOverlaysHqolPatched.Server;
using HarmonyLib;

namespace com.aoirint.StreamOverlaysHqolPatched.Patches;

[HarmonyPatch(typeof(TimeOfDay))]
internal static class TimeOfDayPatch
{
    [HarmonyPatch(nameof(TimeOfDay.SyncNewProfitQuotaClientRpc))]
    [HarmonyPostfix]
    private static void SyncNewProfitQuotaClientRpcPatch()
    {
        WebServer.UpdateOverlaysData();
    }

    [HarmonyPatch(nameof(TimeOfDay.UpdateProfitQuotaCurrentTime))]
    [HarmonyPostfix]
    private static void UpdateProfitQuotaCurrentTimePatch()
    {
        LootManager.UpdateLootTotal();
        WebServer.UpdateOverlaysData();
    }
}
