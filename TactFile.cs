using System;
using System.IO;
using MelonLoader;

namespace GbHapticsIntegration
{
    internal class TactFile
    {
        private string Identifier;

        internal void Register(string identifier, string filename, string folderpath, string DefaultResourcesName)
        {
            Identifier = identifier;

            string FolderPath = Path.Combine(GbHapticsIntegration.BaseUserDataDirectory, folderpath);
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            string filepath = Path.Combine(FolderPath, filename);
            try
            {
                bool is_file = true;
                string json = null;
                if (File.Exists(filepath))
                {
                    json = File.ReadAllText(filepath);
                    if (string.IsNullOrEmpty(json))
                        throw new Exception($"Failed to Read JSON from File!");
                }
                else
                {
                    is_file = false;
                    json = Properties.Resources.ResourceManager.GetString(DefaultResourcesName);
                    if (string.IsNullOrEmpty(json))
                        throw new Exception($"Failed to Read Default JSON ({DefaultResourcesName}) from Resources!");
                    MelonLogger.Msg(json);
                }

                bHaptics.RegisterFeedbackFromTactFile(Identifier, json);
                Debug.LogTactFileRegister(Identifier, filepath, is_file);
            }
            catch (Exception ex)
            {
                GbHapticsIntegration.Logger.Error($"Failed to Register bHaptics Pattern [{Identifier}]\nfrom TactFile [{filepath}]:\n{ex}");
            }
        }

        internal void Play()
            => Play(new bHaptics.ScaleOption());
        internal void Play(bHaptics.ScaleOption scaleOption)
            => Play(scaleOption, new bHaptics.RotationOption(0, 0));
        internal void Play(bHaptics.ScaleOption scaleOption, bHaptics.RotationOption rotationOption)
        {
            bHaptics.SubmitRegistered(Identifier, Identifier, scaleOption, rotationOption);
            Debug.LogTactFilePlayback(Identifier, scaleOption, rotationOption);
        }

        internal bool IsPlaying()
             => bHaptics.IsPlaying(Identifier);
    }
}
