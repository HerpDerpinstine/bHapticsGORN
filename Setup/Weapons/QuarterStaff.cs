namespace GbHapticsIntegration.Setup.Weapons
{
    internal class W_QuarterStaff : I_WeaponBase
    {
        internal W_QuarterStaff() : base(WeaponType.QuarterStaff)
            => Setup("Weapons\\QuarterStaff",
                use_blunt2: true);
    }
}