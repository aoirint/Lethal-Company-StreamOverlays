using com.aoirint.StreamOverlaysHqolPatched.Managers;
using com.aoirint.StreamOverlaysHqolPatched.Server;
using HarmonyLib;
using ShipInventory.Items;

namespace com.aoirint.StreamOverlaysHqolPatched.Dependencies.ShipInventoryProxy.Patches;

[HarmonyPatch(typeof(ItemManager))]
internal static class ItemManagerPatch
{
    [HarmonyPatch(nameof(ItemManager.UpdateCache))]
    [HarmonyPostfix]
    private static void UpdateCachePatch()
    {
        if (LootManager.CanUpdateLootTotal())
        {
            LootManager.UpdateLootTotal();
            WebServer.UpdateOverlaysData();
        }
    }
}
