using System;
using System.Net.Http;
using System.Windows.Forms;

namespace ArcRaiders_Color_Tool
{
    public partial class UpdateNotifier : Form
    {
        private Version current, latest;

        // Utilize repository's file as version notifier
        // I know it sounds very dangerous. but i am broke as hell.
        private string downloadUrl = @"https://github.com/astron8t-voyagerx/ArcRaiders-Color-Tool/releases/latest";
        private string checkUrl = @"https://raw.githubusercontent.com/astron8t-voyagerx/ArcRaiders-Color-Tool/main/version";
        public UpdateNotifier(Version current)
        {
            InitializeComponent();
            this.current = current;
            this.CurrentVersionLabel.Text = current.ToString();
            CheckUpdate();
        }

        private async void CheckUpdate()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(checkUrl);
                    response.EnsureSuccessStatusCode();

                    string version = await response.Content.ReadAsStringAsync();
                    this.LatestVersionLabel.Text = version;
                    latest = new Version(version);
                    if(latest > current)
                    {
                        this.ShowDialog();
                    }
                }
                catch (Exception)
                {
                    // when response returns error or invalid string
                    this.Text = "Update Check Failure";
                    UpdateNotifyLabel.Text = "Update Check Failure\nPlease Visit Github Repository";
                    latestLabel.Visible = LatestVersionLabel.Visible = false;
                    UpdateButton.Text = "Visit";
                    downloadUrl = @"https://github.com/astron8t-voyagerx/ArcRaiders-Color-Tool";
                    this.ShowDialog();
                }
            }
        }

        private void UpdateCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(downloadUrl);
            Application.Exit();
        }
    }
}
