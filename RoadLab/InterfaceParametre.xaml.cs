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
using System.Windows.Shapes;

namespace RoadLab
{
    /// <summary>
    /// Interaction logic for InterfaceParametre.xaml
    /// </summary>
    public partial class InterfaceParametre : Window
    {
        public InterfaceParametre()
        {
            InitializeComponent();
            mark1txtBox.Text = mark1Btn.Content.ToString();
            mark2txtBox.Text = mark2Btn.Content.ToString();
            mark3txtBox.Text = mark3Btn.Content.ToString();
            mark4txtBox.Text = mark4Btn.Content.ToString();
            mark5txtBox.Text = mark5Btn.Content.ToString();
            mark6txtBox.Text = mark6Btn.Content.ToString();
            mark7txtBox.Text = mark7Btn.Content.ToString();
            mark8txtBox.Text = mark8Btn.Content.ToString();
        }

        private void interfaceCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mark1Btn_Click(object sender, RoutedEventArgs e)
        {
            mark1Btn.Content = mark1txtBox.Text;
           
        }

        private void mark2Btn_Click(object sender, RoutedEventArgs e)
        {
            mark2Btn.Content = mark2txtBox.Text;
            
        }

        private void mark3Btn_Click(object sender, RoutedEventArgs e)
        {
            mark3Btn.Content = mark3txtBox.Text;
           
        }

        private void mark4Btn_Click(object sender, RoutedEventArgs e)
        {
            mark4Btn.Content = mark4txtBox.Text;
            
        }

        private void mark5Btn_Click(object sender, RoutedEventArgs e)
        {
            mark5Btn.Content = mark5txtBox.Text;
           
        }

        private void mark6Btn_Click(object sender, RoutedEventArgs e)
        {
            mark6Btn.Content = mark6txtBox.Text;
            
        }


        private void mark7Btn_Click(object sender, RoutedEventArgs e)
        {
            mark7Btn.Content = mark7txtBox.Text;
           
        }

        private void mark8Btn_Click(object sender, RoutedEventArgs e)
        {
            mark8Btn.Content = mark8txtBox.Text;
            
        }

    }
}
