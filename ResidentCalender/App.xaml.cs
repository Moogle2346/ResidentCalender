using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ResidentCalender
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var icon = GetResourceStream(new Uri("calender.ico", UriKind.Relative)).Stream;
            var menu = new System.Windows.Forms.ContextMenuStrip();
            menu.Items.Add("終了", null, Exit_Click);
            var notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Visible = true,
                Icon = new System.Drawing.Icon(icon),
                Text = "タスクトレイ常駐アプリのテストです",
                ContextMenuStrip = menu
            };
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Shutdown();
        }
    }

}
