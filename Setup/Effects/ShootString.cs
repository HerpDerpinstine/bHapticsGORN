using GbHapticsIntegration.Setup.Devices;
using MelonLoader;

namespace GbHapticsIntegration.Setup.Effects
{
    internal class E_ShootString : I_EffectBase
    {
        internal I_Hand<I_GeneralValues> HandL;
        internal I_Hand<I_GeneralValues> HandR;
        internal I_Arm<I_GeneralValues> ArmL;
        internal I_Arm<I_GeneralValues> ArmR;

        internal E_ShootString(I_WeaponBase weaponBase, string basefolder) : base(weaponBase)
        {
            string className = "ShootString";

            HandL = new I_Hand<I_GeneralValues>(true, basefolder, className);
            HandR = new I_Hand<I_GeneralValues>(false, basefolder, className);
            ArmL = new I_Arm<I_GeneralValues>(true, basefolder, className);
            ArmR = new I_Arm<I_GeneralValues>(false, basefolder, className);
        }

        internal void Play(bool is_left)
        {
            if (!Config.HapticEffects.ShootString)
                return;

            if (is_left)
            {
                if (HandL.IsEnabled())
                    HandL.Play(new bHaptics.ScaleOption(HandL.General.IntensityScale));
                if (ArmL.IsEnabled())
                    ArmL.Play(new bHaptics.ScaleOption(ArmL.General.IntensityScale));
                return;
            }

            if (HandR.IsEnabled())
                HandR.Play(new bHaptics.ScaleOption(HandR.General.IntensityScale));
            if (ArmR.IsEnabled())
                ArmR.Play(new bHaptics.ScaleOption(ArmR.General.IntensityScale));
        }

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