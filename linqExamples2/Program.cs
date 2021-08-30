using System;
using System.Linq;
using System.Collections.Generic;

namespace linqExamples2
{
    class Program
    {
        static void Main(string[] args)
        {
		// CREAR UNA LISTA DE TIPO CLIENTE CON 10  CLIENTES
			List<Cliente> clientes = new List<Cliente> {
                new Cliente {idCliente = 1, nombre = "Pedro"},
                new Cliente {idCliente = 2, nombre = "Roberto"},
                new Cliente {idCliente = 3, nombre = "Andres"},
                new Cliente {idCliente = 4, nombre = "Carlos"},
                new Cliente {idCliente = 5, nombre = "Magdalena"},
                new Cliente {idCliente = 6, nombre = "Maria"},
                new Cliente {idCliente = 7, nombre = "Fausto"},
                new Cliente {idCliente = 8, nombre = "Rancio"},
                new Cliente {idCliente = 9, nombre = "Marta"},
                new Cliente {idCliente = 10, nombre = "Karol"}
             };

		// CREAR UNA LISTA DE TIPO FACTURA CON 10  FACTURAS
			List<Factura> facturas = new List<Factura>
			{
                // los que tienen mas son 1 - 2 -9
                // los que tienen menos son 10 -6 - 7
                new Factura{
                    observacion = "ProbCompra 1 productos", 

					idCliente = 10, 
					fecha=Convert.ToDateTime("05/09/2021"), 
					total = 1.5m
                    },
                new Factura{
                    observacion = "Compra 2 productos", 
					idCliente = 6, 
					fecha=Convert.ToDateTime("05/10/2020"), 
					total = 2.5m
                    },
                new Factura{
                    observacion = "Compra 3 productos", 
					idCliente = 10, 
					fecha=Convert.ToDateTime("05/02/2021"), 
				    total = 3.5m
                    },
                new Factura{
                    observacion = "ProbCompra 4 productos", 
					idCliente = 7, 
					fecha=Convert.ToDateTime("05/05/2012"), 
				    total = 4.5m
                    },
                new Factura{
                    observacion = "Compra 5 productos", 
					idCliente = 9, 
					fecha=Convert.ToDateTime("05/12/2021"), 
					total = 5.5m
                    },
                new Factura{
                    observacion = "Compra 6 productos", 
					idCliente = 5, 
					fecha=Convert.ToDateTime("05/05/2010"), 
					total = 6.5m
                    },
                new Factura{
                    observacion = "ProbCompra 7 productos", 
					idCliente = 9, 
					fecha=Convert.ToDateTime("05/05/2005"), 
					total = 7.5m
                    },
                new Factura{
                    observacion = "Compra 8 productos", 
					idCliente = 9, 
				    fecha=Convert.ToDateTime("05/12/2008"), 
					total = 8.5m
                    },
                new Factura{
                    observacion = "Compra 9 productos", 
					idCliente = 2, 
					fecha=Convert.ToDateTime("05/08/2020"), 
					total = 9.5m
                    },
                new Factura{
                    observacion = "Compra 10 productos", 
					idCliente = 1, 
					fecha=Convert.ToDateTime("05/05/2009"), 
				    total = 10.5m
                    }
			};

			int op = 0;
			do
            {
				Console.WriteLine("SELECCIONE UNA OPCION DEL MENU");
				Console.WriteLine("1.- Mostrar los 3 clientes con mas monto en ventas\n"
                + "2.- Mostrar los 3 clientes con menos monto en ventas\n"
                + "3.- Mostrar el cliente con mas ventas realizadas\n"
                + "4.- Mostrar el cliente y la cantidad de ventas realizadas\n"
                + "5.- Mostrar las ventas realizadas hace menos de un anio\n"
                + "6.- Mostrar las ventas mas antiguas\n"
                + "7.- Mostrar los clientes que tengan una venta cuya observacion comience con 'Prob'\n"
                + "8.- Salir");
				Console.Write("Opcion: ");
				op = Convert.ToInt32(Console.ReadLine());
				Console.WriteLine("\n");

                switch(op){
                    case 1:
						masMontoVentas(facturas, clientes);
						break;
                    case 2:
						menosMontoVentas(facturas, clientes);
						break;
                    case 3:
						clienteConMasVentas(facturas, clientes);
						break;
                    case 4:
						clienteYVentasRealizadas(facturas, clientes);
						break;
                    case 5:
						ventasMenorAnio(facturas, clientes);
						break;
                    case 6:
						ventasAntiguas(facturas, clientes);
						break;
                    case 7:
						clientesObservacion(facturas, clientes);
						break;
                    case 8:
						break;
				}

			} while (op != 8);

		}



