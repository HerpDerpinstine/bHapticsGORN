using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Weapons.Caestus
{
    internal class W_Wings : I_WeaponBase
    {
        internal W_Wings() : base(CaestusType.Mobility)
            => Setup("Weapons\\Caestus\\Wings",
                use_blunt: true,
                use_wobble: true);
    }
}