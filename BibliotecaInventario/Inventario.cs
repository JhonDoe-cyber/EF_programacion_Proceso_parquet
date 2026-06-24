using System;
using System.IO;

namespace BibliotecaInventario
{
    public class Inventario
    {
        public void RegistrarProducto(
            ref int totalProductos,
            string[] codProd,
            string[] nomProd,
            double[] preCompra,
            double[] preVenta,
            int[] stock,
            string[] ubicacion)
        {
            Console.WriteLine("\n=== REGISTRO DE NUEVO PRODUCTO ===");

            if (totalProductos >= 100)
            {
                Console.WriteLine("[ERROR] Límite de memoria alcanzado (Capacidad máxima: 100 productos).");
                return;
            }

            string codigo;
            bool codigoDuplicado;

            // Bucle de validación estricta para el Código (Garantiza que no esté vacío y que sea ÚNICO)
            do
            {
                codigoDuplicado = false;
                Console.Write("Código: ");
                codigo = Console.ReadLine();

                // 1. Validar que no se escriban espacios en blanco o se deje vacío
                if (string.IsNullOrWhiteSpace(codigo))
                {
                    Console.WriteLine("[ERROR] El código no puede estar vacío o contener solo espacios.");
                    codigoDuplicado = true; // Forzar reintento
                    continue;
                }

                // 2. Validar que el código NO exista ya en el sistema (Evita duplicados)
                for (int i = 0; i < totalProductos; i++)
                {
                    if (codProd[i].ToUpper() == codigo.ToUpper())
                    {
                        Console.WriteLine($"[ERROR] El código '{codigo.ToUpper()}' ya está registrado para el producto '{nomProd[i]}'. Ingrese un código único.");
                        codigoDuplicado = true;
                        break; // Sale del for para solicitar un nuevo código
                    }
                }

            } while (codigoDuplicado);

            // Si el código pasó todas las pruebas, se guarda de forma segura
            codProd[totalProductos] = codigo.ToUpper();

            // Validación del Nombre (que tampoco se quede vacío)
            string nombre;
            do
            {
                Console.Write("Nombre: ");
                nombre = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    Console.WriteLine("[ERROR] El nombre del producto no puede estar vacío.");
                }
            } while (string.IsNullOrWhiteSpace(nombre));
            nomProd[totalProductos] = nombre;

            // Validación de Precio Compra
            double pc;
            do
            {
                Console.Write("Precio Compra: S/. ");
                if (!double.TryParse(Console.ReadLine(), out pc) || pc <= 0)
                {
                    Console.WriteLine("[ERROR] Debe ser un número real estrictamente mayor a 0.");
                    pc = -1;
                }
            } while (pc <= 0);
            preCompra[totalProductos] = pc;

            // Validación de Precio Venta
            double pv;
            do
            {
                Console.Write("Precio Venta: S/. ");
                if (!double.TryParse(Console.ReadLine(), out pv) || pv <= 0)
                {
                    Console.WriteLine("[ERROR] Debe ser un número real estrictamente mayor a 0.");
                    pv = -1;
                }
            } while (pv <= 0);
            preVenta[totalProductos] = pv;

            // Validación de Stock
            int s;
            do
            {
                Console.Write("Stock Inicial: ");
                if (!int.TryParse(Console.ReadLine(), out s) || s < 0)
                {
                    Console.WriteLine("[ERROR] Ingrese un entero no negativo (>= 0).");
                    s = -1;
                }
            } while (s < 0);
            stock[totalProductos] = s;

            Console.Write("Ubicación en Almacén: ");
            ubicacion[totalProductos] = Console.ReadLine();

            totalProductos++;
            Console.WriteLine("[✓] Producto indexado correctamente y sin duplicación.");
        }

        public void GuardarDatos(int totalProductos, string[] codProd, string[] nomProd, double[] preCompra, double[] preVenta, int[] stock, string[] ubicacion)
        {
            try
            {
                using (StreamWriter archivo = new StreamWriter("productos.txt"))
                {
                    for (int i = 0; i < totalProductos; i++)
                    {
                        archivo.WriteLine($"{codProd[i]};{nomProd[i]};{preCompra[i]};{preVenta[i]};{stock[i]};{ubicacion[i]}");
                    }
                }
                Console.WriteLine("Archivo 'productos.txt' actualizado con éxito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar archivo: " + ex.Message);
            }
        }

        public void CargarDatos(ref int totalProductos, string[] codProd, string[] nomProd, double[] preCompra, double[] preVenta, int[] stock, string[] ubicacion)
        {
            if (!File.Exists("productos.txt"))
                return;

            try
            {
                using (StreamReader archivo = new StreamReader("productos.txt"))
                {
                    string linea;
                    while ((linea = archivo.ReadLine()) != null && totalProductos < 100)
                    {
                        string[] datos = linea.Split(';');
                        if (datos.Length == 6)
                        {
                            codProd[totalProductos] = datos[0].ToUpper();
                            nomProd[totalProductos] = datos[1];
                            double.TryParse(datos[2], out preCompra[totalProductos]);
                            double.TryParse(datos[3], out preVenta[totalProductos]);
                            int.TryParse(datos[4], out stock[totalProductos]);
                            ubicacion[totalProductos] = datos[5];
                            totalProductos++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar archivo: " + ex.Message);
            }
        }
    }
}