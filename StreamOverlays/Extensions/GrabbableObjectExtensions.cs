using HarmonyLib;
using System.Reflection;

namespace com.aoirint.StreamOverlaysHqolPatched.Extensions;

internal static class GrabbableObjectExtensions
{
    private static readonly FieldInfo _deactivatedField = AccessTools.Field(typeof(GrabbableObject), "deactivated");

    public static bool IsDeactivated(this GrabbableObject grabbableObject)
    {
        return _deactivatedField?.GetValue(grabbableObject) as bool? ?? false;
    }
}
