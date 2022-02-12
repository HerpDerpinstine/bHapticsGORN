using GbHapticsIntegration.Setup.Devices;
using MelonLoader;
using UnityEngine;
using GbHapticsIntegration.Setup.ConfigModels;
using Tomlet.Attributes;

namespace GbHapticsIntegration.Setup.Effects
{
    internal class E_Wobble : I_EffectBase
    {
        [TomlDoNotInlineObject]
        internal class CM_VelocityNew : CM_Velocity
        {
            public CM_VelocityNew()
                => Max = 1f;
        }
        internal I_Hand<CM_Intensity, CM_VelocityNew> HandL;
        internal I_Hand<CM_Intensity, CM_VelocityNew> HandR;

        [TomlDoNotInlineObject]
        internal class CM_VelocityNew2 : CM_Velocity
        {
            public CM_VelocityNew2()
            {
                Max = 1f;
                Multiplier = 0.0025f;
            }
        }
        internal I_Arm<CM_Intensity, CM_VelocityNew2> ArmL;
        internal I_Arm<CM_Intensity, CM_VelocityNew2> ArmR;

        internal E_Wobble(I_WeaponBase weaponBase, string basefolder) : base(weaponBase)
        {
            string className = "Wobble";

            HandL = new I_Hand<CM_Intensity, CM_VelocityNew>(true, basefolder, className);
            HandR = new I_Hand<CM_Intensity, CM_VelocityNew>(false, basefolder, className);
            ArmL = new I_Arm<CM_Intensity, CM_VelocityNew2>(true, basefolder, className);
            ArmR = new I_Arm<CM_Intensity, CM_VelocityNew2>(false, basefolder, className);
        }

        internal void Play(Vector3 velocity, bool is_left)
        {
            if (!Config.HapticEffects.Wobble)
                return;

            if (is_left)
            {
                if (HandL.IsEnabled()
                    && !WeaponBase.IsPlaying_Blunt(bHaptics.PositionType.HandL)
                    && !WeaponBase.IsPlaying_Blunt2(bHaptics.PositionType.HandL)
                    && !WeaponBase.IsPlaying_Blunt3(bHaptics.PositionType.HandL)
                    && !WeaponBase.IsPlaying_Cut(bHaptics.PositionType.HandL)
                    && !WeaponBase.IsPlaying_DrawString(bHaptics.PositionType.HandL)
                    && !WeaponBase.IsPlaying_Shoot(bHaptics.PositionType.HandL)
                    && !WeaponBase.IsPlaying_ShootString(bHaptics.PositionType.HandL)
                    && !WeaponBase.IsPlaying_Stab(bHaptics.PositionType.HandL))
                {
                    bHaptics.ScaleOption scaleOption = GetScaleOption(velocity.magnitude, HandL.VelocityScaling);
                    scaleOption.Intensity *= HandL.General.IntensityScale;
                    HandL.Play(scaleOption);
                }

                if (ArmL.IsEnabled()
                    && !WeaponBase.IsPlaying_Blunt(bHaptics.PositionType.ForearmL)
                    && !WeaponBase.IsPlaying_Blunt2(bHaptics.PositionType.ForearmL)
                    && !WeaponBase.IsPlaying_Blunt3(bHaptics.PositionType.ForearmL)
                    && !WeaponBase.IsPlaying_Cut(bHaptics.PositionType.ForearmL)
                    && !WeaponBase.IsPlaying_DrawString(bHaptics.PositionType.ForearmL)
                    && !WeaponBase.IsPlaying_Shoot(bHaptics.PositionType.ForearmL)
                    && !WeaponBase.IsPlaying_ShootString(bHaptics.PositionType.ForearmL)
                    && !WeaponBase.IsPlaying_Stab(bHaptics.PositionType.ForearmL))
                {
                    bHaptics.ScaleOption scaleOption = GetScaleOption(velocity.magnitude, ArmL.VelocityScaling);
                    scaleOption.Intensity *= ArmL.General.IntensityScale;
                    ArmL.Play(scaleOption);
                }

                return;
            }

            if (HandR.IsEnabled()
                    && !WeaponBase.IsPlaying_Blunt(bHaptics.PositionType.HandR)
                    && !WeaponBase.IsPlaying_Blunt2(bHaptics.PositionType.HandR)
                    && !WeaponBase.IsPlaying_Blunt3(bHaptics.PositionType.HandR)
                    && !WeaponBase.IsPlaying_Cut(bHaptics.PositionType.HandR)
                    && !WeaponBase.IsPlaying_DrawString(bHaptics.PositionType.HandR)
                    && !WeaponBase.IsPlaying_Shoot(bHaptics.PositionType.HandR)
                    && !WeaponBase.IsPlaying_ShootString(bHaptics.PositionType.HandR)
                    && !WeaponBase.IsPlaying_Stab(bHaptics.PositionType.HandR))
            {
                bHaptics.ScaleOption scaleOption = GetScaleOption(velocity.magnitude, HandR.VelocityScaling);
                scaleOption.Intensity *= HandR.General.IntensityScale;
                HandR.Play(scaleOption);
            }

            if (ArmR.IsEnabled()
                    && !WeaponBase.IsPlaying_Blunt(bHaptics.PositionType.ForearmR)
                    && !WeaponBase.IsPlaying_Blunt2(bHaptics.PositionType.ForearmR)
                    && !WeaponBase.IsPlaying_Blunt3(bHaptics.PositionType.ForearmR)
                    && !WeaponBase.IsPlaying_Cut(bHaptics.PositionType.ForearmR)
                    && !WeaponBase.IsPlaying_DrawString(bHaptics.PositionType.ForearmR)
                    && !WeaponBase.IsPlaying_Shoot(bHaptics.PositionType.ForearmR)
                    && !WeaponBase.IsPlaying_ShootString(bHaptics.PositionType.ForearmR)
                    && !WeaponBase.IsPlaying_Stab(bHaptics.PositionType.ForearmR))
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