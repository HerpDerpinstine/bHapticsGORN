using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Weapons
{
    internal class W_Gladius : I_WeaponBase
    {
        internal W_Gladius() : base(WeaponType.Gladius)
            => Setup("Weapons\\Gladius",
                use_blunt2: true,
                use_stab: true,
                use_cut: true);
    }
}