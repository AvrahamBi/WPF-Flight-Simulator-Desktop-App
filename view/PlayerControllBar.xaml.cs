using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlightSimulator2.viewModel;
using FlightSimulator2.model;

namespace FlightSimulator2.view
{
    /// <summary>
    /// Interaction logic for PlayerControllBar.xaml
    /// </summary>
    public partial class PlayerControllBar : UserControl
    {
        PlayerControllBarVM control_bar_vm;
        public PlayerControllBar()
        {
            InitializeComponent();
            control_bar_vm = new PlayerControllBarVM(new PlayerControlBarM(100));
            Client.client_instance.Disconnect();
            Client.client_instance.setControlBar(control_bar_vm.controlBar);
            DataContext = control_bar_vm;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void diconnect_Click(object sender, RoutedEventArgs e)
        {
            Client.client_instance.Disconnect();

        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if (string.Equals(playOrPause.Content, "Pause"))
            {
                control_bar_vm.VM_play_or_pause = false;
                playOrPause.Content = "Play";
            }
            else {
            control_bar_vm.VM_play_or_pause = true;
            playOrPause.Content = "Pause";
                }
        }
    }
}
