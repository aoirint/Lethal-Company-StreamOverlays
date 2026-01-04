using BepInEx;
using BepInEx.Configuration;
using com.aoirint.StreamOverlaysHqolPatched.Dependencies;
using com.aoirint.StreamOverlaysHqolPatched.Dependencies.ShipInventoryProxy;
using com.aoirint.StreamOverlaysHqolPatched.Dependencies.Vanilla;
using com.aoirint.StreamOverlaysHqolPatched.Helpers;
using com.aoirint.StreamOverlaysHqolPatched.Managers;
using com.aoirint.StreamOverlaysHqolPatched.Patches;
using com.aoirint.StreamOverlaysHqolPatched.Server;
using HarmonyLib;
using System.Threading.Tasks;

namespace com.aoirint.StreamOverlaysHqolPatched;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(LethalConfigProxy.PLUGIN_GUID, BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency(ShipInventoryProxy.PLUGIN_GUID, BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency(HQoLProxy.PLUGIN_72_GUID, BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency(HQoLProxy.PLUGIN_73_GUID, BepInDependency.DependencyFlags.SoftDependency)]
internal class Plugin : BaseUnityPlugin
{
    private readonly Harmony _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

    internal static Plugin Instance { get; private set; }
    internal static new ConfigFile Config { get; private set; }

    #pragma warning disable IDE0051 // Remove unused private members
    private void Awake()
    #pragma warning restore IDE0051 // Remove unused private members
    {
        Instance = this;

        StreamOverlaysHqolPatched.Logger.Initialize(BepInEx.Logging.Logger.CreateLogSource(MyPluginInfo.PLUGIN_GUID));
        StreamOverlaysHqolPatched.Logger.LogInfo($"{MyPluginInfo.PLUGIN_NAME} has awoken!");

        Config = Utils.CreateGlobalConfigFile();

        _harmony.PatchAll(typeof(NetworkManagerPatch));
        _harmony.PatchAll(typeof(GameNetworkManagerPatch));
        _harmony.PatchAll(typeof(StartOfRoundPatch));
        _harmony.PatchAll(typeof(RoundManagerPatch));
        _harmony.PatchAll(typeof(TimeOfDayPatch));
        _harmony.PatchAll(typeof(PlayerControllerBPatch));
        _harmony.PatchAll(typeof(DepositItemsDeskPatch));

        if (VehicleControllerProxy.Enabled)
        {
            VehicleControllerProxy.PatchAll(_harmony);
        }

        if (ShipInventoryProxy.Enabled)
        {
            ShipInventoryProxy.PatchAll(_harmony);
        }

        ConfigManager.Initialize(Config);

        Task.Run(WebServer.Initialize);
    }
}
