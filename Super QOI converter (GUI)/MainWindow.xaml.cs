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
using Path = System.IO.Path;

namespace Super_QOI_converter__GUI_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<string> filePathsList;

        public MainWindow()
        {
            InitializeComponent();
            filePathsList = new();
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
            foreach (var filePath in openFileDialog.FileNames)
                filePathsList.Add(filePath);

            UpdateListView();
        }

        private void ClearListBtn_Click(object sender, RoutedEventArgs e)
        {
            filePathsList.Clear();
            UpdateListView();
        }

        private void UpdateListView()
        {
            FilesListView.ItemsSource = null;
            FilesListView.UpdateLayout();
            FilesListView.ItemsSource = filePathsList;
        }
    }
}
