namespace GbHapticsIntegration.Setup.Weapons
{
    internal class W_Mace : I_WeaponBase
    {
        internal W_Mace() : base(WeaponType.Mace)
            => Setup("Weapons\\Mace",
                use_blunt2: true);
    }
}