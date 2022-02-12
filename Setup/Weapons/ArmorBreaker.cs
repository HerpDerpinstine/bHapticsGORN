namespace GbHapticsIntegration.Setup.Weapons
{
    internal class W_ArmorBreaker : I_WeaponBase
    {
        internal W_ArmorBreaker() : base(WeaponType.ArmorBreaker)
            => Setup("Weapons\\ArmorBreaker",
                use_blunt2: true);
    }
}