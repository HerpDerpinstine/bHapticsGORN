namespace GbHapticsIntegration.Setup.Weapons
{
    internal class W_Boulder : I_WeaponBase
    {
        internal W_Boulder() : base(WeaponType.Boulder)
            => Setup("Weapons\\Boulder",
                use_blunt2: true);
    }
}