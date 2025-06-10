public class Intervencion
{
    public string Codigo { get; set; }
    public string Descripcion { get; set; }
    public string Especialidad { get; set; }
    public decimal Arancel { get; set; }
    public bool EsAltaComplejidad { get; set; }
    public decimal PorcentajeAdicional { get; set; }

    public Intervencion(string codigo, string descripcion, string especialidad, decimal arancel, bool esAltaComplejidad = false, decimal porcentajeAdicional = 0)
    {
        Codigo = codigo;
        Descripcion = descripcion;
        Especialidad = especialidad;
        Arancel = arancel;
        EsAltaComplejidad = esAltaComplejidad;
        PorcentajeAdicional = porcentajeAdicional;
    }

    public decimal CalcularCosto()
    {
        if (EsAltaComplejidad)
        {
            return Arancel + (Arancel * PorcentajeAdicional / 100);
        }
        return Arancel;
    }
}