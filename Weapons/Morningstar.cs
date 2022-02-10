using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Weapons
{
    internal class W_Morningstar : I_WeaponBase
    {
        internal W_Morningstar() : base(WeaponType.Morningstar)
            => Setup("Weapons\\Morningstar",
                use_blunt2: true);
    }
}