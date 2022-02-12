namespace GbHapticsIntegration.Setup.Weapons
{
    internal class W_Warhammer : I_WeaponBase
    {
        internal W_Warhammer() : base(WeaponType.Warhammer)
            => Setup("Weapons\\Warhammer",
                use_blunt2: true);
    }
}