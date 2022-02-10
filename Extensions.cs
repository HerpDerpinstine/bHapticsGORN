using UnityEngine;
using MelonLoader;

namespace GbHapticsIntegration
{
    internal static class Extensions
    {
        internal static float MultiplyVelocity(this Vector3 val, float multiplier, float min, float max)
            => Mathf.Clamp(val.magnitude * multiplier, min, max);

        internal static bHaptics.ScaleOption ToNewScaleOption(this float val)
            => new bHaptics.ScaleOption(val);

        internal static bHaptics.ScaleOption GetVelocityScaleOption(this Vector3 velocity, float multiplier, float min, float max)
            => velocity.MultiplyVelocity(multiplier, min, max).ToNewScaleOption();
    }
}