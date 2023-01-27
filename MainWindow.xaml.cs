using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.ComponentModel;

namespace JsonParsing_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            TempListView.ItemsSource = Temp;
            TempListView.Items.Refresh();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // 화면에 출력할 변수
        private string JTitle = "Default";
        private int CaseNo = -1;
        private string Date = "";
        private string Updated = "";
        private List<DailyTemp> Temp = new List<DailyTemp>();

        // Temp 삭제를 위한 변수
        private int SelectedNo = -1;


        // Binding 연습 겸 Desc 설정
        private string _Desc = "";
        public string Desc
        {
            get { return _Desc; }
            set
            {
                if (_Desc != value)
                {
                    _Desc = value;
                    OnPropertyChanged("Desc");

                }
            }
        }

        public void TempAdd(DailyTemp d)
        {
            // InputWindow에서 추가 시 실행
            Temp.Add(d);
            TempListView.Items.Refresh();
        }

        // 파일 열기 및 수정->저장 경로
        string filePath = "";
        private void LoadBtn(object sender, RoutedEventArgs e)
        {

            //탐색기 Open
            OpenFileDialog dlgOpenFile = new OpenFileDialog();
            dlgOpenFile.Filter = "Json files (*.json) | *.json;*.JSON;*.Json";

            if (dlgOpenFile.ShowDialog().ToString() == "OK")
            {
                // 파일 경로에 맞춰 Json deserialize -> JsonExample 클래스
                string fileName = dlgOpenFile.FileName;
                filePath = fileName;
                string text = File.ReadAllText(fileName);
                JsonExample jsonExample = JsonConvert.DeserializeObject<JsonExample>(text);

                // MainWindow Data Setting
                JTitle = jsonExample.Title;
                CaseNo = jsonExample.CaseNo;
                Date = jsonExample.Date;
                Desc = jsonExample.Desc;
                Updated = jsonExample.Updated;
                Temp = jsonExample.Temp;

                TitleBox.Text = JTitle;
                DateBox.Text = Date;
                UpdatedBox.Text = Updated;

                TempListView.ItemsSource = Temp;
                TempListView.Items.Refresh();
                switch (CaseNo)
                {
                    case 1: CaseNo1.IsChecked = true; break;
                    case 2: CaseNo2.IsChecked = true; break;
                    case 3: CaseNo3.IsChecked = true; break;
                    case 4: CaseNo4.IsChecked = true; break;
                }

                System.Diagnostics.Debug.WriteLine("Load Complete!");
            }
        }
        private void ClearBtn(object sender, RoutedEventArgs e)
        {
            // 임시로 대충 해놓은 새로고침
            MainWindow newWindow = new MainWindow();
            System.Windows.Application.Current.MainWindow = newWindow;
            newWindow.Show();
            this.Close();

        }
        private void SaveBtn(object sender, RoutedEventArgs e)
        {
            // Updated = Today
            Updated = DateTime.Now.ToString("yyyy.MM.dd");

            // jsonExample Setting
            JsonExample jsonExample = new JsonExample(JTitle, CaseNo, Date, Desc, Updated);
            jsonExample.Temp = Temp;


            if (filePath != "")
            {            
                // 원래 파일에 Save
                File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonExample, Formatting.Indented).ToString());
                DateBox.Text = Date;
                UpdatedBox.Text = Updated;
            }
            else
            {
                // 새 파일은 새로 저장
                SaveAsBtn(sender, e);
            }

        }

        private void SaveAsBtn(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlgsaveFile = new SaveFileDialog();
            dlgsaveFile.Filter = "Json files (*.json) | *.json;*.JSON;*.Json";

            if (dlgsaveFile.ShowDialog().ToString() == "OK")
            {
                string saveAsFilePath = dlgsaveFile.FileName;

                // Today
                Date = DateTime.Now.ToString("yyyy.MM.dd");
                Updated = Date;

                JTitle = System.IO.Path.GetFileNameWithoutExtension(saveAsFilePath);

                JsonExample jsonExample = new JsonExample(JTitle, CaseNo, Date, Desc, Updated);
                jsonExample.Temp = Temp;
                if (saveAsFilePath != "")
                {
                    File.WriteAllText(saveAsFilePath, JsonConvert.SerializeObject(jsonExample, Formatting.Indented).ToString());
                    TitleBox.Text = JTitle;
                    DateBox.Text = Date;
                    UpdatedBox.Text = Updated;
                    //저장된 파일 경로로 filePath 변경
                    filePath = saveAsFilePath;
                }
            }
        }
        private void AddBtn(object sender, RoutedEventArgs e)
        {
            // Input Window Open
            InputWindow input = new InputWindow();
            input.Show();

        }
        private void DeleteBtn(object sender, RoutedEventArgs e)
        {
            if (SelectedNo != -1)
            {
                // 선택된 TodayTemp 하나 삭제
                Temp.RemoveAt(SelectedNo);
                TempListView.Items.Refresh();
            }
        }

        private void CaseNoChecked(object sender, RoutedEventArgs e)
        {
            if (CaseNo1.IsChecked == true) CaseNo = 1;
            else if (CaseNo2.IsChecked == true) CaseNo = 2;
            else if (CaseNo3.IsChecked == true) CaseNo = 3;
            else CaseNo = 4;
        }
        private void lstTemps_SelectionChanged(object sender, RoutedEventArgs e)
        {
            SelectedNo = TempListView.SelectedIndex;
        }

    }
}
