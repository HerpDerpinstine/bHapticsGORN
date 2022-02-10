using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Weapons.Caestus
{
    internal class W_IronFist : I_WeaponBase
    {
        internal W_IronFist() : base(CaestusType.Punch)
            => Setup("Weapons\\Caestus\\IronFist",
                use_blunt3: true);
    }
}