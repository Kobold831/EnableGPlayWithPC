using System.Collections.Generic;
using System.Linq;
using SharpAdbClient;

namespace EnableGPlayWithPC {
    internal static class AndroidDebugBridgeUtils {

        /// <summary>
        /// ADBからの文字列を調べて、インストールに成功しているかどうかチェックする。
        /// </summary>
        /// <param name="str">出力。</param>
        /// <returns>インストールに成功しているかどうか。</returns>
        internal static bool IsInstallSuccessful(string str) {
            var lines = str.Split('\n');
            if (lines.Where(s => s.StartsWith("Success")).Any()) {
                /* Successで始まる行が少なくともひとつはある */
                /* インストールに成功している */
                return true;
            } else {
                /* インストールに失敗している */
                return false;
            }
        }

        /// <summary>
        /// 権限を付与する。
        /// </summary>
        /// <param name="packageName">パッケージ名。</param>
        /// <param name="perms">権限の一覧。</param>
        /// <param name="deviceData">ADB デバイス。</param>
        /// <returns>成功したかどうか。</returns>
        internal static bool GrantPermissions(string packageName, IEnumerable<string> perms, DeviceData deviceData) {
            var adbClient = new AdbClient();
            foreach (var perm in perms) {
                var receiver = new ConsoleOutputReceiver();
                var cmd = $"pm grant {packageName} {Permissions.Prefix}{perm}";
                adbClient.ExecuteRemoteCommand(cmd, deviceData, receiver);

                var result = receiver.ToString();

                if (!IsPermissionGranted(result)) {
                    /* 権限付与に失敗した */
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ADBからの文字列を調べて、権限付与に成功しているかどうかチェックする。
        /// </summary>
        /// <param name="str">出力。</param>
        /// <returns>権限付与に成功しているかどうか。</returns>
        internal static bool IsPermissionGranted(string str) {
            var lines = str.Split('\n');
            if (lines.Where(s => s.StartsWith("Operation not allowed:")).Any()) {
                /* Operation not allowed:で始まる行が少なくともひとつはある */
                return false;
            } else {
                /* 権限付与に成功している */
                return true;
            }
        }

        /// <summary>
        /// ADBからの文字列を調べて、インストールされているかどうかチェックする。
        /// </summary>
        /// <param name="str">パッケージ名</param>
        /// <returns>インストールされているかどうか。</returns>
        internal static bool IsInstallPackage(DeviceData deviceData, string str) {
            var adbClient = new AdbClient();
            var installCmd = $"pm list packages \"\"{str}\"\"";

            ConsoleOutputReceiver receiver = new ConsoleOutputReceiver();
            adbClient.ExecuteRemoteCommand(installCmd, deviceData, receiver);

            var lines = receiver.ToString().Split('\n');
            if (lines.Where(s => s == "$\"package:{str}\"").Any()) {
                /* package:で始まる行が少なくともひとつはある */
                return true;
            } else {
                /* インストールされていない */
                return false;
            }
        }

        /// <summary>
        /// ブロードキャストを送信する。
        /// </summary>
        /// <param name="str">ACTION名</param>
        /// <returns>成功したか。</returns>
        internal static bool SendBroadcast(DeviceData deviceData, string str) {
            var adbClient = new AdbClient();
            var installCmd = $"am broadcast -a {str}";

            ConsoleOutputReceiver receiver = new ConsoleOutputReceiver();
            adbClient.ExecuteRemoteCommand(installCmd, deviceData, receiver);

            var lines = receiver.ToString().Split('\n');
            if (lines.Where(s => s.StartsWith("Broadcast completed:")).Any()) {
                /* Broadcast completed:で始まる行が少なくともひとつはある */
                return true;
            } else {
                /* 失敗 */
                return false;
            }
        }

        /// <summary>
        /// Activityを送信する。
        /// </summary>
        /// <param name="str">パッケージ名/Activityフルクラス名</param>
        /// <returns>成功したか。</returns>
        internal static bool StartActivity(DeviceData deviceData, string str) {
            var adbClient = new AdbClient();
            var installCmd = $"am start -n {str}";

            ConsoleOutputReceiver receiver = new ConsoleOutputReceiver();
            adbClient.ExecuteRemoteCommand(installCmd, deviceData, receiver);

            var lines = receiver.ToString().Split('\n');
            if (lines.Where(s => s.StartsWith("Starting:")).Any()) {
                /* Starting:で始まる行が少なくともひとつはある */
                return true;
            } else {
                /* 失敗 */
                return false;
            }
        }
    }
}
