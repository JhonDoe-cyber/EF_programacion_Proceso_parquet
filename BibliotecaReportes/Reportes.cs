using System;
using System.Collections.Generic;

namespace BibliotecaReportes
{
    public class Reportes
    {
        public void MostrarReporteVentas(
            List<string> historialBoletas,
            List<string> historialProductos,
            List<int> historialCantidades,
            List<decimal> historialVentas)
        {
            Console.WriteLine("\n=======================================================");
            Console.WriteLine("             REPORTE GENERAL DE VENTAS Detallado");
            Console.WriteLine("=======================================================");

            if (historialVentas.Count == 0)
            {
                Console.WriteLine("No se han registrado transacciones comerciales hoy.");
                return;
            }

            // Encabezados formateados de forma ordenada
            Console.WriteLine(string.Format("{0,-10} {1,-20} {2,-10} {3,-12}", "Boleta", "Producto", "Cant.", "Monto"));
            Console.WriteLine("-------------------------------------------------------");

            decimal totalAcumulado = 0;

            for (int i = 0; i < historialVentas.Count; i++)
            {
                Console.WriteLine(string.Format("{0,-10} {1,-20} {2,-10} S/. {3,-9:F2}",
                    historialBoletas[i],
                    historialProductos[i],
                    historialCantidades[i],
                    historialVentas[i]));

                totalAcumulado += historialVentas[i];
            }

            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine($"RECAUDACIÓN TOTAL DEL DÍA: S/. {totalAcumulado:F2}");
            Console.WriteLine("=======================================================");
        }
    }
}