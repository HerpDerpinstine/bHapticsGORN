using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Weapons
{
    internal class W_GreatAxe : I_WeaponBase
    {
        internal W_GreatAxe() : base(WeaponType.GreatAxe)
            => Setup("Weapons\\GreatAxe",
                use_blunt2: true,
                use_stab: true,
                use_cut: true);
    }
}