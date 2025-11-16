using osu.Framework.Platform;
using osu.Eclipse.Game;

namespace osu.Eclipse.Desktop
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using (GameHost host = Host.GetSuitableHost(@"osu! eclipse"))
            using (var game = new EclipseGame())
                host.Run(game);
        }
    }
}
