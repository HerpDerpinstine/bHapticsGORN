using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Weapons
{
    internal class W_Arrow : I_WeaponBase
    {
        internal W_Arrow() : base(WeaponType.Arrow)
            => Setup("Weapons\\Arrow", 
                use_blunt2: true, 
                use_stab: true, 
                use_cut: true);
    }
}