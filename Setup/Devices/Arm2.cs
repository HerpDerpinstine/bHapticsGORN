using System.IO;
using MelonLoader.Preferences;
using GbHapticsIntegration.Setup.ConfigModels;

namespace GbHapticsIntegration.Setup.Devices
{
    internal class I_Arm<T, L> : TactFile where T : CM_Toggle, new() where L : new()
    {
        private bool IsLeft;
        private MelonPreferences_ReflectiveCategory GeneralCategory;
        private MelonPreferences_ReflectiveCategory VelocityScalingCategory;
        internal T General;
        internal L VelocityScaling;

        internal I_Arm(bool is_left, string basefolder, string className)
        {
            IsLeft = is_left;
            string ArmName = $"Arm{(IsLeft ? "L" : "R")}";
            basefolder = Path.Combine(basefolder, ArmName);

            General = Config.SetupCategory<T>(ref GeneralCategory, "General", basefolder, className);
            VelocityScaling = Config.SetupCategory<L>(ref VelocityScalingCategory, "VelocityScaling", basefolder, className);

            Register(
                $"{basefolder.Replace("\\", "/").Replace("/", "_")}_{className}", 
                $"{className}.tact", 
                basefolder,
                $"{className}_{ArmName}");
        }

        internal bool IsEnabled()
            => (IsLeft
            ? Config.HapticDevices.ArmL
            : Config.HapticDevices.ArmR)
            && General.Enabled;
    }
}