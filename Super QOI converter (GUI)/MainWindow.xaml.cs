using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace Super_QOI_converter__GUI_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class FileRow
        {
            private Image StateImage { get; set; }
            private string Path { get; set; }
            private Button DeleteButton { get; set; } 


            public FileRow(string path)
            {
                StateImage = new()
                {
                    MaxHeight = 10,
                    MaxWidth = 10,
                    Height = 8,
                    Width = 8,
                };
                Path = path;
                DeleteButton = new()
                {
                    Width = 10,
                    Content = "Delete"
                };
            }
        }

        private List<FileRow> files;

        public MainWindow()
        {
            InitializeComponent();
            files = new();
        }

        private void AddFilesBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Multiselect = true,
                Title = "Select files to be added",
                Filter = "Supported images|*.jpeg;*.png;*.jpg;*.bmp;*.JPEG;*.PNG;*.JPG;*.BMP"
            };
            openFileDialog.ShowDialog();
            foreach (var file in openFileDialog.FileNames)
                files.Add(new FileRow(file));

            FilesListView.ItemsSource = files;
        }
    }
}
