using System;
using MelonLoader;

namespace GbHapticsIntegration
{
    internal static class Debug
    {
#if DEBUG
        private static bool Config_Register = true;
        internal static bool Config_Load = false;
        internal static bool Config_PrintMsg = false;
        private static bool TactFile_Register = true;
        private static bool TactFile_Playback = true;
        private static bool FieldRef_Get = true;
        private static bool FieldRef_Found = true;
        private static bool Patch_Init = true;
        private static bool Damage_Fist = false;
        private static bool Damage_Weapon = false;
#else
        private static bool Config_Register = false;
        internal static bool Config_Load = true;
        internal static bool Config_PrintMsg = false;
        private static bool TactFile_Register = false;
        private static bool TactFile_Playback = false;
        private static bool FieldRef_Get = true;
        private static bool FieldRef_Found = true;
        private static bool Patch_Init = true;
        private static bool Damage_Fist = false;
        private static bool Damage_Weapon = false;
#endif

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

        internal static void LogFieldRefGet(string identifier)
        {
            if (!FieldRef_Get)
                return;
            GbHapticsIntegration.Logger.Msg($"Getting FieldRef {identifier}...");
        }

        internal static void LogFieldRefFound(string identifier, object obj)
        {
            if (!FieldRef_Found)
                return;
            if (obj == null)
            {
                GbHapticsIntegration.Logger.Error($"Failed to Find FieldRef {identifier}!");
                return;
            }
            GbHapticsIntegration.Logger.Msg($"Found FieldRef {identifier}!");
        }

        internal static void LogMethodGet(string identifier)
        {
            if (!FieldRef_Get)
                return;
            GbHapticsIntegration.Logger.Msg($"Getting Method {identifier}...");
        }

        internal static void LogMethodFound(string identifier, object obj)
        {
            if (!FieldRef_Found)
                return;
            if (obj == null)
            {
                GbHapticsIntegration.Logger.Error($"Failed to Find Method {identifier}!");
                return;
            }
            GbHapticsIntegration.Logger.Msg($"Found Method {identifier}!");
        }

        internal static void LogPatchInit(string identifier)
        {
            if (!Patch_Init)
                return;
            GbHapticsIntegration.Logger.Msg($"Patching Method {identifier}...");
        }

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
    }
}
