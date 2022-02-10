using System;
using HarmonyLib;
using UnityEngine;
using GbHapticsIntegration.Managers;
using System.Reflection;

namespace GbHapticsIntegration.Hooks
{
    internal static class H_DamageRelay
    {
        internal static AccessTools.FieldRef<DamagerRigidbody, Vector3> DamagerRigidbody_velocity = null;
        internal static MethodInfo DamageRelay_Damage = null;

        internal static void Initialize()
        {
            Debug.LogFieldRefGet("DamagerRigidbody.velocity");
            DamagerRigidbody_velocity = AccessTools.FieldRefAccess<DamagerRigidbody, Vector3>("velocity");
            Debug.LogFieldRefFound("DamagerRigidbody.velocity", DamagerRigidbody_velocity);

            Debug.LogPatchInit("DamageRelay.Damage");
            Type Type_DamageRelay = typeof(DamageRelay);
            Type Type_DamageRelay_Hook = typeof(H_DamageRelay);
            DamageRelay_Damage = AccessTools.Method(Type_DamageRelay, "Damage");
            GbHapticsIntegration.ModHarmony.Patch(DamageRelay_Damage,
                null,
                new HarmonyMethod(AccessTools.Method(Type_DamageRelay_Hook, "Damage_Postfix")));
        }

        private static void Damage_Postfix(DamageRelay __instance,
            DamageType __0, // type
            GameObject __4, // sender
            Collision __6, // collision
            AITargetable __7, // responsibleEntity
            bool __8 // isFloor
        )
        {
            if (!Config.General.Allow_Player_Communication)
                return;

            // Check for Valid Damage Types
            if (__0 == DamageType.Bleed)
                return;

            // Check if This Relay is Player Owned
            if ((__instance.owner == null)
                || __instance.owner.IsPlayer())
                return;

            // Check if Responsible Entity is Player and is Game Controller
            if ((__7 == null) 
                || !__7.IsPlayer() 
                || (GameController.IsPartyMode
                && ((Player)__7 != GameController.Player)))
                return;

            // Check for Valid DamagerRigidbody
            DamagerRigidbody damager = __4.GetComponent<DamagerRigidbody>(); 
            if (damager == null)
                return;

            // If Damager Is Floor
            if (__8)
            {
                // Check if Enemy is being Held by Player
                M_Enemy.OnFloorDamage();
                return;
            }

            // If Damager is Fist
            if (damager.isPlayerFist)
            {
                Fist fistComp = damager.GetComponentInParent<Fist>();
                if (fistComp != null)
                    M_Enemy.OnFistDamage(fistComp, (__6.relativeVelocity - DamagerRigidbody_velocity(damager)), __0);
                return;
            }

            // Check that Damager is Weapon and is Currently being Held
            if ((damager.weaponBase == null)
                || !damager.weaponBase.beingWielded
                || !damager.weaponBase.grabbedByHand.isPlayer)
                return;

            M_Enemy.OnWeaponDamage(damager.weaponBase, (__6.relativeVelocity - DamagerRigidbody_velocity(damager)), __0, __6);
        }
    }
}