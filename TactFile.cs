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
#if DEBUG
                bool is_file = true;
#endif
                string json = null;
                if (File.Exists(filepath))
                {
                    json = File.ReadAllText(filepath);
                    if (string.IsNullOrEmpty(json))
                        throw new Exception($"Failed to Read JSON from File!");
                }
                else
                {
#if DEBUG
                    is_file = false;
#endif
                    json = Properties.Resources.ResourceManager.GetString(DefaultResourcesName);
                    if (string.IsNullOrEmpty(json))
                        throw new Exception($"Failed to Read Default JSON ({DefaultResourcesName}) from Resources!");
                }

                bHaptics.RegisterFeedbackFromTactFile(Identifier, json);

#if DEBUG
                Debug.LogTactFileRegister(Identifier, filepath, is_file);
#endif
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

#if DEBUG
            Debug.LogTactFilePlayback(Identifier, scaleOption, rotationOption);
#endif
        }

        internal bool IsPlaying()
             => bHaptics.IsPlaying(Identifier);
    }
}
