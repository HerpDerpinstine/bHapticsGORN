using GbHapticsIntegration.Setup.Devices;
using GbHapticsIntegration.Managers;
using MelonLoader;

namespace GbHapticsIntegration.Setup.Effects
{
	internal class E_HeartBeat : I_EffectBase
	{
		internal I_Head<I_GeneralValues> Head;
		internal I_Vest<I_GeneralValues> Vest;

		internal E_HeartBeat()
		{
			string className = "HeartBeat";

			Head = new I_Head<I_GeneralValues>("Player", className);
			Vest = new I_Vest<I_GeneralValues>("Player", className);
		}

		internal void Play()
		{
			if (!Config.HapticEffects.HeartBeat)
				return;

			if (Head.IsEnabled())
				Head.Play(new bHaptics.ScaleOption(Head.General.IntensityScale));

			if (Vest.IsEnabled() && !M_Tact.playerDamage.IsPlaying(bHaptics.PositionType.Vest))
				Vest.Play(new bHaptics.ScaleOption(Vest.General.IntensityScale));
		}

		internal override bool IsPlaying(bHaptics.PositionType positionType)
		{
			switch (positionType)
			{
				case bHaptics.PositionType.Head:
					return Head.IsPlaying();
				case bHaptics.PositionType.Vest:
					return Vest.IsPlaying();
				default:
					return false;
			}
		}
	}
}