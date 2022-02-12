namespace GbHapticsIntegration.Setup.Weapons
{
    internal class W_Sword : I_WeaponBase
    {
        internal W_Sword() : base(WeaponType.Sword)
            => Setup("Weapons\\Sword",
                use_blunt2: true,
                use_stab: true,
                use_cut: true);
    }
}