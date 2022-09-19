using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Core;
using Microsoft.Win32;

namespace Super_QOI_converter__GUI_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IOptionsConfirmation
    {
        private bool? _isfolderConfirmed;

        public MainWindow()
        {
            InitializeComponent();
            FilesListView.LayoutUpdated += UnlockButtons;
        }

        public bool ConfirmCopy(string originalFile = "") => CopyAttributesCheckBox.IsChecked!.Value;

        public bool ConfirmDeletion(string originalFile = "") => DeleteOriginalFilesCheckBox.IsChecked!.Value;

        public bool ConfirmOverwrite(ref string existingFile)
        {
            switch (OverwriteComboBox.SelectedIndex)
            {
                case 0: // Ask
                    var messageBox = new AskOverwrite { FileName = existingFile };
                    if (!messageBox.ShowDialog()!.Value)
                        return false;

                    if (messageBox.SelectedOption == AskOverwrite.Options.Rename)
                        existingFile = existingFile.Insert(existingFile.Length - 4, "_copy");
                    return true;

                case 1: // Skip
                    return false;

                case 2: // Rename
                    existingFile = existingFile.Insert(existingFile.Length - 4, "_copy");
                    return true;

                case 3: // overwrite
                    return true;

                default:
                    return true;
            }
        }

        public void ManageDirectory(string directoryPath) => FilesListView.Items.Add(Directory.GetFiles(directoryPath));

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

        private void StartConversionBtn_OnClick(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < FilesListView.Items.Count;)
            {
                var item = (string)FilesListView.Items[i];
                if (Converter.ConvertToQoi(this, item))
                    FilesListView.Items.Remove(item);
                else
                    i++;
            }
        }
    }
}