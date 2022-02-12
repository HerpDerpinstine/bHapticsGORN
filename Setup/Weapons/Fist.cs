namespace GbHapticsIntegration.Setup.Weapons
{
    internal class W_Fist : I_WeaponBase
    {
        internal W_Fist()
            => Setup("Fist",
                use_wobble: true,
                use_blunt: true);
    }
}