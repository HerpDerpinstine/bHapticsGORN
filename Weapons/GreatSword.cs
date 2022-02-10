using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Weapons
{
    internal class W_GreatSword : I_WeaponBase
    {
        internal W_GreatSword() : base(WeaponType.GreatSword)
            => Setup("Weapons\\GreatSword",
                use_blunt2: true,
                use_stab: true,
                use_cut: true);
    }
}