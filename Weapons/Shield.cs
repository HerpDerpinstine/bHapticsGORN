using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Weapons
{
    internal class W_Shield : I_WeaponBase
    {
        internal W_Shield() : base(WeaponType.Shield)
            => Setup("Weapons\\Shield",
                use_blunt2: true);
    }
}