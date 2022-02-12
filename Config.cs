using System.IO;
using MelonLoader;
using MelonLoader.Preferences;

namespace GbHapticsIntegration
{
	internal static class Config
	{
		internal static I_General General = null;
		internal static I_HapticDevices HapticDevices = null;
		internal static I_HapticEffects HapticEffects = null;

		internal class I_General
		{
			private static MelonPreferences_ReflectiveCategory Category = null;
			internal static void Init()
				=> General = SetupCategory<I_General>(ref Category, nameof(General));

			internal bool Allow_Player_Communication = true;
		}

		internal class I_HapticDevices
		{
			private static MelonPreferences_ReflectiveCategory Category = null;
			internal static void Init()
				=> HapticDevices = SetupCategory<I_HapticDevices>(ref Category, nameof(HapticDevices));

			internal bool Head = true;
			internal bool VestFront = true;
			internal bool VestBack = true;
			internal bool HandL = true;
			internal bool HandR = true;
			internal bool ArmL = true;
			internal bool ArmR = true;
		}

		internal class I_HapticEffects
        {
			private static MelonPreferences_ReflectiveCategory Category = null;
			internal static void Init()
				=> HapticEffects = SetupCategory<I_HapticEffects>(ref Category, nameof(HapticEffects));

			internal bool Blunt = true;
			internal bool Cut = true;
			internal bool DrawString = true;
			internal bool Gong = true;
			internal bool HeartBeat = true;
			internal bool PlayerDamage = true;
			internal bool Shoot = true;
			internal bool ShootString = true;
			internal bool Stab = true;
			internal bool Surprise = true;
			internal bool Wobble = true;
		}

		internal static T SetupCategory<T>(ref MelonPreferences_ReflectiveCategory category, string categoryName, string folderPath = null, string fileName = "Config") where T : new()
		{
			if (category == null)
			{
				string FolderPath = GbHapticsIntegration.BaseUserDataDirectory;

				if (!string.IsNullOrEmpty(folderPath))
					FolderPath = Path.Combine(FolderPath, folderPath);

				if (!Directory.Exists(FolderPath))
					Directory.CreateDirectory(FolderPath);

				string FilePath = Path.Combine(FolderPath, $"{fileName}.cfg");

				category = MelonPreferences.CreateCategory<T>(categoryName);
				category.SetFilePath(FilePath, Debug.Config_Load, Debug.Config_PrintMsg);
				category.SaveToFile(false);

				Debug.LogConfigRegister(categoryName, FilePath);
			}
			return category.GetValue<T>();
		}
	}
}