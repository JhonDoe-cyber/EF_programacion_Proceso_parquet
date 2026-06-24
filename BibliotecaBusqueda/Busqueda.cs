using System;

namespace BibliotecaBusqueda
{
    public class Busqueda
    {
        public void BuscarProducto(
            int totalProductos,
            string[] codProd,
            string[] nomProd,
            double[] preCompra,
            double[] preVenta,
            int[] stock,
            string[] ubicacion)
        {
            Console.WriteLine("\n=== PRODUCTOS EN EXISTENCIA ===");
            if (totalProductos == 0)
            {
                Console.WriteLine("El almacén se encuentra vacío.");
            }
            else
            {
                Console.WriteLine(string.Format("{0,-10} {1,-25} {2,-12} {3,-8}", "Código", "Nombre", "Precio V.", "Stock"));
                Console.WriteLine("-----------------------------------------------------------------");
                for (int i = 0; i < totalProductos; i++)
                {
                    Console.WriteLine(string.Format("{0,-10} {1,-25} S/. {2,-9:F2} {3,-8}",
                        codProd[i], nomProd[i], preVenta[i], stock[i]));
                }
            }
            Console.WriteLine("=================================================================\n");

            // Solicitud de búsqueda en la parte inferior
            Console.Write("¿Qué producto desea buscar? (Código o Nombre): ");
            string buscar = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(buscar))
            {
                Console.WriteLine("Entrada inválida. Búsqueda cancelada.");
                return;
            }

            bool encontrado = false;

            for (int i = 0; i < totalProductos; i++)
            {
                if (codProd[i].ToUpper() == buscar.ToUpper() || nomProd[i].ToUpper() == buscar.ToUpper())
                {
                    Console.WriteLine("\n[✓] Producto Encontrado:");
                    Console.WriteLine("Código: " + codProd[i]);
                    Console.WriteLine("Nombre: " + nomProd[i]);
                    Console.WriteLine("Precio Compra: S/. " + preCompra[i].ToString("F2"));
                    Console.WriteLine("Precio Venta: S/. " + preVenta[i].ToString("F2"));
                    Console.WriteLine("Stock Disponible: " + stock[i] + " unidades");
                    Console.WriteLine("Ubicación Física: " + ubicacion[i]);
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("Producto no encontrado en el sistema.");
            }
        }
    }
}