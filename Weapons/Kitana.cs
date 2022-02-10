using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Weapons
{
    internal class W_Kitana : I_WeaponBase
    {
        internal W_Kitana() : base(WeaponType.Kitana)
            => Setup("Weapons\\Kitana",
                use_blunt2: true,
                use_stab: true,
                use_cut: true);
    }
}