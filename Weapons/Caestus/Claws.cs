using GbHapticsIntegration.Setup;

namespace GbHapticsIntegration.Weapons.Caestus
{
    internal class W_Claws : I_WeaponBase
    {
        internal W_Claws() : base(CaestusType.Wolverine)
            => Setup("Weapons\\Caestus\\Claws",
                use_blunt2: true,
                use_stab: true,
                use_cut: true);
    }
}