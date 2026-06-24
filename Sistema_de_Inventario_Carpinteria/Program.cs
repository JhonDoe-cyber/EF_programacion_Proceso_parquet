using System;
using System.Collections.Generic;

using BibliotecaInventario;
using BibliotecaBusqueda;
using BibliotecaVentas;
using BibliotecaReportes;

namespace Sistema_de_Inventario_Carpinteria
{
    class Program
    {
        // Arreglos del inventario (Tamaño 100)
        static string[] codProd = new string[100];
        static string[] nomProd = new string[100];
        static double[] preCompra = new double[100];
        static double[] preVenta = new double[100];
        static int[] stock = new int[100];
        static string[] ubicacion = new string[100];

        static int totalProductos = 0;

        // Historiales distribuidos para el desglose detallado de boletas
        static List<string> historialBoletas = new List<string>();
        static List<string> historialProdVendidos = new List<string>();
        static List<int> historialCantidades = new List<int>();
        static List<decimal> historialVentas = new List<decimal>();

        // Instancias de Bibliotecas de Clases (DLL)
        static Inventario inventarioDLL = new Inventario();
        static Busqueda busquedaDLL = new Busqueda();
        static Ventas ventasDLL = new Ventas();
        static Reportes reportesDLL = new Reportes();

        static void Main(string[] args)
        {
            // 1. Carga primero los datos guardados por el usuario
            inventarioDLL.CargarDatos(
                ref totalProductos,
                codProd,
                nomProd,
                preCompra,
                preVenta,
                stock,
                ubicacion);

            // 2. Si hay pocos productos o está vacío, agregamos los 20 automáticos base
            if (totalProductos < 20)
            {
                CargarBase20Productos();
            }

            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("=================================");
                Console.WriteLine("   SISTEMA DE INVENTARIO - CARPINTERÍA");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Registrar Producto");
                Console.WriteLine("2. Buscar / Listar Productos");
                Console.WriteLine("3. Realizar Venta");
                Console.WriteLine("4. Reporte de Ventas Detallado");
                Console.WriteLine("5. Guardar y Salir");
                Console.WriteLine("=================================");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = 0;
                }

                switch (opcion)
                {
                    case 1:
                        RegistrarProducto();
                        break;
                    case 2:
                        BuscarProducto();
                        break;
                    case 3:
                        RealizarVenta();
                        break;
                    case 4:
                        MostrarReporteVentas();
                        break;
                    case 5:
                        GuardarDatos();
                        Console.WriteLine("Datos sincronizados. Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Intente de nuevo.");
                        break;
                }

                if (opcion != 5)
                {
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 5);
        }

        static void RegistrarProducto()
        {
            inventarioDLL.RegistrarProducto(
                ref totalProductos,
                codProd,
                nomProd,
                preCompra,
                preVenta,
                stock,
                ubicacion);
        }

        static void BuscarProducto()
        {
            busquedaDLL.BuscarProducto(
                totalProductos,
                codProd,
                nomProd,
                preCompra,
                preVenta,
                stock,
                ubicacion);
        }

        static void RealizarVenta()
        {
            ventasDLL.RealizarVenta(
                ref totalProductos,
                codProd,
                nomProd,
                preVenta,
                stock,
                historialBoletas,
                historialProdVendidos,
                historialCantidades,
                historialVentas);
        }

        static void MostrarReporteVentas()
        {
            reportesDLL.MostrarReporteVentas(
                historialBoletas,
                historialProdVendidos,
                historialCantidades,
                historialVentas);
        }

        static void GuardarDatos()
        {
            inventarioDLL.GuardarDatos(
                totalProductos,
                codProd,
                nomProd,
                preCompra,
                preVenta,
                stock,
                ubicacion);
        }

        // Método para rellenar los 20 productos sin sobreescribir lo que ya registraste
        static void CargarBase20Productos()
        {
            string[] nombres = {
                "Tablon de Caoba 2x10", "Plancha Triplay 4mm", "Cola Sintetica 1Gal", "Tornillo Madera 1.5pulg",
                "Clavo con Cabeza 2pulg", "Lija para Madera #80", "Lija para Madera #120", "Barniz Marino Transp.",
                "Tinte para Madera Nogal", "Bisagra de Cangrejo 35mm", "Corredera Telescopica 40cm", "Tirador de Acero Inox",
                "Fondo Blanco Base 1Gl", "Disolvente Thinner 1Lt", "Broca para Madera 3/8", "Formon para Carpintero",
                "Martillo de Uña Estandar", "Flexometro 5 Metros", "Escuadra de Alum. 12pulg", "Masilla para Madera Pino"
            };

            double[] compras = { 45.0, 18.5, 22.0, 0.05, 0.08, 1.20, 1.20, 35.0, 12.5, 2.50, 8.0, 4.50, 28.0, 7.5, 6.0, 14.0, 18.0, 11.0, 15.0, 5.5 };
            double[] ventas = { 65.0, 26.0, 32.0, 0.15, 0.20, 2.00, 2.00, 48.0, 18.0, 4.50, 13.5, 7.50, 39.0, 11.0, 9.5, 22.0, 27.0, 16.5, 23.0, 8.5 };
            int[] stocks = { 15, 40, 12, 500, 800, 60, 75, 10, 15, 100, 30, 50, 8, 20, 14, 6, 5, 12, 7, 18 };
            string[] estantes = { "Pasillo A", "Estante 1", "Zona Liquidos", "Caja Tornillos", "Caja Clavos", "Cajon 3", "Cajon 3", "Estante 4", "Estante 4", "Cajon 1", "Pasillo B", "Cajon 2", "Estante 5", "Zona Liquidos", "Estante Herramientas", "Estante Herramientas", "Estante Herramientas", "Vitrina", "Vitrina", "Cajon 4" };

            for (int i = 0; i < nombres.Length; i++)
            {
                // Solo lo agrega si no excede el límite del arreglo de 100
                if (totalProductos >= 100) break;

                // Validamos que el producto no exista ya por nombre para no duplicar
                bool yaExiste = false;
                for (int j = 0; j < totalProductos; j++)
                {
                    if (nomProd[j].ToUpper() == nombres[i].ToUpper()) yaExiste = true;
                }

                if (!yaExiste)
                {
                    codProd[totalProductos] = "PROD" + (totalProductos + 1).ToString("D3");
                    nomProd[totalProductos] = nombres[i];
                    preCompra[totalProductos] = compras[i];
                    preVenta[totalProductos] = ventas[i];
                    stock[totalProductos] = stocks[i];
                    ubicacion[totalProductos] = estantes[i];
                    totalProductos++;
                }
            }
        }
    }
}