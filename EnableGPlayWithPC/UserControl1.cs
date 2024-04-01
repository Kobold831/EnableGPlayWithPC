using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EnableGPlayWithPC.Main;

namespace EnableGPlayWithPC {
    public partial class UserControl1 : UserControl {

        public static UserControl1 instance;

        /* 実行リスト */
        readonly string[] str = { "Googleサービスの有効化" };

        bool selfUpdate = false;

        public static UserControl1 GetInstance() {
            return instance;
        }

        public UserControl1() {
            InitializeComponent();
            instance = this;
            linkLabel1.Text = "開発者のページはここをクリックしてください。";
            linkLabel1.Links.Add(8, 2, "");
            checkedListBox1.Items.AddRange(str);
            checkedListBox1.CheckOnClick = true;
            checkedListBox1.Enabled = false;
            checkedListBox1.SetItemCheckState(0, CheckState.Checked);
        }

        public void WriteConsole(string text) {
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.SelectionLength = 0;
            textBox1.SelectedText = text + "\r\n";
        }

        public async void Button_Click(object sender, EventArgs e) {
            Task task;
            Main.GetInstance().ChangeUserControl();
            if (selfUpdate) {
                task = Task.Run(Main.GetInstance().SelfUpdateAsync);
            } else {
                task = Task.Run(Main.GetInstance().TryEnableGoogleServices);
            }
            await task;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start(@"https://github.com/Kobold831/EnableGPlayWithPC");
        }

        public void SelfUpdate() {
            selfUpdate = true;
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(@"Check.json")));
            ms.Seek(0, SeekOrigin.Begin);
            var data = new DataContractJsonSerializer(typeof(JsonData)).ReadObject(ms) as JsonData;

            WriteConsole("アップデートがあります。実行を押して、アップデートを適用してください。\r\n" + "アップデート内容：" + data.JsonEgp.JsonUpdate.Description);
        }
    }
}