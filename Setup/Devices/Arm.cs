using System.IO;
using MelonLoader;
using MelonLoader.Preferences;

namespace GbHapticsIntegration.Setup.Devices
{
    internal class I_Arm<T> : TactFile where T : I_GeneralValues, new()
    {
        private bool IsLeft;
        private MelonPreferences_ReflectiveCategory GeneralCategory;
        internal T General;

        internal I_Arm(bool is_left, string basefolder, string className)
        {
            IsLeft = is_left;
            string ArmName = $"Arm{(IsLeft ? "L" : "R")}";
            basefolder = Path.Combine(basefolder, ArmName);

            General = Config.SetupCategory<T>(ref GeneralCategory, "General", basefolder, className);

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
