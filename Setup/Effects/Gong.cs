using GbHapticsIntegration.Setup.Devices;
using GbHapticsIntegration.Setup.ConfigModels;
using MelonLoader;

namespace GbHapticsIntegration.Setup.Effects
{
	internal class E_Gong : I_EffectBase
	{
		internal I_Vest<CM_Intensity2> Vest;

		internal E_Gong()
		{
			string className = "Gong";

			Vest = new I_Vest<CM_Intensity2>("Player", className);
		}

		internal enum Strength
        {
			Hard,
			Medium,
			Soft
        }
		internal void Play(Strength strength)
		{
			if (!Config.HapticEffects.Gong)
				return;

			if (Vest.IsEnabled())
			{
				switch (strength)
                {
					case Strength.Soft:
						Vest.Play(new bHaptics.ScaleOption(Vest.General.IntensityScale_Soft));
						break;
					case Strength.Medium:
						Vest.Play(new bHaptics.ScaleOption(Vest.General.IntensityScale_Medium));
						break;
					default:
						Vest.Play(new bHaptics.ScaleOption(Vest.General.IntensityScale_Hard));
						break;
				}
			}
		}

		internal override bool IsPlaying(bHaptics.PositionType positionType)
		{
			switch (positionType)
			{
				case bHaptics.PositionType.Vest:
					return Vest.IsPlaying();
				default:
					return false;
			}
		}
	}
}