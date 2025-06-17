using System;
using System.Linq;

public class Program
{
    private static Hospital hospital = new Hospital();

    public static void Main(string[] args)
    {
        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE GESTIÓN DEL SANATORIO ===");
            Console.WriteLine("1. Dar de alta un nuevo paciente");
            Console.WriteLine("2. Listar pacientes");
            Console.WriteLine("3. Asignar intervención a paciente");
            Console.WriteLine("4. Calcular costo de intervenciones por DNI");
            Console.WriteLine("5. Reporte de liquidaciones pendientes");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    AltaPaciente();
                    break;
                case "2":
                    ListarPacientes();
                    break;
                case "3":
                    AsignarIntervencion();
                    break;
                case "4":
                    CalcularCostoPorDNI();
                    break;
                case "5":
                    GenerarReportePendientes();
                    break;
                case "6":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    break;
            }

            if (!salir)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    private static void AltaPaciente()
    {
        Console.WriteLine("\n=== ALTA DE NUEVO PACIENTE ===");

        Console.Write("Documento de identidad: ");
        string dni = Console.ReadLine();

        Console.Write("Nombre y apellido: ");
        string nombre = Console.ReadLine();

        Console.Write("Teléfono: ");
        string telefono = Console.ReadLine();

        Console.Write("¿Tiene obra social? (S/N): ");
        bool tieneOS = Console.ReadLine().ToUpper() == "S";

        string obraSocial = null;
        decimal cobertura = 0;

        if (tieneOS)
        {
            Console.Write("Nombre de la obra social: ");
            obraSocial = Console.ReadLine();

            Console.Write("Porcentaje de cobertura (0-100): ");
            decimal.TryParse(Console.ReadLine(), out cobertura);
        }

        var paciente = new Paciente(dni, nombre, telefono, obraSocial, cobertura);
        hospital.Pacientes.Add(paciente);

        Console.WriteLine($"\nPaciente {nombre} registrado exitosamente.");
    }

    private static void ListarPacientes()
    {
        Console.WriteLine("\n=== LISTADO DE PACIENTES ===");
        foreach (var paciente in hospital.Pacientes)
        {
            Console.WriteLine(paciente);
        }
    }

    private static void AsignarIntervencion()
    {
        Console.WriteLine("\n=== ASIGNAR INTERVENCIÓN A PACIENTE ===");

        Console.Write("Documento del paciente: ");
        string dni = Console.ReadLine();

        var paciente = hospital.Pacientes.FirstOrDefault(p => p.DocumentoIdentidad == dni);

        if (paciente == null)
        {
            Console.WriteLine("Paciente no encontrado. Se procederá a dar de alta.");
            AltaPaciente();
            paciente = hospital.Pacientes.Last();
        }

        Console.WriteLine("\nIntervenciones disponibles:");
        foreach (var intervencion in hospital.Intervenciones)
        {
            Console.WriteLine($"{intervencion.Codigo}: {intervencion.Descripcion} ({intervencion.Especialidad}) - ${intervencion.CalcularCosto()}");
        }

        Console.Write("\nCódigo de intervención a asignar: ");
        string codigo = Console.ReadLine();

        var intervencionSeleccionada = hospital.Intervenciones.FirstOrDefault(i => i.Codigo == codigo);
        if (intervencionSeleccionada == null)
        {
            Console.WriteLine("Intervención no válida.");
            return;
        }

        var medicosDisponibles = hospital.Doctores
            .Where(d => d.Especialidad == intervencionSeleccionada.Especialidad && d.Disponible)
            .ToList();

        if (!medicosDisponibles.Any())
        {
            Console.WriteLine("No hay médicos disponibles para esta especialidad.");
            return;
        }

        Console.WriteLine("\nMédicos disponibles:");
        for (int i = 0; i < medicosDisponibles.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {medicosDisponibles[i]}");
        }

        Console.Write("Seleccione un médico: ");
        if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= medicosDisponibles.Count)
        {
            Console.Write("Fecha de la intervención (dd/mm/aaaa): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime fecha))
            {
                var medico = medicosDisponibles[seleccion - 1];
                var intervencionQuirurgica = new IntervencionQuirurgica(fecha, intervencionSeleccionada, medico);
                paciente.AgregarIntervencion(intervencionQuirurgica);

                Console.WriteLine($"\nIntervención asignada exitosamente. ID: {intervencionQuirurgica.Id}");
            }
            else
            {
                Console.WriteLine("Fecha no válida.");
            }
        }
        else
        {
            Console.WriteLine("Selección no válida.");
        }
    }

    private static void CalcularCostoPorDNI()
    {
        Console.WriteLine("\n=== CALCULAR COSTO POR DNI ===");

        Console.Write("Documento del paciente: ");
        string dni = Console.ReadLine();

        var paciente = hospital.Pacientes.FirstOrDefault(p => p.DocumentoIdentidad == dni);
        if (paciente == null)
        {
            Console.WriteLine("Paciente no encontrado.");
            return;
        }

        decimal total = paciente.CalcularTotalPendientePago();
        Console.WriteLine($"\nTotal pendiente de pago para {paciente.NombreApellido}: ${total}");
    }

    private static void GenerarReportePendientes()
    {
        Console.WriteLine("\n=== REPORTE DE LIQUIDACIONES PENDIENTES ===");
        Console.WriteLine("ID\tFecha\tDescripción\tPaciente\tMédico\tObra Social\tImporte");
        Console.WriteLine(new string('-', 100));

        foreach (var paciente in hospital.Pacientes)
        {
            foreach (var intervencion in paciente.ObtenerIntervencionesPendientes())
            {
                Console.WriteLine($"{intervencion.Id}\t" +
                                $"{intervencion.Fecha.ToShortDateString()}\t" +
                                $"{intervencion.Intervencion.Descripcion}\t" +
                                $"{paciente.NombreApellido}\t" +
                                $"{intervencion.Medico.NombreApellido} ({intervencion.Medico.Matricula})\t" +
                                $"{(paciente.ObraSocial ?? "-")}\t" +
                                $"${intervencion.CalcularCostoFinal(paciente)}");
            }
        }
    }
}