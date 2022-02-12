using UnityEngine;
using MelonLoader;
using GbHapticsIntegration.Setup.Devices;
using GbHapticsIntegration.Managers;
using GbHapticsIntegration.Setup.ConfigModels;

namespace GbHapticsIntegration.Setup.Effects
{
	internal class E_PlayerDamage : I_EffectBase
	{
		internal class I_VelocityScalingValues
		{
			internal bool Enabled = true;
			internal float Min = 0f;
			internal float Max = 2f;
			internal float Multiplier = 0.01f;
		}
		internal I_Vest<CM_Intensity, I_VelocityScalingValues> Vest;

		internal E_PlayerDamage()
		{
			string className = "PlayerDamage";

			Vest = new I_Vest<CM_Intensity, I_VelocityScalingValues>("Player", className);
		}

		internal void Play(Vector3 contactPos, Vector3 velocity)
		{
			if (!Config.HapticEffects.PlayerDamage)
				return;

			if (Vest.IsEnabled())
			{
				CapsuleCollider collider = GameController.Player.damageRelay.transform.GetComponent<CapsuleCollider>();

				// To-Do: Replace this with a proper solution. It works but this is a very hacky way to do it.
				Vector3 oldposition = collider.transform.localPosition;
				collider.transform.localPosition = new Vector3(collider.transform.localPosition.x + collider.center.x, collider.transform.localPosition.y + collider.center.y, collider.transform.localPosition.z + collider.center.z);

				Vector3 newposition = collider.transform.position;

				Quaternion oldrotation = collider.transform.rotation;
				collider.transform.rotation = Quaternion.Euler(oldrotation.eulerAngles.x, GameController.Player.eyeCamera.transform.eulerAngles.y, oldrotation.eulerAngles.z);

				Vector3 newforward = collider.transform.forward;
				collider.transform.localPosition = oldposition;
				collider.transform.rotation = oldrotation;

				Vest.Play(GetScaleOption(velocity.magnitude, Vest.VelocityScaling),
					M_Player.ContactToHapticRotation(contactPos,
					newposition,
					newforward,
					collider.bounds.size.y));
			}
		}


		private bHaptics.ScaleOption GetScaleOption(float magnitude, I_VelocityScalingValues velocityScalingValues)
			=> GetScaleOption(magnitude, velocityScalingValues.Enabled, velocityScalingValues.Multiplier, velocityScalingValues.Min, velocityScalingValues.Max);

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