using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Weapons
{
    internal class W_Halberd : I_WeaponBase
    {
        internal W_Halberd() : base(WeaponType.Halberd)
            => Setup("Weapons\\Halberd",
                use_blunt2: true,
                use_stab: true,
                use_cut: true);
    }
}