using System;
using System.Collections.Generic;
using System.Text;

namespace InterpolacionNumericaCuadratica
{
    public class Iteracion
    {
        private double error;

        public int I { get; set; }
        public double X0 { get; set; }
        public double Fx0 { get; set; }
        public double X1 { get; set; }
        public double Fx1 { get; set; }
        public double X2 { get; set; }
        public double Fx2 { get; set; }
        public double X3 { get; set; }
        public double Fx3 { get; set; }
        public double Error { get => Math.Round(error, 4); set => error = value; }
    }
}
