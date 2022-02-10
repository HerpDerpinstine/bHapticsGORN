using System.IO;
using MelonLoader;
using MelonLoader.Preferences;

namespace GbHapticsIntegration.Setup.Devices
{
    internal class I_Vest<T> : TactFile where T : I_GeneralValues, new()
    {
        private MelonPreferences_ReflectiveCategory GeneralCategory;
        internal T General;

        internal I_Vest(string basefolder, string className)
        {
            basefolder = Path.Combine(basefolder, "Vest");

            General = Config.SetupCategory<T>(ref GeneralCategory, "General", basefolder, className);

            Register(
                $"{basefolder.Replace("\\", "/").Replace("/", "_")}_{className}",
                $"{className}.tact",
                basefolder,
                $"{className}_Vest");
        }

        internal bool IsEnabled()
            => (Config.HapticDevices.VestFront 
            && Config.HapticDevices.VestBack 
            && General.Enabled);
    }
}
