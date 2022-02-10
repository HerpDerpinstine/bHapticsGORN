using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Weapons
{
    internal class W_ThrowingKnife : I_WeaponBase
    {
        internal W_ThrowingKnife() : base(WeaponType.ThrowingKnife, CaestusType.ThrowingKnife)
            => Setup("Weapons\\ThrowingKnife",
                use_blunt2: true,
                use_stab: true,
                use_cut: true);
    }
}