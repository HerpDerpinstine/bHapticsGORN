namespace GbHapticsIntegration.Setup.Weapons.Caestus
{
    internal class W_GrapplingHook : I_WeaponBase
    {
        internal W_GrapplingHook() : base(CaestusType.Grapple)
             => Setup("Weapons\\Caestus\\GrapplingHook",
                 use_blunt2: true,
                 use_shoot: true);
    }
}