		// tomar la lista facturas y ordenarla en orden descendente, tomar los tres primeros,
		// unir con la lista de clientes y crear nuevas propiedades de facutura y cliente.
		// Recorrer la lista principal
		static void masMontoVentas(List<Factura> facturas, List<Cliente> clientes){
			Console.WriteLine("Los 3 clientes con mas monto en ventas: ");
			var listaClientes = facturas.
				OrderByDescending(factura => factura.total)
				.Take(3)
				.Join(clientes,
				factura => factura.idCliente,
				cliente => cliente.idCliente,
				(factura, cliente) =>
			    new
				{ 

                idClienteFactura = factura.idCliente, 
                nombreCliente = cliente.nombre 
			});

			foreach(var actual in listaClientes){
				Console.WriteLine("Cliente id: {0}", actual.idClienteFactura);
				Console.WriteLine("Nombre: {0}", actual.nombreCliente);
				Console.WriteLine();
			}
			Console.WriteLine();
		}

		// agrupar los datos de la lista de facturas por el ID, ordenarlos, 
		// otmar los tres primeros, recorrer cada grupo e impirmir los valores 
    static void menosMontoVentas(List<Factura> facturas, List<Cliente> clientes){
      Console.WriteLine("Los 3 clientes con menos monto en ventas: ");
			IEnumerable<IGrouping<int, decimal>> grupoMontos = facturas
			.GroupBy(id => id.idCliente, total => total.total)
            .OrderBy(total => total.Sum())
            .Take(3);
		
			foreach(IGrouping<int, decimal> grupo in grupoMontos){
				var listaClientes = grupo.Join(clientes,
                    id => grupo.Key,
			        idNombre => idNombre.idCliente,
			        (factura, cliente) => 
                    new

					{
						idFactura = grupo.Key,
						nombreCli = cliente.nombre,
						totalMonto = grupo.Sum()
			        }); 																	                          

        foreach(var seleccionado in listaClientes){
					Console.WriteLine("Id cliente: {0}", seleccionado.idFactura);
					Console.WriteLine("Nombre del cliente: {0}", seleccionado.nombreCli);
					Console.WriteLine("Monto: {0}", seleccionado.totalMonto);
					break;
				}

        Console.WriteLine();
				
			}

			Console.WriteLine();

    }


		// agrupar los datos de la lista de facturas por el ID, ordenarlos por el total de la suma de cada grupo, 
		// otmar los tres primeros, recorrer cada grupo e impirmir los valores 
    private static void clienteConMasVentas(List<Factura> facturas, List<Cliente> clientes)
		{
			IEnumerable<IGrouping<int, decimal>> grupoMontos = facturas
			.GroupBy(id => id.idCliente, total => total.total)
			.OrderBy(total => total.Sum());
			
			// descomentar para obtner el valor maximo de ventas
			//var max = grupoMontos.Max(grupo => grupo.Sum());

			// obtener el ultimo grupo generado
			var ultimoGrupo = grupoMontos.Last();

			// recorrer la lista de clientes donde el ID del ultimo grupo sea igual al ID del cliente
			var seleccionado = clientes
				
				.Where(cliente => cliente.idCliente == ultimoGrupo.Key)
				.Select(cliente => cliente)
				.FirstOrDefault();

			Console.WriteLine("ID: {0}", seleccionado.idCliente);
			Console.WriteLine("Nombre: {0}", seleccionado.nombre);
			Console.WriteLine();
		}

