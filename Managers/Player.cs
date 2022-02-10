using GbHapticsIntegration.Hooks;
using UnityEngine;
using MelonLoader;

namespace GbHapticsIntegration.Managers
{
    internal static class M_Player
    {
        internal static void OnHeartBeat()
        {
            if (!Config.General.Allow_Player_Communication)
                return;
            M_Tact.heartBeat.Play();
        }

        internal static void HitSomething(DamagerRigidbody damager, Collision collision)
        {
            if (!Config.General.Allow_Player_Communication)
                return;

            GrabHand grabHand = damager.weaponBase.grabbedByHand;
            if (!grabHand.isPlayer
                || (grabHand.ownerFist == null))
                return;

            Vector3 velocity = collision.relativeVelocity - (Vector3)H_DamagerRigidbody.DamagerRigidbody_velocity.GetValue(damager);

            if (damager.isPlayerFist)
                M_Enemy.OnFistDamage(grabHand.ownerFist, velocity, DamageType.Blunt);
            else
                M_Enemy.OnWeaponDamage(damager.weaponBase, velocity, DamageType.Blunt, collision);
        }

        internal static bHaptics.RotationOption ContactToHapticRotation(Vector3 contactPos, Collider targetCollider)
            => ContactToHapticRotation(contactPos, targetCollider.transform.position, targetCollider.transform.forward, targetCollider.bounds.size.y);

        internal static bHaptics.RotationOption ContactToHapticRotation(Vector3 contactPos, Vector3 targetPos, Vector3 targetForward, float targetHeight)
        {
            Vector3 targetDir = contactPos - targetPos;
            float angle = Angle(targetDir, targetForward);
            float offsetY = (contactPos.y - targetPos.y) / targetHeight;
            return new bHaptics.RotationOption(angle, offsetY);
        }

        private static float Angle(Vector3 fwd, Vector3 targetDir)
        {
            var fwd2d = new Vector3(fwd.x, 0, fwd.z);
            var targetDir2d = new Vector3(targetDir.x, 0, targetDir.z);
            float angle = Vector3.Angle(fwd2d, targetDir2d);
            if (AngleDir(fwd, targetDir, Vector3.up) == -1)
            {
                angle = 360.0f - angle;
                if (angle > 359.9999f)
                    angle -= 360.0f;
                return angle;
            }
            return angle;
        }

        private static int AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
        {
            Vector3 perp = Vector3.Cross(fwd, targetDir);
            float dir = Vector3.Dot(perp, up);
            if (dir > 0.0)
                return 1;
            if (dir < 0.0)
                return -1;
            return 0;
        }
    }
}