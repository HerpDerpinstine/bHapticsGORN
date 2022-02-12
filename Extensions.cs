using UnityEngine;
using MelonLoader;

namespace GbHapticsIntegration
{
    internal static class Extensions
    {
        internal static float MultiplyVelocity(this Vector3 val, float multiplier, float min, float max)
            => Mathf.Clamp(val.magnitude * multiplier, min, max);

        internal static bHaptics.ScaleOption ToNewScaleOption(this float val)
            => new bHaptics.ScaleOption(val);

        internal static bHaptics.ScaleOption GetVelocityScaleOption(this Vector3 velocity, float multiplier, float min, float max)
            => velocity.MultiplyVelocity(multiplier, min, max).ToNewScaleOption();

        internal static void GetBodyForwardPos(this Player _this, out Vector3 newposition, out Vector3 newforward, out float height)
        {
            CapsuleCollider collider = _this.damageRelay.transform.GetComponent<CapsuleCollider>();
            height = collider.bounds.size.y;

            // To-Do: Replace this with a proper solution. It works but this is a very hacky way to do it.
            Vector3 oldposition = collider.transform.localPosition;
            collider.transform.localPosition = new Vector3(oldposition.x + collider.center.x, oldposition.y + collider.center.y, oldposition.z + collider.center.z);
            newposition = collider.transform.position;
            newforward = collider.transform.forward;
            collider.transform.localPosition = oldposition;
        }
    }
}