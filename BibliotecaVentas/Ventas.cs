using System;
using System.Collections.Generic;

namespace BibliotecaVentas
{
    public class Ventas
    {
        public void RealizarVenta(
            ref int totalProductos,
            string[] codProd,
            string[] nomProd,
            double[] preVenta,
            int[] stock,
            List<string> historialBoletas,
            List<string> historialProductos,
            List<int> historialCantidades,
            List<decimal> historialVentas)
        {
            Console.WriteLine("\n=== MÓDULO DE VENTAS ===");
            Console.Write("Código o nombre del producto a vender: ");
            string buscar = Console.ReadLine();

            bool encontrado = false;

            for (int i = 0; i < totalProductos; i++)
            {
                if (codProd[i].ToUpper() == buscar.ToUpper() || nomProd[i].ToUpper() == buscar.ToUpper())
                {
                    encontrado = true;
                    Console.WriteLine($"Producto: {nomProd[i]} | Stock Actual: {stock[i]} | Precio: S/. {preVenta[i]:F2}");

                    // Validación ABET de cantidad con reintento seguro
                    int cantidad = -1;
                    do
                    {
                        Console.Write("Cantidad a vender: ");
                        if (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad <= 0)
                        {
                            Console.WriteLine("[Error] Ingrese una cantidad entera mayor a 0.");
                            cantidad = -1;
                        }
                        else if (cantidad > stock[i])
                        {
                            Console.WriteLine($"[Error] Stock insuficiente. Solo quedan {stock[i]} unidades.");
                            return; // Retorna al menú principal sin romper el flujo
                        }
                    } while (cantidad == -1);

                    // Reducción del inventario en tiempo real
                    stock[i] -= cantidad;
                    decimal totalPagar = (decimal)preVenta[i] * cantidad;
                    Console.WriteLine($"Total a Pagar: S/. {totalPagar:F2}");

                    // Selección del Tipo de Pago
                    int tipoPago = 0;
                    do
                    {
                        Console.WriteLine("\n--- Seleccione Tipo de Pago ---");
                        Console.WriteLine("1. Efectivo");
                        Console.WriteLine("2. YAPE");
                        Console.WriteLine("3. Transferencia Bancaria");
                        Console.Write("Opción: ");

                        if (!int.TryParse(Console.ReadLine(), out tipoPago) || tipoPago < 1 || tipoPago > 3)
                        {
                            Console.WriteLine("[Error] Opción inválida. Seleccione 1, 2 o 3.");
                            tipoPago = 0;
                        }
                    } while (tipoPago == 0);

                    if (tipoPago == 1) // Efectivo
                    {
                        decimal montoCliente = 0;
                        do
                        {
                            Console.Write("Monto entregado por el cliente: S/. ");
                            if (!decimal.TryParse(Console.ReadLine(), out montoCliente) || montoCliente < totalPagar)
                            {
                                Console.WriteLine($"[Error] El monto debe ser numérico y alcanzar o superar los S/. {totalPagar:F2}");
                                montoCliente = 0;
                            }
                        } while (montoCliente == 0);

                        decimal vuelto = montoCliente - totalPagar;
                        Console.WriteLine("\n=================================");
                        Console.WriteLine("[✓] VENTA EXITOSA EN EFECTIVO");
                        Console.WriteLine($"Vuelto a entregar: S/. {vuelto:F2}");
                        Console.WriteLine("=================================");
                    }
                    else // Yape o Transferencia
                    {
                        Console.WriteLine("\n=================================");
                        Console.WriteLine("[✓] VENTA EXITOSA EN LÍNEA");
                        Console.WriteLine("Transacción procesada correctamente.");
                        Console.WriteLine("=================================");
                    }

                    // Registro de la boleta y almacenamiento estructurado (Punto 4)
                    string codigoBoleta = "B" + (historialVentas.Count + 1).ToString("D3"); // Genera B001, B002, etc.
                    historialBoletas.Add(codigoBoleta);
                    historialProductos.Add(nomProd[i]);
                    historialCantidades.Add(cantidad);
                    historialVentas.Add(totalPagar);

                    break;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("Producto no encontrado en la base de datos.");
            }
        }
    }
}