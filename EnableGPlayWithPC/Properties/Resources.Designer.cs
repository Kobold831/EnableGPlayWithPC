﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EnableGPlayWithPC.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EnableGPlayWithPC.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   すべてについて、現在のスレッドの CurrentUICulture プロパティをオーバーライドします
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   adb\adb.exe に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string AdbPath {
            get {
                return ResourceManager.GetString("AdbPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   {0}は対象端末ではありません。
        ///このソフトウェアは使用できません。
        ///エラーが繰り返し発生する場合は開発者にお問い合わせください。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Dialog_Not_Benesse_Tab_Desc {
            get {
                return ResourceManager.GetString("Dialog_Not_Benesse_Tab_Desc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   {0}に権限を付与できませんでした。
        ///エラーが繰り返し発生する場合は開発者にお問い合わせください。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Dialog_PermNotGranted_Desc {
            get {
                return ResourceManager.GetString("Dialog_PermNotGranted_Desc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   問題が発生したためデバイスと通信できませんでした。
        ///エラーが繰り返し発生する場合は開発者にお問い合わせください。
        ///以下を確認してから再実行してください：
        ///・デバイスの接続
        ///・このPCからのUSBデバッグの許可
        ///
        ///取得したデバイス名：{0} に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Dialog_Process_Error_Adb {
            get {
                return ResourceManager.GetString("Dialog_Process_Error_Adb", resourceCulture);
            }
        }
        
        /// <summary>
        ///   {0}が見つかりません。
        ///適切にファイルが解凍されなかったかファイルが削除された可能性があります。
        ///再ダウンロードしてください。
        ///エラーが繰り返し発生する場合は開発者にお問い合わせください。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Dialog_Process_Error_Adb404 {
            get {
                return ResourceManager.GetString("Dialog_Process_Error_Adb404", resourceCulture);
            }
        }
        
        /// <summary>
        ///   {0}が見つかりません。
        ///適切にファイルが解凍されなかったかファイルが削除された可能性があります。
        ///再ダウンロードしてください。
        ///エラーが繰り返し発生する場合は開発者にお問い合わせください。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Dialog_Process_Error_File404 {
            get {
                return ResourceManager.GetString("Dialog_Process_Error_File404", resourceCulture);
            }
        }
        
        /// <summary>
        ///   処理中にエラーが発生しました。
        ///{0}をインストールできませんでした。
        ///詳細情報：
        ///{1} に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Dialog_Process_Error_In {
            get {
                return ResourceManager.GetString("Dialog_Process_Error_In", resourceCulture);
            }
        }
        
        /// <summary>
        ///   処理エラー に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Dialog_Process_Error_Title {
            get {
                return ResourceManager.GetString("Dialog_Process_Error_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   {0}をアンインストールできませんでした。
        ///エラーが繰り返し発生する場合は開発者にお問い合わせください。
        ///以下を確認してから再実行してください：
        ///・アンインストールブロッカーが設定されていないこと
        ///・デバイス管理者に設定されていないこと
        ///
        ///詳細情報：
        ///{1} に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Dialog_Process_Error_Un {
            get {
                return ResourceManager.GetString("Dialog_Process_Error_Un", resourceCulture);
            }
        }
        
        /// <summary>
        ///   予期しないエラーが発生しました。
        ///再実行してください。
        ///エラーが繰り返し発生する場合は開発者にお問い合わせください。
        ///詳細情報：
        ///{0} に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Dialog_Process_Error_Unknown {
            get {
                return ResourceManager.GetString("Dialog_Process_Error_Unknown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Google の機能は有効化されました！
        ///デバイスを再起動しました。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Dialog_Successed_Desc {
            get {
                return ResourceManager.GetString("Dialog_Successed_Desc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   成功しました！ に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Dialog_Successed_Inst {
            get {
                return ResourceManager.GetString("Dialog_Successed_Inst", resourceCulture);
            }
        }
        
        /// <summary>
        ///   ２つ以上のデバイスが接続されているため続行できません。
        ///以下を確認してから再実行してください：
        ///ひとつのデバイスのみ接続されているか確認
        ///エラーが繰り返し発生する場合は開発者にお問い合わせください。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Dialog_TooManyDevices_Desc {
            get {
                return ResourceManager.GetString("Dialog_TooManyDevices_Desc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   バージョン : {0} / Kobold Ver.
        ///
        ///Copyright(c) 2020 AioiLight に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Information {
            get {
                return ResourceManager.GetString("Information", resourceCulture);
            }
        }
        
        /// <summary>
        ///   バージョン情報 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Information_Title {
            get {
                return ResourceManager.GetString("Information_Title", resourceCulture);
            }
        }
    }
}
