public abstract class Intervencion
{
    public string Codigo { get; set; }
    public string Descripcion { get; set; }
    public string Especialidad { get; set; }
    public decimal Arancel { get; set; }

    protected Intervencion(string codigo, string descripcion, string especialidad, decimal arancel)
    {
        Codigo = codigo;
        Descripcion = descripcion;
        Especialidad = especialidad;
        Arancel = arancel;
    }

    public abstract decimal CalcularCosto();
}

public class IntervencionComun : Intervencion
{
    public IntervencionComun(string codigo, string descripcion, string especialidad, decimal arancel)
        : base(codigo, descripcion, especialidad, arancel) { }

    public override decimal CalcularCosto()
    {
        return Arancel;
    }
}

public class IntervencionAltaComplejidad : Intervencion
{
    public static decimal PorcentajeAdicional { get; set; } = 0.30m; // 30% adicional

    public IntervencionAltaComplejidad(string codigo, string descripcion, string especialidad, decimal arancel)
        : base(codigo, descripcion, especialidad, arancel) { }

    public override decimal CalcularCosto()
    {
        return Arancel * (1 + PorcentajeAdicional);
    }
}