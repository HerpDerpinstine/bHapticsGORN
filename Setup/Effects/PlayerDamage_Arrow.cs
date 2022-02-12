using UnityEngine;
using MelonLoader;
using GbHapticsIntegration.Setup.Devices;
using GbHapticsIntegration.Managers;
using GbHapticsIntegration.Setup.ConfigModels;
using Tomlet.Attributes;

namespace GbHapticsIntegration.Setup.Effects
{
	internal class E_PlayerDamage_Arrow : I_EffectBase
	{
		[TomlDoNotInlineObject]
		internal class CM_VelocityNew : CM_Velocity
		{
			public CM_VelocityNew()
				=> Multiplier = 0.01f;
		}
		internal I_Vest<CM_Intensity, CM_VelocityNew> Vest;

		internal E_PlayerDamage_Arrow()
		{
			string className = "PlayerDamage_Arrow";

			Vest = new I_Vest<CM_Intensity, CM_VelocityNew>("Player", className);
		}

		internal void Play(Vector3 contactPos, Vector3 velocity)
		{
			if (!Config.HapticEffects.PlayerDamage_Arrow)
				return;

			if (Vest.IsEnabled())
			{
				GameController.Player.GetBodyForwardPos(out Vector3 newposition, out Vector3 newforward, out float height);
				Vest.Play(GetScaleOption(velocity.magnitude, Vest.VelocityScaling),
					M_Player.ContactToHapticRotation(contactPos,
					newposition,
					newforward,
					height));
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