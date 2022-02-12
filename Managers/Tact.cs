using System.Collections.Generic;
using GbHapticsIntegration.Setup;
using GbHapticsIntegration.Setup.Effects;
using GbHapticsIntegration.Setup.Weapons;
using GbHapticsIntegration.Setup.Weapons.Caestus;

namespace GbHapticsIntegration.Managers
{
    internal static class M_Tact
    {
        internal static Dictionary<WeaponType, I_WeaponBase> WeaponsByType = new Dictionary<WeaponType, I_WeaponBase>();
        internal static Dictionary<CaestusType, I_WeaponBase> WeaponsByCaestusType = new Dictionary<CaestusType, I_WeaponBase>();
        internal static E_HeartBeat heartBeat;
        internal static W_Fist fist;
        internal static E_Surprise surprise;
        internal static E_Gong gong;
        internal static E_PlayerDamage playerDamage;
        internal static E_PlayerDamage_Arrow playerDamage_Arrow;

        internal static void Setup()
        {
            heartBeat = new E_HeartBeat();
            fist = new W_Fist();
            surprise = new E_Surprise();
            gong = new E_Gong();

            playerDamage = new E_PlayerDamage();
            playerDamage_Arrow = new E_PlayerDamage_Arrow();

            // Normal
            new W_ArmorBreaker();
            new W_Arrow();
            new W_Axe();
            new W_Boulder();
            new W_Bow();
            new W_ChainBlade();
            new W_GiantFlail();
            new W_Gladius();
            new W_Glaive();
            new W_GreatAxe();
            new W_GreatSword();
            new W_Gun();
            new W_Halberd();
            new W_Kitana();
            new W_Mace();
            new W_Morningstar();
            new W_Nunchucks();
            new W_QuarterStaff();
            new W_Shield();
            new W_Spear();
            new W_SpikedRock();
            new W_Sword();
            new W_ThrowingShield();
            new W_Warhammer();

            // Caestus
            new W_Claws();
            new W_CrabClaws();
            new W_Crossbow();
            new W_GrapplingHook();
            new W_IronFist();
            new W_ThrowingKnife();
            new W_Wings();
        }
    }
}
