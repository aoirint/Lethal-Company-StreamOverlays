using com.aoirint.StreamOverlaysHqolPatched.Dependencies.Vanilla.Patches;
using com.aoirint.StreamOverlaysHqolPatched.Helpers;
using HarmonyLib;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace com.aoirint.StreamOverlaysHqolPatched.Dependencies.Vanilla;

internal static class VehicleControllerProxy
{
    public static bool Enabled
    {
        get
        {
            _enabled ??= GetEnabledState();
            return _enabled.Value;
        }
    }

    private static bool? _enabled;

    private static bool GetEnabledState()
    {
        try
        {
            Assembly assembly = typeof(StartOfRound).Assembly;
            return assembly.GetType("VehicleController") != null;
        }
        catch (Exception ex)
        {
            Logger.LogError($"Failed to get enabled state from VehicleControllerProxy. {ex}");
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void PatchAll(Harmony harmony)
    {
        try
        {
            harmony.PatchAll(typeof(VehicleControllerPatch));
        }
        catch (Exception ex)
        {
            Logger.LogError($"Failed to apply VehicleController patch. {ex}");
        }
    }
    
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static int GetLootTotal()
    {
        if (StartOfRound.Instance == null)
        {
            return 0;
        }

        try
        {
            VehicleController vehicleController = StartOfRound.Instance.attachedVehicle;
            if (vehicleController == null) return 0;

            GrabbableObject[] grabbableObjects = vehicleController.GetComponentsInChildren<GrabbableObject>();

            return grabbableObjects.Where(Utils.IsValidScrapAndNotHeld).Sum(x => x.scrapValue);
        }
        catch (Exception ex)
        {
            Logger.LogError($"Failed to get loot total from attached vehicle. {ex}");
            return 0;
        }
    }
}
