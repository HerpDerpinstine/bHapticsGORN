using System.IO;
using MelonLoader.Preferences;
using GbHapticsIntegration.Setup.ConfigModels;

namespace GbHapticsIntegration.Setup.Devices
{
    internal class I_Hand<T> : TactFile where T : CM_Toggle, new()
    {
        private bool IsLeft;
        private MelonPreferences_ReflectiveCategory GeneralCategory;
        internal T General;

        internal I_Hand(bool is_left, string basefolder, string className)
        {
            IsLeft = is_left;
            string handName = $"Hand{(IsLeft ? "L" : "R")}";
            basefolder = Path.Combine(basefolder, handName);

            General = Config.SetupCategory<T>(ref GeneralCategory, "General", basefolder, className);

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
