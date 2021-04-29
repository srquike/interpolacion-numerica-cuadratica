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

namespace InterpolacionNumericaCuadratica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IList<Iteracion> _iteraciones;

        public MainWindow()
        {
            InitializeComponent();
        }

        private double EvaluarFuncion(double x)
        {
            return (2 * Math.Sin(x)) - (Math.Pow(x, 2) / 10);
        }

        private double EncontrarX3(double x0, double x1, double x2)
        {
            double numerador = EvaluarFuncion(x0) * (Math.Pow(x1, 2) - Math.Pow(x2, 2)) + EvaluarFuncion(x1) * (Math.Pow(x2, 2) - Math.Pow(x0, 2)) + EvaluarFuncion(x2) * (Math.Pow(x0, 2) - Math.Pow(x1, 2));
            double denominador = 2 * EvaluarFuncion(x0) * (x1 - x2) + 2 * EvaluarFuncion(x1) * (x2 - x0) + 2 * EvaluarFuncion(x2) * (x0 - x1);
            double resultado = numerador / denominador;

            return resultado;
        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TxtValorX0.Text, out double x0) && double.TryParse(TxtValorX1.Text, out double x1) && double.TryParse(TxtValorX2.Text, out double x2))
            {
                int i;
                Iteracion iteracion, iteracionAnterior;

                iteracionAnterior = new Iteracion();
                _iteraciones = new List<Iteracion>();
                i = 0;

                do
                {
                    iteracion = new Iteracion();
                    iteracion.I = i;

                    if (iteracion.I == 0)
                    {
                        iteracion.X0 = x0;
                        iteracion.X1 = x1;
                        iteracion.X2 = x2;
                    }
                    else
                    {
                        if (iteracionAnterior.X3 > iteracionAnterior.X0 && iteracionAnterior.X3 < iteracionAnterior.X1)
                        {
                            iteracion.X0 = iteracionAnterior.X0;
                        }
                        else
                        {
                            iteracion.X0 = iteracionAnterior.X1;
                        }

                        iteracion.X1 = iteracionAnterior.X3;

                        if (iteracionAnterior.X3 > iteracionAnterior.X1 && iteracionAnterior.X3 < iteracionAnterior.X2)
                        {
                            iteracion.X2 = iteracionAnterior.X2;
                        }
                        else
                        {
                            iteracion.X2 = iteracionAnterior.X1;
                        }
                    }

                    iteracion.Fx0 = EvaluarFuncion(iteracion.X0);
                    iteracion.Fx1 = EvaluarFuncion(iteracion.X1);
                    iteracion.Fx2 = EvaluarFuncion(iteracion.X2);
                    iteracion.X3 = EncontrarX3(iteracion.X0, iteracion.X1, iteracion.X2);
                    iteracion.Fx3 = EvaluarFuncion(iteracion.X3);
                    iteracion.Error = Math.Abs((iteracion.X3 - iteracionAnterior.X3) / iteracion.X3) * 100;

                    _iteraciones.Add(iteracion);
                    iteracionAnterior = iteracion;
                    i++;

                } while (iteracion.Error != 0.0001 && iteracion.Error != 0);

                DgIteraciones.ItemsSource = _iteraciones;
            }
            else
            {
                MessageBox.Show("Entradas incorrectas");
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            TxtValorX0.Clear();
            TxtValorX1.Clear();
            TxtValorX2.Clear();
            DgIteraciones.ItemsSource = null;
            TxtValorX0.Focus();
        }
    }
}
