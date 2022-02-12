using Tomlet.Attributes;

namespace GbHapticsIntegration.Setup.ConfigModels
{
    [TomlDoNotInlineObject]
    internal class CM_Toggle
    {
        public CM_Toggle() { }

        internal bool Enabled = true;
    }
}
