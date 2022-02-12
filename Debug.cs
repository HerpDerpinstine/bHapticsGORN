#if DEBUG
using System;
using MelonLoader;
#endif

namespace GbHapticsIntegration
{
    internal static class Debug
    {
#if DEBUG
        private static bool Config_Register = false;
        internal static bool Config_Load = false;
        internal static bool Config_PrintMsg = false;
        private static bool TactFile_Register = false;
        private static bool TactFile_Playback = true;
        private static bool FieldRef_Get = true;
        private static bool FieldRef_Found = true;
        private static bool Method_Get = true;
        private static bool Method_Found = true;
        private static bool Patch_Init = true;
        private static bool Damage_Fist = false;
        private static bool Damage_Weapon = false;

        internal static void LogConfigRegister(string identifier, string filepath)
        {
            if (!Config_Register)
                return;

            GbHapticsIntegration.Logger.Msg($"Registered ReflectiveCategory [{identifier}] to [{filepath}]");
        }

        internal static void LogTactFileRegister(string identifier, string filepath, bool is_file)
        {
            if (!TactFile_Register)
                return;

            GbHapticsIntegration.Logger.Msg($"Registered bHaptics Pattern [{identifier}] from {(is_file ? $"TactFile [{filepath}]" : "Default Resources")}");
        }

        internal static void LogTactFilePlayback(string identifier, bHaptics.ScaleOption scaleOption = null, bHaptics.RotationOption rotationOption = null)
        {
            if (!TactFile_Playback)
                return;

            GbHapticsIntegration.Logger.Msg($"Submitted bHaptics Pattern [{identifier}]   |   Scale:  [{((scaleOption == null) ? 1f : scaleOption.Intensity)}] / [{((scaleOption == null) ? 1f : scaleOption.Duration)}]   |   Rotation:  [{((rotationOption == null) ? 1f : rotationOption.OffsetX)}] / [{((rotationOption == null) ? 1f : rotationOption.OffsetY)}]");
        }
#endif

        internal static void LogFieldRefGet(string identifier)
        {
#if DEBUG
            if (!FieldRef_Get)
                return;
#endif

            GbHapticsIntegration.Logger.Msg($"Getting FieldRef {identifier}...");
        }

        internal static void LogFieldRefFound(string identifier, object obj)
        {
#if DEBUG
            if (!FieldRef_Found)
                return;
#endif

            if (obj == null)
            {
                GbHapticsIntegration.Logger.Error($"Failed to Find FieldRef {identifier}!");
                return;
            }

            GbHapticsIntegration.Logger.Msg($"Found FieldRef {identifier}!");
        }

        internal static void LogMethodGet(string identifier)
        {
#if DEBUG
            if (!Method_Get)
                return;
#endif

            GbHapticsIntegration.Logger.Msg($"Getting Method {identifier}...");
        }

        internal static void LogMethodFound(string identifier, object obj)
        {
#if DEBUG
            if (!Method_Found)
                return;
#endif

            if (obj == null)
            {
                GbHapticsIntegration.Logger.Error($"Failed to Find Method {identifier}!");
                return;
            }

            GbHapticsIntegration.Logger.Msg($"Found Method {identifier}!");
        }

        internal static void LogPatchInit(string identifier)
        {
#if DEBUG
            if (!Patch_Init)
                return;
#endif

            GbHapticsIntegration.Logger.Msg($"Patching Method {identifier}...");
        }

#if DEBUG
        internal static void LogDamageFist(CaestusType caestusType, DamageType damageType, bool is_left)
        {
            if (!Damage_Fist)
                return;

            GbHapticsIntegration.Logger.Msg($"OnFistDamage\n" +
                $"is_left = {is_left}\n" +
                $"caestusType = {Enum.GetName(typeof(CaestusType), caestusType)}\n" +
                $"damageType = {Enum.GetName(typeof(DamageType), damageType)}");
        }

        internal static void LogDamageWeapon(WeaponType weaponType, DamageType damageType, bool is_left)
        {
            if (!Damage_Weapon)
                return;

            GbHapticsIntegration.Logger.Msg($"OnWeaponDamage\n" +
                $"is_left = {is_left}\n" +
                $"weaponType = {Enum.GetName(typeof(WeaponType), weaponType)}\n" +
                $"damageType = {Enum.GetName(typeof(DamageType), damageType)}");
        }
#endif
    }
}
