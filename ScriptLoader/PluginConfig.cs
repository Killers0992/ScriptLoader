using Exiled.API.Interfaces;

namespace ScriptLoader
{
    public class PluginConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;
    }
}
