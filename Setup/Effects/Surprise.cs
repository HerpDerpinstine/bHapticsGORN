using GbHapticsIntegration.Setup.Devices;
using MelonLoader;
using GbHapticsIntegration.Setup.ConfigModels;

namespace GbHapticsIntegration.Setup.Effects
{
	internal class E_Surprise : I_EffectBase
	{
		internal I_Vest<CM_Intensity> Vest;

		internal E_Surprise()
		{
			string className = "Surprise";

			Vest = new I_Vest<CM_Intensity>("Player", className);
		}

		internal void Play()
		{
			if (!Config.HapticEffects.Surprise)
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