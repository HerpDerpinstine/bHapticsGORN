using System;
using MelonLoader;

namespace GbHapticsIntegration
{
    internal static class Debug
    {
        private static bool Config_Register = true;
        private static bool Config_Register_Load = false;
        private static bool Config_Load = false;
        private static bool TactFile_Register = true;
        private static bool TactFile_Playback = true;
        private static bool FieldRef_Get = true;
        private static bool FieldRef_Found = true;
        private static bool Patch_Init = true;
        private static bool Damage_Fist = false;
        private static bool Damage_Weapon = false;

        internal static void LogConfigRegister(string identifier, string filepath)
        {
            if (!Config_Register)
                return;
            MelonDebug.Msg($"Registered ReflectiveCategory [{identifier}] to [{filepath}]");
        }

        internal static bool ShouldLogConfigRegisterLoad()
            => Config_Register_Load && MelonDebug.IsEnabled();

        internal static bool ShouldLoadExistingConfigs()
            => !MelonDebug.IsEnabled() || Config_Load;

        internal static void LogTactFileRegister(string identifier, string filepath, bool is_file)
        {
            if (!TactFile_Register)
                return;
            MelonDebug.Msg($"Registered bHaptics Pattern [{identifier}] from {(is_file ? $"TactFile [{filepath}]" : "Default Resources")}");
        }

        internal static void LogTactFilePlayback(string identifier, bHaptics.ScaleOption scaleOption = null, bHaptics.RotationOption rotationOption = null)
        {
            if (!TactFile_Playback)
                return;
            MelonDebug.Msg($"Submitted bHaptics Pattern [{identifier}]   |   Scale:  [{((scaleOption == null) ? 1f : scaleOption.Intensity)}] / [{((scaleOption == null) ? 1f : scaleOption.Duration)}]   |   Rotation:  [{((rotationOption == null) ? 1f : rotationOption.OffsetX)}] / [{((rotationOption == null) ? 1f : rotationOption.OffsetY)}]");
        }

        internal static void LogFieldRefGet(string identifier)
        {
            if (!FieldRef_Get)
                return;
            MelonDebug.Msg($"Getting FieldRef {identifier}...");
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
            MelonDebug.Msg($"Found FieldRef {identifier}!");
        }

        internal static void LogMethodGet(string identifier)
        {
            if (!FieldRef_Get)
                return;
            MelonDebug.Msg($"Getting Method {identifier}...");
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
            MelonDebug.Msg($"Found Method {identifier}!");
        }

        internal static void LogPatchInit(string identifier)
        {
            if (!Patch_Init)
                return;
            MelonDebug.Msg($"Patching Method {identifier}...");
        }

        internal static void LogDamageFist(CaestusType caestusType, DamageType damageType, bool is_left)
        {
            if (!Damage_Fist)
                return;
            MelonDebug.Msg($"OnFistDamage\n" +
                $"is_left = {is_left}\n" +
                $"caestusType = {Enum.GetName(typeof(CaestusType), caestusType)}\n" +
                $"damageType = {Enum.GetName(typeof(DamageType), damageType)}");
        }

        internal static void LogDamageWeapon(WeaponType weaponType, DamageType damageType, bool is_left)
        {
            if (!Damage_Weapon)
                return;
            MelonDebug.Msg($"OnWeaponDamage\n" +
                $"is_left = {is_left}\n" +
                $"weaponType = {Enum.GetName(typeof(WeaponType), weaponType)}\n" +
                $"damageType = {Enum.GetName(typeof(DamageType), damageType)}");
        }
    }
}
