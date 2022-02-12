namespace GbHapticsIntegration.Setup.Weapons.Caestus
{
    internal class W_Crossbow : I_WeaponBase
    {
        internal W_Crossbow() : base(CaestusType.Crossbow)
             => Setup("Weapons\\Caestus\\Crossbow",
                 use_blunt2: true,
                 use_shootString: true);
    }
}
