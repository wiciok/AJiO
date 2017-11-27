using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScheduleParserBackend.Factories;
using ScheduleParserBackend.Factories.Interfaces;
using ScheduleParserBackend.Interfaces;

namespace ScheduleParserWPFFrontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ISchedulePlansDirector _schedulePlansDirector;
        private string _pattern = "F112";
        private int _scheduleOffsetInMinutes = 0;
        private const string FacultyWebpage = "http://www.fmi.pk.edu.pl/?page=rozklady_zajec.php";

        public MainWindow()
        {
            InitializeComponent();
            RoomCodeTbx.Text = _pattern;
            ISchedulePlansDirectorFactory directorFactory = new SchedulePlansDirectorFactory();
            _schedulePlansDirector = directorFactory.GetSchedulePlansDirector();
            FacultyWebpageTextBox.Text = FacultyWebpage;
        }

        private void RoomCodeTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            var newText = (sender as TextBox)?.Text;
            if (newText != null)
                _pattern = newText;
        }

        private void OffsetTextBox_TextChanged(object sender, TextChangedEventArgs e)
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
            _schedulePlansDirector.ParseExcel(_pattern);

            SaveButton.IsEnabled = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _schedulePlansDirector.CollectResultsAndAddOffset(_scheduleOffsetInMinutes);

            var pdfOccurencies = _schedulePlansDirector.GetPatternOccurenciesFromPdfParsing();

            MessageBox.Show(pdfOccurencies == 0
                ? "Brak dodatkowych danych z planów zapisanych w plikach pdf do uwzględnienia!"
                : $"Kod sali pojawiał się w planach zapisanych w plikach pdf {pdfOccurencies} razy. Zadbaj o ręczne spradzenie planów");

            try
            {
                _schedulePlansDirector.SerializeAndSaveResults();
                MessageBox.Show("Plik z danymi zapisany pomyślnie!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd podczas zapisywania pliku wynikowego!");
            }
            finally
            {
                Application.Current.Shutdown();
            }          
        }
    }
}
