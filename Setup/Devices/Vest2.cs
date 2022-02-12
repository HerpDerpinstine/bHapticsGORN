using System.IO;
using MelonLoader.Preferences;
using GbHapticsIntegration.Setup.ConfigModels;

namespace GbHapticsIntegration.Setup.Devices
{
    internal class I_Vest<T, L> : TactFile where T : CM_Toggle, new() where L : new()
    {
        private MelonPreferences_ReflectiveCategory GeneralCategory;
        private MelonPreferences_ReflectiveCategory VelocityScalingCategory;
        internal T General;
        internal L VelocityScaling;
        internal I_Vest(string basefolder, string className)
        {
            string vestName = "Vest";
            basefolder = Path.Combine(basefolder, vestName);

            General = Config.SetupCategory<T>(ref GeneralCategory, "General", basefolder, className);
            VelocityScaling = Config.SetupCategory<L>(ref VelocityScalingCategory, "VelocityScaling", basefolder, className);

            Register(
                $"{basefolder.Replace("\\", "/").Replace("/", "_")}_{className}", 
                $"{className}.tact", 
                basefolder,
                $"{className}_{vestName}");
        }

        internal bool IsEnabled()
            => (Config.HapticDevices.VestFront
            && Config.HapticDevices.VestBack
            && General.Enabled);
    }
}
