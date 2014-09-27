using System;
using System.Windows.Forms;

namespace Slack
{
    public partial class SlackForm : Form
    {
        public SlackForm()
        {
            InitializeComponent();

            webBrowser.Url = new Uri("https://slack.com/signin");
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //webBrowser.Navigate(new Uri("javascript:window.Notification = function(title, options) { window.alert(title); }; window.Notification.permission = 'granted'; undefined;"));
        }

        private void SlackForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.Bounds = Bounds;
            }
            else
            {
                Properties.Settings.Default.Bounds = RestoreBounds;
            }

            Properties.Settings.Default.WindowState = WindowState;

            Properties.Settings.Default.Save();
        }

        private void SlackForm_Load(object sender, EventArgs e)
        {
            Bounds = Properties.Settings.Default.Bounds;
            WindowState = Properties.Settings.Default.WindowState;
        }
    }
}
