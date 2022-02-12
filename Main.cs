using System.IO;
using MelonLoader;
using GbHapticsIntegration.Hooks;
using GbHapticsIntegration.Managers;

namespace GbHapticsIntegration
{
    internal class GbHapticsIntegration : MelonMod
	{
        internal static HarmonyLib.Harmony ModHarmony = null;
        internal static MelonLogger.Instance Logger = null; 
        internal static string BaseUserDataDirectory = null;

        public override void OnApplicationStart()
        {
            ModHarmony = HarmonyInstance;
            Logger = LoggerInstance;
            BaseUserDataDirectory = Path.Combine(MelonUtils.UserDataDirectory, "bHaptics");
            if (!Directory.Exists(BaseUserDataDirectory))
                Directory.CreateDirectory(BaseUserDataDirectory);

            Config.I_General.Init();
            Config.I_HapticDevices.Init();
            Config.I_HapticEffects.Init();
            M_Tact.Setup();

            H_Bow.Initialize();
            H_CrossbowCaestus.Initialize();
            H_DamageRelay.Initialize();
            H_DamagerRigidbody.Initialize();
            H_FakeParent.Initialize();
            H_Gong.Initialize();
            H_GrabHand.Initialize();
            H_Grapple.Initialize();
            H_Gun.Initialize();
            H_PlayerDamageRelay.Initialize();
            H_SurpriseBox.Initialize();

            Logger.Msg("Initialized!");
        }
    }
}