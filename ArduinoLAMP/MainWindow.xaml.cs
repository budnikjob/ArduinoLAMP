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
using System.IO.Ports;
using System.IO;
using System.Xml;

namespace ArduinoLAMP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort serialport = new SerialPort();
        string[] ports = SerialPort.GetPortNames(); 

        public MainWindow()
        {
            InitializeComponent();
            com_input.ItemsSource = ports; 
        }

        private void com_input_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (serialport.IsOpen)
            {
                serialport.Close();
            }
            else
            {
                try
                {
                    serialport.PortName = com_input.SelectedItem as string;
                    serialport.BaudRate = 9600;
                    serialport.Open();
                    MessageBox.Show("Порт открыт");
                }
                catch
                {
                    MessageBox.Show("Порт используется другим приложением!");
                }
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (my_temp.IsChecked == false)
            {
                int br_i = (int)brightness.Value;
                string a = 1 + kelvin_var.Text + br_i;
                serialport.Write(a);
                MessageBox.Show(a);
            }
            else
            {
                int br_i = (int)brightness.Value;
                string a = 1 + temp_input.Text + br_i;
                serialport.Write(a);
                MessageBox.Show(a);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            serialport.Write("off");
        }

        private void Button_Click_Rainbow(object sender, RoutedEventArgs e)
        {
            serialport.Write("Rainbow");
        }

        private void Button_Click_dot(object sender, RoutedEventArgs e)
        {
            serialport.Write("Dot");
        }
    }
}
