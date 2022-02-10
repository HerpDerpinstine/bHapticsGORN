using System.IO;
using MelonLoader;
using MelonLoader.Preferences;

namespace GbHapticsIntegration.Setup.Devices
{
    internal class I_Hand<T, L> : TactFile where T : I_GeneralValues, new() where L : new() 
    {
        private bool IsLeft;
        private MelonPreferences_ReflectiveCategory GeneralCategory;
        private MelonPreferences_ReflectiveCategory VelocityScalingCategory;
        internal T General;
        internal L VelocityScaling;

        internal I_Hand(bool is_left, string basefolder, string className)
        {
            IsLeft = is_left;
            string handName = $"Hand{(IsLeft ? "L" : "R")}";
            basefolder = Path.Combine(basefolder, handName);

            General = Config.SetupCategory<T>(ref GeneralCategory, "General", basefolder, className);
            VelocityScaling = Config.SetupCategory<L>(ref VelocityScalingCategory, "VelocityScaling", basefolder, className);

            Register(
                $"{basefolder.Replace("\\", "/").Replace("/", "_")}_{className}",
                $"{className}.tact",
                basefolder, 
                $"{className}_{handName}");
        }

        internal bool IsEnabled()
            => (IsLeft
            ? Config.HapticDevices.HandL
            : Config.HapticDevices.HandR)
            && General.Enabled;
    }
}