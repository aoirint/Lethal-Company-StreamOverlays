using com.aoirint.StreamOverlaysHqolPatched.Managers;
using com.aoirint.StreamOverlaysHqolPatched.Server;
using HarmonyLib;
using ShipInventory.Objects;

namespace com.aoirint.StreamOverlaysHqolPatched.Dependencies.ShipInventoryProxy.Patches;

[HarmonyPatch(typeof(ChuteInteract))]
internal static class ChuteInteractPatch
{
    [HarmonyPatch(nameof(ChuteInteract.SpawnItemClientRpc))]
    [HarmonyPostfix]
    private static void SpawnItemClientRpcPatch()
    {
        if (LootManager.CanUpdateLootTotal())
        {
            LootManager.UpdateLootTotal();
            WebServer.UpdateOverlaysData();
        }
    }
}
