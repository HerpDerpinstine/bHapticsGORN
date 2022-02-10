using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Weapons
{
    internal class W_Nunchucks : I_WeaponBase
    {
        internal W_Nunchucks() : base(WeaponType.Nunchucks)
            => Setup("Weapons\\Nunchucks",
                use_blunt2: true);
    }
}