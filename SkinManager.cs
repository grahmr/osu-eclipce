using System.Collections.Generic;

namespace osu.Eclipse.Game.Skins
{
    public class SkinManager
    {
        private readonly List<ISkinProvider> skinProviders = new List<ISkinProvider>();

        public void RegisterProvider(ISkinProvider provider)
        {
            skinProviders.Add(provider);
        }

        public ISkinProvider GetProvider(string name)
        {
            return skinProviders.Find(p => p.Name == name);
        }
    }

    public interface ISkinProvider
    {
        string Name { get; }
        void ApplySkin();
        // Extend this interface with methods for resource retrieval, audio, etc.
    }
}
