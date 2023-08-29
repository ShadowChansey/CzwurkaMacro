using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CzwurkaMacro
{
    public partial class Form1 : Form

    {
        const int PauseBetweenStrokes = 50;
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        const int KEY_DOWN_EVENT = 0x0001; //Key down flag
        const int KEY_UP_EVENT = 0x0002; //Key up flag

        public static void HoldKey(byte key, int duration)
        {
            int totalDuration = 0;
            while (totalDuration < duration)
            {
                keybd_event(key, 0, KEY_DOWN_EVENT, 0);
                keybd_event(key, 0, KEY_UP_EVENT, 0);
                System.Threading.Thread.Sleep(PauseBetweenStrokes);
                totalDuration += PauseBetweenStrokes;
            }
        }
        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        const int WM_MOUSELEFTDOWN = 0x201;
        const int WM_MOUSELEFTUP = 0x202;
        const int WM_DDOWN = 0x44;
        const int WM_DUP = 0xC4;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "start")
            {
                button1.Text = "stop";
                kopanie(button1, textBox1);
            }
            else if (button1.Text == "stop")
                button1.Text = "start";

            void kopanie(Button toggle, TextBox oknomc)
            {
                
                while (toggle.Text == "stop")
                {
                    IntPtr hwnd = FindWindow(null, oknomc.Text);
                    PostMessage(hwnd, WM_MOUSELEFTDOWN, 0, 0);
                    PostMessage(hwnd, WM_DDOWN, 0, 0);
                    Thread.Sleep(1000);
                    PostMessage(hwnd, WM_DUP, 0, 0);
                    Thread.Sleep(10000);
                    PostMessage(hwnd, WM_MOUSELEFTUP, 0, 0);
                    Thread.Sleep(1);

                }
            }
        }
    }
}
