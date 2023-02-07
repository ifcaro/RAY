using System;
using System.Reflection;
using System.Windows.Forms;

namespace RAY
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            string appVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            VersionLabel.Text = string.Format(Strings.VERSION_X, appVersion);
        }

        private void WebSiteLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.ifcaro.net");
        }

        private void GithubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ifcaro/RAY");
        }
    }
}
