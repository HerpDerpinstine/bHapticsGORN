namespace GbHapticsIntegration.Setup.Weapons
{
    internal class W_Gun : I_WeaponBase
    {
        internal W_Gun() : base(WeaponType.Gun)
            => Setup("Weapons\\Gun",
                use_blunt2: true,
                use_shoot: true);
    }
}