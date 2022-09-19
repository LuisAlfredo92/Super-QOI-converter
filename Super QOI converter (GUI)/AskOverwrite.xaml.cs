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

namespace Super_QOI_converter__GUI_
{
    /// <summary>
    /// Lógica de interacción para AskOverwrite.xaml
    /// </summary>
    public partial class AskOverwrite : Window
    {
        public enum Options // If DialogOptions is false, that means Skip
        {
            Rename,
            Overwrite
        }

        public string FileName
        {
            set => MsgTextBlock.Text = $"{value} already exists.{Environment.NewLine}What do you want to do?";
        }

        public Options SelectedOption;

        public AskOverwrite()
        {
            InitializeComponent();
        }

        private void BtnPressed(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button)!.Content)
            {
                case "Rename":
                    DialogResult = true;
                    SelectedOption = Options.Rename;
                    return;

                case "Skip":
                    DialogResult = false;
                    return;

                case "Overwrite":
                    DialogResult = true;
                    SelectedOption = Options.Overwrite;
                    return;
            }
        }

        private void OnClosingWindow(object sender, System.ComponentModel.CancelEventArgs e) =>
            DialogResult ??= false;
    }
}