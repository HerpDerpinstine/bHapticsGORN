namespace GbHapticsIntegration.Setup.Weapons
{
    internal class W_Spear : I_WeaponBase
    {
        internal W_Spear() : base(WeaponType.Spear)
            => Setup("Weapons\\Spear",
                use_blunt2: true,
                use_stab: true,
                use_cut: true);
    }
}