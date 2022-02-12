namespace GbHapticsIntegration.Setup.Weapons.Caestus
{
    internal class W_CrabClaws : I_WeaponBase
    {
        internal W_CrabClaws() : base(CaestusType.CrabClaw)
            => Setup("Weapons\\Caestus\\CrabClaws",
                use_blunt2: true,
                use_stab: true,
                use_cut: true);
    }
}