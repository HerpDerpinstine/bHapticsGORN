using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Weapons
{
    internal class W_ThrowingShield : I_WeaponBase
    {
        internal W_ThrowingShield() : base(WeaponType.ThrowingShield)
            => Setup("Weapons\\ThrowingShield",
                use_blunt2: true,
                use_stab: true,
                use_cut: true);
    }
}