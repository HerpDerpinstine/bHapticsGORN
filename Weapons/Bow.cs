using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Weapons
{
    internal class W_Bow : I_WeaponBase
    {
        internal W_Bow() : base(WeaponType.Bow)
            => Setup("Weapons\\Bow",
                use_blunt2: true,
                use_drawString: true,
                use_shootString: true);
    }
}
