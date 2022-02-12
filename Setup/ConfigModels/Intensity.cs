using Tomlet.Attributes;

namespace GbHapticsIntegration.Setup.ConfigModels
{
    [TomlDoNotInlineObject]
    internal class CM_Intensity : CM_Toggle
    {
        public CM_Intensity() { }

        internal float IntensityScale = 1.0f;
    }
}
