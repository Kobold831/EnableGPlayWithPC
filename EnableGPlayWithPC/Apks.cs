using System.IO;

namespace EnableGPlayWithPC {
    internal static class Apks {
        internal static string GoogleServicesFramework = "apk\\common\\GoogleServicesFramework.apk";
        internal static string GoogleServicesFramework_NEXT = "apk\\next\\GoogleServicesFramework.apk";
        internal static string GmsCore_2 = "apk\\2\\GmsCore.apk";
        internal static string GmsCore_3_NEO = "apk\\3-neo\\GmsCore.apk";
        internal static string GmsCore_NEXT = "apk\\next\\GmsCore.apk";
        internal static string Phonesky_2 = "apk\\2\\Phonesky.apk";
        internal static string Phonesky_3_NEO = "apk\\3-neo\\Phonesky.apk";
        internal static string Phonesky_NEXT = "apk\\next\\Phonesky.apk";
        internal static string SetupWarning = "apk\\tool\\SetupWarning.apk";
        internal static string EnableGServices = "apk\\tool\\EnableGServices.apk";
        internal static string BypassRevokePermission = "apk\\tool\\BypassRevokePermission.apk";

        internal static string[] GAppsInstallList_2(string appDir) {
            string[] files = {
                Path.Combine(appDir, GoogleServicesFramework),
                Path.Combine(appDir, GmsCore_2),
                Path.Combine(appDir, Phonesky_2) };
            return files;
        }

        internal static string[] GAppsInstallList_3_NEO(string appDir) {
            string[] files = {
                Path.Combine(appDir, GoogleServicesFramework),
                Path.Combine(appDir, GmsCore_3_NEO),
                Path.Combine(appDir, Phonesky_3_NEO) };
            return files;
        }

        internal static string[] GAppsInstallList_NEXT(string appDir) {
            string[] files = {
                Path.Combine(appDir, GoogleServicesFramework_NEXT),
                Path.Combine(appDir, GmsCore_NEXT),
                Path.Combine(appDir, Phonesky_NEXT) };
            return files;
        }

        internal static string[] ToolAppsInstallList(string appDir) {
            string[] files = {
                Path.Combine(appDir, SetupWarning),
                Path.Combine(appDir, EnableGServices),
                Path.Combine(appDir, BypassRevokePermission) };
            return files;
        }
    }
}
