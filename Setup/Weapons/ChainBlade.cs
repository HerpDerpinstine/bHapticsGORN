using UnityEngine;

namespace GbHapticsIntegration.Setup.Weapons
{
    internal class W_ChainBlade : I_WeaponBase
    {
        internal W_ChainBlade() : base(WeaponType.ChainBlade)
            => Setup("Weapons\\ChainBlade",
                use_blunt2: true,
                use_stab: true,
                use_cut: true);

        internal override Fist Parse2HandedDamageFist(WeaponBase weaponBase, Collision collision)
        {
            TwoHandedWeaponBase twoHandedWeaponBase = (TwoHandedWeaponBase)weaponBase;
            return Parse2HandedDamageFist(collision,
                    twoHandedWeaponBase.frontGrip.grabbedBy,
                    twoHandedWeaponBase.grabbable.grabbedBy,
                    twoHandedWeaponBase.frontGrip.gameObject.GetComponent<Collider>(),
                    twoHandedWeaponBase.transform.Find("Blade")?.GetComponent<Collider>());
        }
    }
}