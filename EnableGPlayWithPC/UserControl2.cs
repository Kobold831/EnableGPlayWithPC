using System;
using System.Windows.Forms;
using SharpAdbClient;

namespace EnableGPlayWithPC {
    public partial class UserControl2 : UserControl {
        static UserControl2 instance;

        public static UserControl2 GetInstance() {
            return instance;
        }

        public UserControl2() {
            InitializeComponent();
            instance = this;
            progressBar1.Style = ProgressBarStyle.Marquee;
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            button1.Text = "OK";
            button1.Enabled = false;
        }

        public void SetMessage(string str) {
            Invoke(new Action<string>(c), str);
        }

        /* 停止処理 */
        public void StopProgress() {
            var adbClient = new AdbClient();

            /* ADB停止 */
            try {
                adbClient.KillAdb();
            } catch(System.Net.Sockets.SocketException) {
            }

            Invoke(new Action(b));
        }

        public void WriteLog(string logText) {
            Invoke(new Action<string>(a), logText);
        }

        public void WriteLine() {
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.SelectionLength = 0;
            textBox1.SelectedText = "\r\n";
        }

        public void Button_Click(object sender, EventArgs e) {
            Main.GetInstance().Cancel();
        }

        private void a(string logText) {
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.SelectionLength = 0;
            textBox1.SelectedText = "[" + DateTime.Now.ToString() + "] " + logText + "\r\n";
        }

        private void b() {
            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Value = 0;
            button1.Enabled = true;
        }

        private void c(string str) {
            label1.Text = str;
        }
    }
}
