using System.IO;

namespace EnableGPlayWithPC {
    internal static class Apks {
        internal static string GoogleServicesFramework = "apk\\gsa\\GoogleServicesFramework.apk";
        internal static string GmsCore = "apk\\gsa\\GmsCore.apk";
        internal static string Phonesky = "apk\\gsa\\Phonesky.apk";
        internal static string SetupWarning = "apk\\tool\\SetupWarning.apk";
        internal static string EnableGServices = "apk\\tool\\EnableGServices.apk";

        internal static string[] GAppsInstallList_2(string appDir) {
            string[] files = {
                Path.Combine(appDir, GoogleServicesFramework),
                Path.Combine(appDir, GmsCore),
                Path.Combine(appDir, Phonesky) };
            return files;
        }

        internal static string[] ToolAppsInstallList(string appDir) {
            string[] files = {
                Path.Combine(appDir, SetupWarning),
                Path.Combine(appDir, EnableGServices) };
            return files;
        }
    }
}
