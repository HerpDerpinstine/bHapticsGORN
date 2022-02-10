using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Weapons
{
    internal class W_Axe : I_WeaponBase
    {
        internal W_Axe() : base(WeaponType.Axe)
            => Setup("Weapons\\Axe",
                use_blunt2: true,
                use_stab: true,
                use_cut: true);
    }
}