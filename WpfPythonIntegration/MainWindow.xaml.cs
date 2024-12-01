using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfPythonIntegration
{
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();

            // Asignar eventos para controlar el placeholder
            MessageTextBox.GotFocus += MessageTextBox_GotFocus;
            MessageTextBox.LostFocus += MessageTextBox_LostFocus;

            // Verificar si inicialmente hay texto o no para mostrar el placeholder
            MessageTextBox_LostFocus(null, null);
        }

        private void MessageTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PlaceholderTextBlock.Visibility == Visibility.Visible)
            {
                PlaceholderTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void MessageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(MessageTextBox.Text))
            {
                PlaceholderTextBlock.Visibility = Visibility.Visible;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageTextBox.Text;

            if (!string.IsNullOrEmpty(message))
            {
                await EnviarMensajeAlServidorAsync(message);
            }
            else
            {
                MessageBox.Show("Por favor, escribe un mensaje.");
            }
        }

        private async Task EnviarMensajeAlServidorAsync(string mensaje)
        {
            try
            {
                var content = new StringContent($"{{\"mensaje\":\"{mensaje}\"}}", Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("http://127.0.0.1:5000/mensaje", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    OutputTextBox.Text = $"Respuesta del servidor: {responseString}";
                }
                else
                {
                    OutputTextBox.Text = $"Error al comunicarse con el servidor: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
