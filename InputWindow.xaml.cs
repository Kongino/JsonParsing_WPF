using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JsonParsing_WPF
{
    /// <summary>
    /// Input.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InputWindow : Window
    {
        public InputWindow()
        {
            InitializeComponent();
        }
        public static DailyTemp dailyTemp;


        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            dailyTemp = new DailyTemp(DateBox.Text, int.Parse(HighBox.Text), int.Parse(LowBox.Text), int.Parse(AverageBox.Text));
            ((MainWindow)Application.Current.MainWindow).TempAdd(dailyTemp);
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
