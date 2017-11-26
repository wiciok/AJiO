using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScheduleParserBackend;

namespace ScheduleParserWPFFrontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SchedulePlansDirector _schedulePlansDirector;
        private string _pattern = "F112";
        private int _scheduleOffsetInMinutes = 0;
        private const string FacultyWebpage = "http://www.fmi.pk.edu.pl/?page=rozklady_zajec.php";

        public MainWindow()
        {
            InitializeComponent();
            RoomCodeTbx.Text = _pattern;
            _schedulePlansDirector = new SchedulePlansDirector(new FacultyPageParser(), new ScheduleFilesDownloader(), new PdfParser(), null);
            FacultyWebpageTextBox.Text = FacultyWebpage;
        }

        private void RoomCodeTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            var newText = (sender as TextBox)?.Text;
            if (newText != null)
                _pattern = newText;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(int.TryParse((sender as TextBox)?.Text, out var newValue))
                _scheduleOffsetInMinutes = newValue;
        }

        private async void DownloadPlansButton_Click(object sender, RoutedEventArgs e)
        {
            var plansList = await _schedulePlansDirector.GetSchedulePlansLinks();

            var strBuilder = new StringBuilder();
            foreach (var el in plansList)
            {
                strBuilder.AppendLine(el);
            }

            DownloadedPlansLinksTextBlock.Text = strBuilder.ToString();

            await _schedulePlansDirector.GetSchedulePlansFiles();
            _schedulePlansDirector.ParsePdfs(_pattern);
            //_schedulePlansDirector.ParseExcel();
        }
    }
}
