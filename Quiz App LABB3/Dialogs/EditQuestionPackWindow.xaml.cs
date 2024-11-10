﻿using System;
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
using System.Windows.Shapes;

namespace Quiz_App_LABB3.Dialogs
{
    /// <summary>
    /// Interaction logic for EditQuestionPackWindow.xaml
    /// </summary>
    public partial class EditQuestionPackWindow : Window
    {
        public EditQuestionPackWindow()
        {
            InitializeComponent();
            DataContext = (App.Current.MainWindow as MainWindow).DataContext;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            LimitLabel.Content = $"{TimeSlider.Value} seconds";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //private void CreateNewPack(object obj)
        //{

        //}
    }
}
