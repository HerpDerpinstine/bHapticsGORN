using GbHapticsIntegration.Setup.Devices;
using MelonLoader;
using UnityEngine;

namespace GbHapticsIntegration.Setup.Effects
{
    internal class E_Cut : I_EffectBase
    {
        internal class I_VelocityScalingValues
        {
            internal bool Enabled = true;
            internal float Min = 0f;
            internal float Max = 2f;
            internal float Multiplier = 0.0075f;
        }
        internal I_Hand<I_GeneralValues, I_VelocityScalingValues> HandL;
        internal I_Hand<I_GeneralValues, I_VelocityScalingValues> HandR;

        internal class I_VelocityScalingValues2
        {
            internal bool Enabled = true;
            internal float Min = 0f;
            internal float Max = 2f;
            internal float Multiplier = 0.00375f;
        }
        internal I_Arm<I_GeneralValues, I_VelocityScalingValues2> ArmL;
        internal I_Arm<I_GeneralValues, I_VelocityScalingValues2> ArmR;

        internal E_Cut(I_WeaponBase weaponBase, string basefolder) : base(weaponBase)
        {
            string className = "Cut";

            HandL = new I_Hand<I_GeneralValues, I_VelocityScalingValues>(true, basefolder, className);
            HandR = new I_Hand<I_GeneralValues, I_VelocityScalingValues>(false, basefolder, className);
            ArmL = new I_Arm<I_GeneralValues, I_VelocityScalingValues2>(true, basefolder, className);
            ArmR = new I_Arm<I_GeneralValues, I_VelocityScalingValues2>(false, basefolder, className);
        }

        internal void Play(Vector3 velocity, bool is_left)
        {
            if (!Config.HapticEffects.Cut)
                return;

            if (is_left)
            {
                if (HandL.IsEnabled())
                {
                    bHaptics.ScaleOption scaleOption = GetScaleOption(velocity.magnitude, HandL.VelocityScaling);
                    scaleOption.Intensity *= HandL.General.IntensityScale;
                    HandL.Play(scaleOption);
                }

                if (ArmL.IsEnabled())
                {
                    bHaptics.ScaleOption scaleOption = GetScaleOption(velocity.magnitude, ArmL.VelocityScaling);
                    scaleOption.Intensity *= ArmL.General.IntensityScale;
                    ArmL.Play(scaleOption);
                }

                return;
            }

            if (HandR.IsEnabled())
            {
                bHaptics.ScaleOption scaleOption = GetScaleOption(velocity.magnitude, HandR.VelocityScaling);
                scaleOption.Intensity *= HandR.General.IntensityScale;
                HandR.Play(scaleOption);
            }

            if (ArmR.IsEnabled())
            {
                bHaptics.ScaleOption scaleOption = GetScaleOption(velocity.magnitude, ArmR.VelocityScaling);
                scaleOption.Intensity *= ArmR.General.IntensityScale;
                ArmR.Play(scaleOption);
            }
        }

        private bHaptics.ScaleOption GetScaleOption(float magnitude, I_VelocityScalingValues velocityScalingValues)
            => GetScaleOption(magnitude, velocityScalingValues.Enabled, velocityScalingValues.Multiplier, velocityScalingValues.Min, velocityScalingValues.Max);
        private bHaptics.ScaleOption GetScaleOption(float magnitude, I_VelocityScalingValues2 velocityScalingValues)
            => GetScaleOption(magnitude, velocityScalingValues.Enabled, velocityScalingValues.Multiplier, velocityScalingValues.Min, velocityScalingValues.Max);

        internal override bool IsPlaying(bHaptics.PositionType positionType)
        {
            switch (positionType)
            {
                case bHaptics.PositionType.HandL:
                    return HandL.IsPlaying();
                case bHaptics.PositionType.ForearmL:
                    return ArmL.IsPlaying();
                case bHaptics.PositionType.HandR:
                    return HandR.IsPlaying();
                case bHaptics.PositionType.ForearmR:
                    return ArmR.IsPlaying();
                default:
                    return false;
            }
        }
    }
}