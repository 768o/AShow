using System;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace AShow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] Codes;
        public MainWindow()
        {
            InitializeComponent();
            this.Width = double.Parse(ConfigurationManager.AppSettings["width"]);
            this.Height = double.Parse(ConfigurationManager.AppSettings["height"]);
            Codes = ConfigurationManager.AppSettings["codes"].ToString().Split(',');
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(SetText);
            dispatcherTimer.Interval = new TimeSpan(0, 0, int.Parse(ConfigurationManager.AppSettings["interval"]));
            dispatcherTimer.Start();
        }

        private void SetText(object sender, EventArgs e)
        {
            DebugBoxText.Text = "";
            var rows = "";
            foreach (var code in Codes)
            {
                var data = SinaHQ.GetRealTimeData(code);
                var row = data.Name + "(" + code + ")" + "," + data.CurrentPrice + "(" + (((double.Parse(data.CurrentPrice)
                - double.Parse(data.YesterdayClosingPrice)) / double.Parse(data.YesterdayClosingPrice)) * 100).ToString("0.00") + "%)"
                + "\n" + data.Date + " " + data.Time + "\n";
                rows += row;
            }
            DebugBoxText.Text = rows;
        }

        private void Window_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
