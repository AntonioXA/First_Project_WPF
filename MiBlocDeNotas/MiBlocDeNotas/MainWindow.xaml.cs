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
using System.IO;
using System.Text;
using System.Windows.Media.Media3D;

namespace MiBlocDeNotas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Con este boton se borran todos los TextBox. Se utiliza el metodo clear();
        private void borrar(object sender, RoutedEventArgs e)
        {
            txtOrigen.Clear();
            txtDestino.Clear();
            txtContenido.Clear();
        }

        //Con este boton se cierra el programa. Se utiliza el metodo close();
        private void BotonSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Con este boton se guarda el texto que tengamos en el textbox grande en el archivo txt que hayamos indicado en el textbox de arriba a la derecha
        private void BotonGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileStream escribir = new FileStream(txtDestino.Text, FileMode.Open);
                escribir.Write(ASCIIEncoding.ASCII.GetBytes(txtContenido.Text), 0, txtContenido.Text.Length - 0);
                escribir.Close();
                MessageBox.Show("Guardado con exito", "Informacio", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (FileNotFoundException ex)
            {
                Foco("El archivo no existe o esta vacio", txtOrigen);
            }
            catch (ArgumentException ex)
            {
                Foco("El nombre del archivo no se puede dejar vacio", txtOrigen);
            }
            catch (UnauthorizedAccessException ex)
            {
                Foco("No tienes permisos para modificar el archivo", txtOrigen);
            }
            catch (IOException ex)
            {
            }
        }

        //Con este metodo se escribe el texto en el textbox grande que haya en el archivo txt que se indice en el textbox de arriba a la izquierda
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileStream leer = new FileStream(txtOrigen.Text, FileMode.Open);
                byte[] infoArchivo = new byte[100];
                leer.Read(infoArchivo, 0, (int)leer.Length);
                txtContenido.Text = ASCIIEncoding.ASCII.GetString(infoArchivo);
                leer.Close();
            }
            catch(FileNotFoundException ex)
            {
                Foco("El archivo no existe o esta vacio", txtOrigen);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                Foco("El archivo es demasiado grande", txtOrigen);
            }
            catch(ArgumentException ex)
            {
                Foco("El nombre del archivo no se puede dejar vacio", txtOrigen);
            }
            catch(UnauthorizedAccessException ex)
            {
                Foco("No tienes permisos para modificar el archivo", txtOrigen);
            }
            catch (IOException ex)
            {
            }
           
            
        }

        private void Foco(String error, TextBox textbox)
        {
            MessageBox.Show(error, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            textbox.Focus();
        }
    }
}
