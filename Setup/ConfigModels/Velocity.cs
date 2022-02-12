using Tomlet.Attributes;

namespace GbHapticsIntegration.Setup.ConfigModels
{
    [TomlDoNotInlineObject]
    internal class CM_Velocity : CM_Toggle
    {
        public CM_Velocity() { }

        internal float Min = 0f;
        internal float Max = 2f;
        internal float Multiplier = 0.005f;
    }
}
