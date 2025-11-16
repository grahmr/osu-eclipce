using osu.Framework;
using osu.Framework.Graphics;
using osu.Eclipse.Game.Skins;

namespace osu.Eclipse.Game
{
    public class EclipseGame : Game
    {
        public EclipseGame()
        {
            Name = "osu! eclipse";
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            // Initializing both stable and lazer skin support
            var skinManager = new SkinManager();
            skinManager.RegisterProvider(new LegacySkinProvider());
            skinManager.RegisterProvider(new ModernSkinProvider());

            // Example: Apply legacy skin by default
            skinManager.GetProvider("LegacySkin")?.ApplySkin();

            // TODO: Add main menu and beatmap/ruleset/score importers; combine features here
        }
    }
}
