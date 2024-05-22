/*
 * EnableGPlayWithPC
 *
 * Copyright (c) 2020 AioiLight
 *
 * MIT License
 * https://github.com/AioiLight/EnableGPlayWithPC/blob/master/LICENSE
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SharpAdbClient;
using SharpAdbClient.DeviceCommands;

namespace EnableGPlayWithPC {
    public partial class Main : Form {
        static Main instance;
        static UserControl1 ctr1;
        static UserControl2 ctr2;

        public static Main GetInstance() {
            return instance;
        }

        public Main() {
            InitializeComponent();
            instance = this;
            ctr1 = new UserControl1 {
                Visible = true
            };
            panel1.Controls.Add(ctr1);

            UserControl1.GetInstance().WriteConsole($"{FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).LegalCopyright}");
            UserControl1.GetInstance().WriteConsole(Properties.Resources.Information_Message);
        }

        /* 情報ボタン */
        private void InfoToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show(string.Format(Properties.Resources.Information + $"{FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).LegalCopyright}", Assembly.GetExecutingAssembly().GetName().Version), Properties.Resources.Information_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /* キャンセル処理 */
        public void Cancel() {
            ctr2.Visible = false;
            ctr1.Visible = true;
        }

        /* エラー処理 */
        private void ShowErrorMessage(string str) {
            UserControl2.GetInstance().StopProgress();
            UserControl2.GetInstance().SetMessage("エラーが発生しました");
            UserControl2.GetInstance().WriteLog(str);
        }

        /* 完了処理 */
        private void ShowCompleteMessage() {
            UserControl2.GetInstance().StopProgress();
            UserControl2.GetInstance().SetMessage("完了");
            UserControl2.GetInstance().WriteLog("完了しました！[OK]を押して終了してください");
        }

        /* 画面切替処理 */
        public void ChangeUserControl() {
            ctr2 = new UserControl2();
            ctr1.Visible = false;
            ctr2.Visible = true;
            panel1.Controls.Add(ctr2);
        }

        /* ダイアログ */
        private void ShowDialog(string instructionText, string text) {
            MessageBox.Show(text, instructionText, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        /* リスト1番目 */
        public void TryEnableGoogleServices() {
            DeviceData deviceData;
            var adbClient = new AdbClient();
            bool bl, sp;
            string str, errMsg, path, appDir, deviceName = "失敗しました";

            /* 処理ダイアログの表示 */
            ShowProcessDialog(0, null, 0);

            try {
                appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                /* ファイル存在確認してなければエラー終了 */
                ShowProcessDialog(1, null, 0);
                (path, bl) = IsCheckFileExists(appDir);
                if (!bl) {
                    ShowErrorMessage(string.Format(Properties.Resources.Dialog_Process_Error_File404, path));
                    Invoke(new Action<string, string>(ShowDialog), Properties.Resources.Dialog_Process_Error_Title, string.Format(Properties.Resources.Dialog_Process_Error_File404, path));
                    return;
                }

                /* ADBファイル存在確認してなければエラー終了 */
                ShowProcessDialog(2, null, 0);
                (bl) = StartAdbServer(appDir);
                if (!bl) {
                    ShowErrorMessage(string.Format(Properties.Resources.Dialog_Process_Error_Adb404, Path.Combine(appDir, Properties.Resources.AdbPath)));
                    Invoke(new Action<string, string>(ShowDialog), Properties.Resources.Dialog_Process_Error_Title, string.Format(Properties.Resources.Dialog_Process_Error_Adb404, Path.Combine(appDir, Properties.Resources.AdbPath)));
                    return;
                }

                /* ADB接続を確認してなければエラー終了 */
                ShowProcessDialog(3, null, 0);
                try {
                    ConsoleOutputReceiver receiver = new ConsoleOutputReceiver();
                    adbClient.ExecuteRemoteCommand($"getprop ro.product.model", adbClient.GetDevices().First(), receiver);
                    deviceName = receiver.ToString().Substring(0, receiver.ToString().Length - 2);
                    UserControl2.GetInstance().WriteLog("端末を検出しました：" + deviceName);
                } catch (Exception) {
                    ShowErrorMessage(string.Format(Properties.Resources.Dialog_Process_Error_Adb, deviceName));
                    Invoke(new Action<string, string>(ShowDialog), Properties.Resources.Dialog_Process_Error_Title, string.Format(Properties.Resources.Dialog_Process_Error_Adb, deviceName));
                    return;
                }

                /* デバイス接続・接続数確認して異常であればエラー終了 */
                (deviceData, bl) = IsCheckDeviceConnect();
                if (!bl) {
                    ShowErrorMessage(Properties.Resources.Dialog_TooManyDevices_Desc);
                    Invoke(new Action<string, string>(ShowDialog), (Properties.Resources.Dialog_Process_Error_Title, Properties.Resources.Dialog_TooManyDevices_Desc), this.Handle);
                    return;
                }

                /* チャレンジパッドを確認して該当デバイスでなければエラー終了 */
                (deviceName, bl, sp) = IsCheckDeviceName(deviceData);
                // チャレンジパッドだが CT2 では無い
                if (bl && !sp) {
                    ShowErrorMessage(string.Format(Properties.Resources.Dialog_Process_Error_Unsupported, deviceName));
                    Invoke(new Action<string, string>(ShowDialog), Properties.Resources.Dialog_Process_Error_Title, string.Format(Properties.Resources.Dialog_Process_Error_Unsupported, deviceName));
                    Process.Start("https://github.com/Kobold831/EnableGPlayWithPC/tree/HEAD/docs#%E5%AF%BE%E5%BF%9C%E6%A9%9F%E7%A8%AE");
                    return;
                }
                // 他の端末
                else if (!bl) {
                    ShowErrorMessage(string.Format(Properties.Resources.Dialog_Not_Benesse_Tab_Desc, deviceName));
                    Invoke(new Action<string, string>(ShowDialog), Properties.Resources.Dialog_Process_Error_Title, string.Format(Properties.Resources.Dialog_Not_Benesse_Tab_Desc, deviceName));
                    return;
                }

                /* ツールアプリインストール試行して失敗すればエラー終了 */
                SetMessage("｢ツールアプリ｣をインストールしています...");
                (bl, str, errMsg) = TryInstallTool(deviceData);
                if (!bl) {
                    ShowErrorMessage(string.Format(Properties.Resources.Dialog_Process_Error_In, str, errMsg));
                    Invoke(new Action<string, string>(ShowDialog), Properties.Resources.Dialog_Process_Error_Title, string.Format(Properties.Resources.Dialog_Process_Error_In, str, errMsg));
                    return;
                }   

                /* GAppsアンインストール試行して失敗すればエラー終了 */
                (str, errMsg, bl) = TryUninstallAPK(deviceData);
                if (!bl) {
                    ShowErrorMessage(string.Format(Properties.Resources.Dialog_Process_Error_Un, str, errMsg));
                    Invoke(new Action<string, string>(ShowDialog), Properties.Resources.Dialog_Process_Error_Title, string.Format(Properties.Resources.Dialog_Process_Error_Un, str, errMsg));
                    return;
                }

                /* GAppsインストール試行して失敗すればエラー終了 */
                (bl, str, errMsg) = TryInstallAPK(deviceData, appDir);
                if (!bl) {
                    ShowErrorMessage(string.Format(Properties.Resources.Dialog_Process_Error_In, str, errMsg));
                    Invoke(new Action<string, string>(ShowDialog), Properties.Resources.Dialog_Process_Error_Title, string.Format(Properties.Resources.Dialog_Process_Error_In, str, errMsg));
                    return;
                }

                /* GApps権限付与試行して失敗すればエラー終了 */
                (bl, str) = TryGrantPermissions(deviceData);
                if (!bl) {
                    ShowErrorMessage(string.Format(Properties.Resources.Dialog_PermNotGranted_Desc, str));
                    Invoke(new Action<string, string>(ShowDialog), Properties.Resources.Dialog_Process_Error_Title, string.Format(Properties.Resources.Dialog_PermNotGranted_Desc, str));
                    return;
                }

                /* GApps上書きインストール試行して失敗すればエラー終了 */
                (bl, str, errMsg) = TryReInstallAPK(deviceData, appDir);
                if (!bl) {
                    ShowErrorMessage(string.Format(Properties.Resources.Dialog_Process_Error_In, str, errMsg));
                    Invoke(new Action<string, string>(ShowDialog), Properties.Resources.Dialog_Process_Error_Title, string.Format(Properties.Resources.Dialog_Process_Error_In, str, errMsg));
                    return;
                }

                /* 最終処理 */
                EndProcess(deviceData);
                return;
            } catch (Exception e) {
                /* 予期しない例外が発生したらエラー終了 */
                ShowErrorMessage(string.Format(Properties.Resources.Dialog_Process_Error_Unknown, e.Message));
                Invoke(new Action<string, string>(ShowDialog), Properties.Resources.Dialog_Process_Error_Title, string.Format(Properties.Resources.Dialog_Process_Error_Unknown, e.Message));
                return;
            }
        }

        void SetMessage(string msg) {
            UserControl2.GetInstance().WriteLog(msg);
            UserControl2.GetInstance().SetMessage(msg);
        }

        /* 処理順にメッセージ変更 */
        private void ShowProcessDialog(int process, string msg, int count) {
            switch (process) {
                case -2:
                    UserControl2.GetInstance().WriteLog("...NG");
                    break;

                case -1:
                    UserControl2.GetInstance().WriteLog("...OK");
                    break;

                case 0:
                    SetMessage("処理しています...");
                    break;

                case 1:
                    SetMessage("ファイルを確認しています...");
                    break;

                case 2:
                    SetMessage("ADBを確認しています...");
                    break;

                case 3:
                    SetMessage("デバイスを確認しています...");
                    break;

                case 4:
                    SetMessage("｢" + msg + "｣をアンインストールしています... (" + count + "/3)");
                    break;

                case 5:
                    SetMessage("｢" + msg + "｣をインストールしています... (" + count + "/3)");
                    break;

                case 6:
                    SetMessage("｢" + msg + "｣に権限を付与しています... (" + count + "/3)");
                    break;

                case 7:
                    SetMessage("｢" + msg + "｣を上書きインストールしています... (" + count + "/3)");
                    break;

                case 8:
                    SetMessage("最終処理をしています...");
                    break;

                default:
                    break;
            }
        }

        /* ファイル存在確認 */
        private (string, bool) IsCheckFileExists(string appDir) {
            foreach (string path in Apks.GAppsInstallList_2(appDir)) {
                UserControl2.GetInstance().WriteLog(path);

                if (!File.Exists(path)) {
                    ShowProcessDialog(-2, null, 0);
                    return (path, false);
                } else {
                    ShowProcessDialog(-1, null, 0);
                }
            }

            foreach (string path in Apks.ToolAppsInstallList(appDir)) {
                UserControl2.GetInstance().WriteLog(path);

                if (!File.Exists(path)) {
                    ShowProcessDialog(-2, null, 0);
                    return (path, false);
                } else {
                    ShowProcessDialog(-1, null, 0);
                }
            }

            return (null, true);
        }

        /* ADBファイル存在確認 */
        private bool StartAdbServer(string appDir) {
            UserControl2.GetInstance().WriteLog(Path.Combine(appDir, Properties.Resources.AdbPath));

            /* ADB起動試行 */
            try {
                new AdbServer().StartServer(Path.Combine(appDir, Properties.Resources.AdbPath), true);
            } catch (Exception) {
                ShowProcessDialog(-2, null, 0);
                return false;
            }

            ShowProcessDialog(-1, null, 0);
            return true;
        }

        /* デバイス接続・接続数確認 */
        private (DeviceData, bool) IsCheckDeviceConnect() {
            var adbClient = new AdbClient();
            try {
                DeviceData deviceData = adbClient.GetDevices().First();

                if (adbClient.GetDevices().Count > 1) {
                    return (deviceData, false);
                }
                return (deviceData, true);
            } catch (Exception) {
                return (null, false);
            }
        }

        /* チャレンジパッド確認 */
        private (string, bool, bool) IsCheckDeviceName(DeviceData deviceData) {
            var adbClient = new AdbClient();
            try {
                string model;

                ConsoleOutputReceiver cor = new ConsoleOutputReceiver();

                adbClient.ExecuteRemoteCommand($"getprop ro.product.model", deviceData, cor);

                /* 余計な改行は入れさせない */
                model = cor.ToString().Substring(0, cor.ToString().Length - 2);

                Console.WriteLine(model.Length);

                /* 出力が名前にあるか確認 */
                if (BenesseTabs.TARGET_MODEL_2.Contains(model)) {
                    return (model, true, true);
                }
                else if (BenesseTabs.TARGET_MODEL_EX.Contains(model)) {
                    return (model, true, false);
                }
                return (model, false, false);
            } catch (Exception) {
                return (null, false, false);
            }
        }

        /* ツールアプリインストール試行 */
        private (bool, string, string) TryInstallTool(DeviceData deviceData) {
            var adbClient = new AdbClient();
            PackageManager pm = new PackageManager(adbClient, deviceData);

            /* ツールインストール */
            try {
                pm.InstallPackage(Apks.SetupWarning, false);
            } catch (Exception e) {
                return (false, "SetupWarning", e.Message);
            }

            AndroidDebugBridgeUtils.StartActivity(deviceData, "com.saradabar.setupwarning/.MainActivity");

            // EnableGservices をインストール
            try {
            pm.InstallPackage(Apks.EnableGServices, false);
            } catch (Exception e) {
                return (false, "EnableGServices", e.Message);
            }

            return (true, null, null);
        }

        /* GAppsのアンインストール試行 */
        private (string, string, bool) TryUninstallAPK(DeviceData deviceData) {
            var adbClient = new AdbClient();
            PackageManager pm = new PackageManager(adbClient, deviceData);

            foreach (string pkg in Packages.Package) {
                try {
                    ShowProcessDialog(4, Packages.PackageNameList[Array.IndexOf(Packages.Package, pkg)], Array.IndexOf(Packages.Package, pkg) + 1);
                    if (AndroidDebugBridgeUtils.IsInstallPackage(deviceData, pkg)) {
                        pm.UninstallPackage(pkg);
                    }
                } catch (Exception e) {
                    return (Packages.PackageNameList[Array.IndexOf(Packages.Package, pkg)], e.Message, false);
                }
            }

            return ("", "", true);
        }

        /* GAppsインストール試行 */
        private (bool, string, string) TryInstallAPK(DeviceData deviceData, string appDir) {
            int i = 0;
            var adbClient = new AdbClient();
            PackageManager pm = new PackageManager(adbClient, deviceData);
            /* インストール */
            try {
                Array.ForEach(Apks.GAppsInstallList_2(appDir), apk => {
                    ShowProcessDialog(5, Packages.PackageNameList[i], i + 1);
                        pm.InstallPackage(apk, false);
                    i++;
                });
            } catch (Exception e) {
                return (false, Packages.PackageNameList[i], e.Message);
            }

            return (true, null, null);
        }

        /* GApps権限付与試行 */
        private (bool, string) TryGrantPermissions(DeviceData device) {
            for (int i = 0; i < 3; i++) {
                ShowProcessDialog(6, Packages.PackageNameList[i], i + 1);
                if (!AndroidDebugBridgeUtils.GrantPermissions(Packages.Package[i], Permissions.PermissionGroups[i], device)) {
                    return (false, Packages.PackageNameList[i]);
                }
            }

            return (true, null);
        }

        /* GApps上書きインストール試行 */
        /* 権限の関係で上書きインストールを行わないと付与されない */
        private (bool, string, string) TryReInstallAPK(DeviceData deviceData, string appDir) {
            int i = 0;
            var adbClient = new AdbClient();
            PackageManager pm = new PackageManager(adbClient, deviceData);
            try {
                Array.ForEach(Apks.GAppsInstallList_2(appDir), apk => {
                    ShowProcessDialog(7, Packages.PackageNameList[i], i + 1);
                    pm.InstallPackage(apk, true);
                    i++;
                });
            } catch (Exception e) {
                return (false, Packages.PackageNameList[i], e.Message);
            }

            return (true, null, null);
        }

        /* 最終処理 */
        private void EndProcess(DeviceData deviceData) {
            var adbClient = new AdbClient();
            PackageManager pm = new PackageManager(adbClient, deviceData);
            ShowProcessDialog(8, null, 0);

            // EnableGService を起動
            AndroidDebugBridgeUtils.SendBroadcast(deviceData, "com.saradabar.intent.action.RUN_ENABLE_GSERV");

            /* SetupWarning のアンインストール */
            pm.UninstallPackage("com.saradabar.setupwarning");

            /* デバイス再起動 */
            adbClient.Reboot("", deviceData);

            /* ADB停止 */
            adbClient.KillAdb();

            ShowCompleteMessage();
            MessageBox.Show(Properties.Resources.Dialog_Successed_Desc, Properties.Resources.Dialog_Successed_Inst, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Main_Load(object sender, EventArgs e) {
            Text += "   Ver." + Assembly.GetExecutingAssembly().GetName().Version;
        }
    }
}
