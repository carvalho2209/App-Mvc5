using System.Globalization;

namespace AAC.AppMvc
{
    public static class CultureConfig
    {
        public static void RegisterCulture()
        {
            var culture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
    }
}