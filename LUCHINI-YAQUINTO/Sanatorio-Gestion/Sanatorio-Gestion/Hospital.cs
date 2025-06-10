using System;
using System.Collections.Generic;
using System.Linq;
public class Hospital
{
    public List<Paciente> Pacientes { get; set; } = new List<Paciente>();
    public List<Doctor> Doctores { get; set; } = new List<Doctor>();
    public List<Intervencion> Intervenciones { get; set; } = new List<Intervencion>();
    private int proximoIdIntervencion = 1;

    public void AgregarPaciente(Paciente paciente)
    {
        Pacientes.Add(paciente);
    }

    public Paciente BuscarPacientePorDNI(string dni)
    {
        return Pacientes.FirstOrDefault(p => p.DocumentoIdentidad == dni);
    }

    public List<Paciente> ListarPacientes()
    {
        return Pacientes;
    }

    public void AsignarIntervencionAPaciente(string dni, DateTime fecha, string codigoIntervencion, string matriculaMedico)
    {
        var paciente = BuscarPacientePorDNI(dni);
        if (paciente == null)
        {
            throw new Exception("Paciente no encontrado");
        }

        var intervencion = Intervenciones.FirstOrDefault(i => i.Codigo == codigoIntervencion);
        if (intervencion == null)
        {
            throw new Exception("Intervención no encontrada");
        }

        var medico = Doctores.FirstOrDefault(d => d.Matricula == matriculaMedico);
        if (medico == null)
        {
            throw new Exception("Médico no encontrado");
        }

        if (medico.Especialidad != intervencion.Especialidad)
        {
            throw new Exception("El médico no tiene la especialidad requerida para esta intervención");
        }

        if (!medico.Disponible)
        {
            throw new Exception("El médico no está disponible");
        }

        var nuevaIntervencion = new IntervencionRealizada(
            proximoIdIntervencion++,
            fecha,
            intervencion,
            medico
        );

        paciente.AgregarIntervencion(nuevaIntervencion);
    }

    public decimal CalcularCostoIntervencionesPorDNI(string dni)
    {
        var paciente = BuscarPacientePorDNI(dni);
        if (paciente == null)
        {
            throw new Exception("Paciente no encontrado");
        }

        return paciente.CalcularTotalPendientePago();
    }

    public List<dynamic> GenerarReporteLiquidacionesPendientes()
    {
        var reporte = new List<dynamic>();

        foreach (var paciente in Pacientes)
        {
            foreach (var intervencion in paciente.ObtenerIntervencionesPendientes())
            {
                reporte.Add(new
                {
                    Id = intervencion.Id,
                    Fecha = intervencion.Fecha.ToShortDateString(),
                    Descripcion = intervencion.Intervencion.Descripcion,
                    Paciente = paciente.NombreCompleto,
                    Medico = $"{intervencion.Medico.NombreCompleto} (Matrícula: {intervencion.Medico.Matricula})",
                    ObraSocial = paciente.ObraSocial,
                    Importe = intervencion.CalcularCostoFinal(paciente).ToString("C")
                });
            }
        }

        return reporte;
    }
}