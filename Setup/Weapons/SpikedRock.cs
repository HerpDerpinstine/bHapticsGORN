namespace GbHapticsIntegration.Setup.Weapons
{
    internal class W_SpikedRock : I_WeaponBase
    {
        internal W_SpikedRock() : base(WeaponType.SpikedRock)
            => Setup("Weapons\\SpikedRock",
                use_blunt2: true);
    }
}