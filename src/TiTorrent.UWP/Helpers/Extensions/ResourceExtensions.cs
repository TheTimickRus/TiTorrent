using Windows.ApplicationModel.Resources;

namespace TiTorrent.UWP.Helpers.Extensions
{
    internal static class ResourceExtensions
    {
        private static readonly ResourceLoader ResLoader = new();

        public static string GetLocalized(this string resourceKey)
        {
            return ResLoader.GetString(resourceKey);
        }
    }
}
