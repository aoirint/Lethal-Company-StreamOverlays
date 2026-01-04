using com.aoirint.StreamOverlaysHqolPatched.Managers;
using HarmonyLib;
using Unity.Netcode;

namespace com.aoirint.StreamOverlaysHqolPatched.Patches;

[HarmonyPatch(typeof(NetworkManager))]
internal static class NetworkManagerPatch
{
    [HarmonyPatch("Initialize")]
    [HarmonyPostfix]
    private static void InitializePatch()
    {
        PluginNetworkManager.Initialize();
    }
}
