using com.aoirint.StreamOverlaysHqolPatched.Managers;
using com.aoirint.StreamOverlaysHqolPatched.Server;
using HarmonyLib;

namespace com.aoirint.StreamOverlaysHqolPatched.Patches;

[HarmonyPatch(typeof(DepositItemsDesk))]
internal static class DepositItemsDeskPatch
{
    [HarmonyPatch(nameof(DepositItemsDesk.SellAndDisplayItemProfits))]
    [HarmonyPostfix]
    private static void SellAndDisplayItemProfitsPatch()
    {
        if (LootManager.CanUpdateLootTotal())
        {
            LootManager.UpdateLootTotal();
            WebServer.UpdateOverlaysData();
        }
    }
}