		// agrupar los datos de la lista facutra por ID y retornado un grupo de los totales
		// ordenarlos por el ID
		private static void clienteYVentasRealizadas(List<Factura> facturas, List<Cliente> clientes)
		{
			IEnumerable<IGrouping<int, decimal>> grupoMontos = facturas
				.GroupBy(id => id.idCliente, total => total.total)
				.OrderBy(id => id.Key);

			// recorrer cada grupo
			foreach(IGrouping<int, decimal> grupo in grupoMontos){
				//unir cada grupo con la lista de clientes donde los ID sean iguales
				var listaClientes = grupo.Join(clientes,

					idFactura => grupo.Key,
					idCliente => idCliente.idCliente,
					(factura, cliente) => 
					// crear nuevas propiedades resultado de la union
					new {
					idCliente = grupo.Key,
					nombreCliente = cliente.nombre,
					montoTotal = grupo.Sum()

					});
				// recorrer la nueva lista accediendo a las nuevas propiedades
				foreach(var seleccionado in listaClientes){
					Console.WriteLine("ID: {0}", seleccionado.idCliente);
					Console.WriteLine("Nombre: {0}", seleccionado.nombreCliente);
					Console.WriteLine("Monto total: {0}", seleccionado.montoTotal);
					break;
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}
		private static void ventasMenorAnio(List<Factura> facturas, List<Cliente> clientes)
		{
			// creando una nueva lista de la lista facturas donde cada año sea mayor que la fecha actual
			// restado un año 
			IEnumerable<Factura> ventasRecientes = facturas

				.Where(factura => factura.fecha > (DateTime.Today.AddYears(-1)))
				.Select(date => date);

			// recorrer la lista con que cumplan con la condicion 
			foreach(Factura actual in ventasRecientes){
				Console.WriteLine("ID: {0}", actual.idCliente);
				Console.WriteLine("Fecha: {0}", actual.fecha);
				Console.WriteLine();
			}
			Console.WriteLine();
		}
		private static void ventasAntiguas(List<Factura> facturas, List<Cliente> clientes)
		{
			// craer una lista recorriendo la lista de facturas y ordenarlas por ascendentemente por la fecha
			// tomar los 5 primeros elementos
			// nuevnamente ordenar los 5 elementos descentemente por la fecha
			IEnumerable<Factura> grupoFechas = facturas
				.OrderBy(date => date.fecha)
				.Take(5)
				.OrderByDescending(date => date.fecha);

			// recorrer la lista con los elementos seleccionados y imprimir sus propiedades
			foreach(Factura actual in grupoFechas){
				Console.WriteLine("Fecha: {0}", actual.fecha);
				Console.WriteLine("Descripcion: {0}", actual.observacion);
				Console.WriteLine();
			}
			Console.WriteLine();
		}
		private static void clientesObservacion(List<Factura> facturas, List<Cliente> clientes)
		{
			Console.WriteLine("Clientes cuya observacion empieza con 'Prob'");
			// crear una lista agrupando los lementos de la lista factura por el ID 
			// y nos retorna un grupo con las observaciones de cada grupo, 
			// ordenarlos
			IEnumerable<IGrouping<int, string>> grupoClientes = facturas
				.GroupBy(id => id.idCliente, factura => factura.observacion)
				.OrderBy(id => id.Key);

			// unir cada grupo con los ID que coincidan de la lista de clientes 

			foreach(IGrouping<int, string> grupo in grupoClientes){
				var listaClientes = grupo.Join(clientes,
					
					id => grupo.Key,
					idCliente => idCliente.idCliente,
					(factura, cliente) =>
					// crear nuevas propiedades a partir de esta union
					new {
					idCliente = grupo.Key,
					nombreCliente = cliente.nombre,
					observacionCompra = factura
					})
					// seleccionar los elementos de cada grupo que inicie con  Prob
																		.Where(observacion => observacion.observacionCompra.StartsWith("Prob"))
																		.Select(observacion => observacion);

				// recoorer la nueva lista accediendo a las nuevas propiedades
				foreach(var seleccionado in listaClientes){
						Console.WriteLine("ID: {0}", seleccionado.idCliente);
						Console.WriteLine("Nombre: {0}", seleccionado.nombreCliente);
						Console.WriteLine("Observacion: {0}", seleccionado.observacionCompra);
						Console.WriteLine();
						break;
				}
			}
			Console.WriteLine();
		}
  }

    class Cliente {
        public int idCliente { get; set; }
        public string nombre { get; set; }
	}
    class Factura {
        public string observacion { get; set; }
        public int idCliente { get; set; }
        public DateTime fecha { get; set; }
        public decimal total{ get; set; }
	}
}
