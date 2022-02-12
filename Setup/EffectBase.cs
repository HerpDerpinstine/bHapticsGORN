using MelonLoader;
using UnityEngine;
using GbHapticsIntegration.Setup.ConfigModels;

namespace GbHapticsIntegration.Setup
{
    internal abstract class I_EffectBase
    {
        internal I_WeaponBase WeaponBase;

        internal I_EffectBase() { }
        internal I_EffectBase(I_WeaponBase weaponBase)
            => WeaponBase = weaponBase;

        internal bHaptics.ScaleOption GetScaleOption(float magnitude, CM_Velocity velocityScalingValues)
            => GetScaleOption(magnitude, velocityScalingValues.Enabled, velocityScalingValues.Multiplier, velocityScalingValues.Min, velocityScalingValues.Max);
        internal bHaptics.ScaleOption GetScaleOption(float magnitude, bool enabled, float multiplier, float min, float max)
        {
            if (!enabled)
                return null;
            return Mathf.Clamp(magnitude * multiplier, min, max).ToNewScaleOption();
        }

        internal abstract bool IsPlaying(bHaptics.PositionType positionType);
    }
}
