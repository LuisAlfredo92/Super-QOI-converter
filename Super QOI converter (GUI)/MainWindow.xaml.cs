using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace Super_QOI_converter__GUI_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            FilesListView.LayoutUpdated += UnlockButtons;
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
            foreach (var filePath in openFileDialog.FileNames.Where(
                         elem => !FilesListView.Items.Contains(elem)))
                FilesListView.Items.Add(filePath);
        }

        private void ClearListBtn_Click(object sender, RoutedEventArgs e)
        {
            FilesListView.Items.Clear();
        }

        private void DelItemBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var pathToDelete = ((sender as Button)!).CommandParameter;
            FilesListView.Items.Remove(pathToDelete);
        }

        private void UnlockButtons(object? sender, EventArgs e)
        {
            if (FilesListView.Items.Count > 0)
                ClearListBtn.IsEnabled = StartConversionBtn.IsEnabled = true;
            else
                ClearListBtn.IsEnabled = StartConversionBtn.IsEnabled = false;
        }
    }
}
