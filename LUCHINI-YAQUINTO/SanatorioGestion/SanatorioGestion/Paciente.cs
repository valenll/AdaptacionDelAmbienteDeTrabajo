using System.Collections.Generic;
using System;
using System.Linq;
public class Paciente
{
    public string DocumentoIdentidad { get; set; }
    public string NombreApellido { get; set; }
    public string Telefono { get; set; }
    public string ObraSocial { get; set; }
    public decimal MontoCobertura { get; set; }
    public List<IntervencionQuirurgica> IntervencionesRealizadas { get; set; } = new List<IntervencionQuirurgica>();

    public Paciente(string documentoIdentidad, string nombreApellido, string telefono, string obraSocial, decimal montoCobertura)
    {
        DocumentoIdentidad = documentoIdentidad;
        NombreApellido = nombreApellido;
        Telefono = telefono;
        ObraSocial = obraSocial;
        MontoCobertura = montoCobertura;
    }

    public void AgregarIntervencion(IntervencionQuirurgica intervencion)
    {
        IntervencionesRealizadas.Add(intervencion);
    }

    public decimal CalcularTotalPendientePago()
    {
        return IntervencionesRealizadas
            .Where(i => !i.Pagado)
            .Sum(i => i.CalcularCostoFinal(this));
    }

    public List<IntervencionQuirurgica> ObtenerIntervencionesPendientes()
    {
        return IntervencionesRealizadas.Where(i => !i.Pagado).ToList();
    }

    public override string ToString()
    {
        return $"{NombreApellido} (DNI: {DocumentoIdentidad}) - Tel: {Telefono} - OS: {ObraSocial ?? "-"}";
    }
}

public class IntervencionQuirurgica
{
    private static int _contadorId = 1;

    public int Id { get; private set; }
    public DateTime Fecha { get; set; }
    public Intervencion Intervencion { get; set; }
    public Doctor Medico { get; set; }
    public bool Pagado { get; set; }

    public IntervencionQuirurgica(DateTime fecha, Intervencion intervencion, Doctor medico)
    {
        Id = _contadorId++;
        Fecha = fecha;
        Intervencion = intervencion;
        Medico = medico;
        Pagado = false;
    }

    public decimal CalcularCostoFinal(Paciente paciente)
    {
        decimal costo = Intervencion.CalcularCosto();

        if (paciente.ObraSocial != null)
        {
            decimal descuento = costo * (paciente.MontoCobertura / 100);
            costo -= descuento;
        }

        return costo;
    }
}