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

		internal void PlaySoft()
		{
			if (!Config.HapticEffects.Gong)
				return;

			if (Vest.IsEnabled())
				Vest.Play();
		}

		internal void PlayMedium()
		{
			if (!Config.HapticEffects.Gong)
				return;

			if (Vest.IsEnabled())
				Vest.Play();
		}

		internal void PlayHard()
		{
			if (!Config.HapticEffects.Gong)
				return;

			if (Vest.IsEnabled())
				Vest.Play();
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