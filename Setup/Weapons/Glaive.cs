namespace GbHapticsIntegration.Setup.Weapons
{
    internal class W_Glaive : I_WeaponBase
    {
        internal W_Glaive() : base(WeaponType.Glaive)
            => Setup("Weapons\\Glaive",
                use_blunt2: true,
                use_stab: true,
                use_cut: true);
    }
}