using Tomlet.Attributes;

namespace GbHapticsIntegration.Setup.ConfigModels
{
    [TomlDoNotInlineObject]
    internal class CM_Intensity2 : CM_Toggle
    {
        public CM_Intensity2() { }

        internal float IntensityScale_Light = 0.5f;
        internal float IntensityScale_Medium = 0.5f;
        internal float IntensityScale_Hard = 0.5f;
    }
}
