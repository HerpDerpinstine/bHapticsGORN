using UnityEngine;
using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Managers
{
    internal static class M_Enemy
    {
        internal static void OnFloorDamage()
        {

        }

        internal static void OnFistDamage(Fist fist, Vector3 velocity, DamageType damageType)
        {
#if DEBUG
            Debug.LogDamageFist(fist.caestusType, damageType, fist.left);
#endif

            switch (damageType)
            {
                case DamageType.Fist:
                case DamageType.Blunt:
                    if (fist.caestusType == CaestusType.None)
                        M_Tact.fist.OnBlunt(velocity, fist.left);
                    else if(M_Tact.WeaponsByCaestusType.TryGetValue(fist.caestusType, out I_WeaponBase weapon_blunt))
                        weapon_blunt.OnBlunt(velocity, fist.left);
                    goto default;

                case DamageType.Stab:
                    if ((fist.caestusType != CaestusType.None)
                        && M_Tact.WeaponsByCaestusType.TryGetValue(fist.caestusType, out I_WeaponBase weapon_stab))
                        weapon_stab.OnStab(velocity, fist.left);
                    goto default; 

                case DamageType.Cut:
                    if ((fist.caestusType != CaestusType.None)
                        && M_Tact.WeaponsByCaestusType.TryGetValue(fist.caestusType, out I_WeaponBase weapon_cut))
                        weapon_cut.OnCut(velocity, fist.left);
                    goto default;

                default:
                    break;
            }
        }

        internal static void OnWeaponDamage(WeaponBase weaponBase, Vector3 velocity, DamageType damageType, Collision collision)
        {
            WeaponType weaponType = weaponBase.type;
            if (!M_Tact.WeaponsByType.TryGetValue(weaponType, out I_WeaponBase weapon))
                return;

            Fist fist = weaponBase.grabbedByHand.ownerFist;
            if (weaponBase.IsTwoHanded && (collision != null))
            {
                fist = weapon.Parse2HandedDamageFist(weaponBase, collision);
                if (fist == null)
                    return;
            }

#if DEBUG
            Debug.LogDamageWeapon(weaponType, damageType, fist.left);
#endif

            switch (damageType)
            {
                case DamageType.Fist:
                case DamageType.Blunt:
                    weapon.OnBlunt(velocity, fist.left);
                    goto default;

                case DamageType.Stab:
                    weapon.OnStab(velocity, fist.left);
                    goto default;

                case DamageType.Cut:
                    weapon.OnCut(velocity, fist.left);
                    goto default;

                default:
                    break;
            }
        }
    }
}
