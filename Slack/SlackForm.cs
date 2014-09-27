using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Slack
{
    public partial class SlackForm : Form
    {
        private bool forceClose = false;

        public SlackForm()
        {
            InitializeComponent();

            webBrowser.Url = new Uri("https://slack.com/signin");
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //webBrowser.Navigate(new Uri("javascript:window.Notification = function(title, options) { window.alert(title); }; window.Notification.permission = 'granted'; undefined;"));
        }

        private void SlackForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon.Dispose();
        }

        private void SlackForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!forceClose)
            {
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;

                e.Cancel = true;
            }
            else
            {
                if (WindowState == FormWindowState.Normal)
                {
                    Properties.Settings.Default.Bounds = Bounds;
                }
                else
                {
                    Properties.Settings.Default.Bounds = RestoreBounds;
                }

                Properties.Settings.Default.Save();
            }
        }

        private void SlackForm_Load(object sender, EventArgs e)
        {
            Bounds = Properties.Settings.Default.Bounds;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            forceClose = true;

            Close();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowInTaskbar = true;

                if (IsIconic(Handle))
                {
                    ShowWindowAsync(Handle, SW_RESTORE);
                }

                Activate();
            }
        }

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        private const int SW_RESTORE = 9;
    }
}
