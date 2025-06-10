using System;
using System.Linq;
class Program
{
    static void Main(string[] args)
    {
        var hospital = new Hospital();

        hospital.Doctores.Add(new Doctor("Juan", "Pérez", "MP123", "Cardiología", true));
        hospital.Doctores.Add(new Doctor("María", "Gómez", "MP456", "Traumatología", true));

        hospital.Intervenciones.Add(new Intervencion("INT001", "Bypass coronario", "Cardiología", 50000m, true, 15));
        hospital.Intervenciones.Add(new Intervencion("INT002", "Reemplazo de cadera", "Traumatología", 40000m));

        bool salir = false;

        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("Sistema de Gestión de Intervenciones Quirúrgicas");
            Console.WriteLine("1. Dar de alta un nuevo paciente");
            Console.WriteLine("2. Listar pacientes");
            Console.WriteLine("3. Asignar intervención a paciente");
            Console.WriteLine("4. Calcular costo de intervenciones por DNI");
            Console.WriteLine("5. Reporte de liquidaciones pendientes");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    AltaPaciente(hospital);
                    break;
                case "2":
                    ListarPacientes(hospital);
                    break;
                case "3":
                    AsignarIntervencion(hospital);
                    break;
                case "4":
                    CalcularCostoIntervenciones(hospital);
                    break;
                case "5":
                    GenerarReporte(hospital);
                    break;
                case "6":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void AltaPaciente(Hospital hospital)
    {
        Console.Clear();
        Console.WriteLine("Alta de nuevo paciente");

        Console.Write("Documento de identidad: ");
        var dni = Console.ReadLine();

        Console.Write("Nombre: ");
        var nombre = Console.ReadLine();

        Console.Write("Apellido: ");
        var apellido = Console.ReadLine();

        Console.Write("Teléfono: ");
        var telefono = Console.ReadLine();

        Console.Write("¿Tiene obra social? (S/N): ");
        var tieneOS = Console.ReadLine().ToUpper() == "S";

        string obraSocial = "-";
        decimal montoCobertura = 0;

        if (tieneOS)
        {
            Console.Write("Nombre de la obra social: ");
            obraSocial = Console.ReadLine();

            Console.Write("Monto de cobertura: ");
            decimal.TryParse(Console.ReadLine(), out montoCobertura);
        }

        var paciente = new Paciente(dni, nombre, apellido, telefono, obraSocial, montoCobertura);
        hospital.AgregarPaciente(paciente);

        Console.WriteLine("Paciente registrado con éxito. Presione cualquier tecla para continuar...");
        Console.ReadKey();
    }

    static void ListarPacientes(Hospital hospital)
    {
        Console.Clear();
        Console.WriteLine("Listado de pacientes");
        Console.WriteLine("DNI\t\tNombre y Apellido\tTeléfono\tObra Social");

        foreach (var paciente in hospital.ListarPacientes())
        {
            Console.WriteLine($"{paciente.DocumentoIdentidad}\t{paciente.NombreCompleto}\t{paciente.Telefono}\t{paciente.ObraSocial}");
        }

        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }

    static void AsignarIntervencion(Hospital hospital)
    {
        Console.Clear();
        Console.WriteLine("Asignar intervención a paciente");

        Console.Write("DNI del paciente: ");
        var dni = Console.ReadLine();

        var paciente = hospital.BuscarPacientePorDNI(dni);
        if (paciente == null)
        {
            Console.WriteLine("Paciente no encontrado. ¿Desea darlo de alta? (S/N)");
            if (Console.ReadLine().ToUpper() == "S")
            {
                AltaPaciente(hospital);
                paciente = hospital.BuscarPacientePorDNI(dni);
            }
            else
            {
                return;
            }
        }

        Console.WriteLine("Intervenciones disponibles:");
        foreach (var interv in hospital.Intervenciones)
        {
            Console.WriteLine($"{interv.Codigo}: {interv.Descripcion} ({interv.Especialidad}) - {interv.CalcularCosto():C}");
        }

        Console.Write("Código de intervención: ");
        var codigo = Console.ReadLine();

        Console.WriteLine("Médicos disponibles:");
        foreach (var doc in hospital.Doctores.Where(d => d.Disponible))
        {
            Console.WriteLine($"{doc.Matricula}: {doc.NombreCompleto} ({doc.Especialidad})");
        }

        Console.Write("Matrícula del médico: ");
        var matricula = Console.ReadLine();

        Console.Write("Fecha de intervención (dd/mm/aaaa): ");
        DateTime.TryParse(Console.ReadLine(), out DateTime fecha);

        try
        {
            hospital.AsignarIntervencionAPaciente(dni, fecha, codigo, matricula);
            Console.WriteLine("Intervención asignada con éxito.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
    }

    static void CalcularCostoIntervenciones(Hospital hospital)
    {
        Console.Clear();
        Console.WriteLine("Calcular costo de intervenciones por DNI");

        Console.Write("DNI del paciente: ");
        var dni = Console.ReadLine();

        try
        {
            var total = hospital.CalcularCostoIntervencionesPorDNI(dni);
            Console.WriteLine($"Total pendiente de pago: {total:C}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
    }

    static void GenerarReporte(Hospital hospital)
    {
        Console.Clear();
        Console.WriteLine("Reporte de liquidaciones pendientes");

        var reporte = hospital.GenerarReporteLiquidacionesPendientes();

        if (reporte.Count == 0)
        {
            Console.WriteLine("No hay liquidaciones pendientes.");
        }
        else
        {
            Console.WriteLine("ID\tFecha\tDescripción\tPaciente\tMédico\tObra Social\tImporte");
            foreach (var item in reporte)
            {
                Console.WriteLine($"{item.Id}\t{item.Fecha}\t{item.Descripcion}\t{item.Paciente}\t{item.Medico}\t{item.ObraSocial}\t{item.Importe}");
            }
        }

        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }
}