public class Doctor
{
    public string NombreApellido { get; set; }
    public string Matricula { get; set; }
    public string Especialidad { get; set; }
    public bool Disponible { get; set; }

    public Doctor(string nombreApellido, string matricula, string especialidad, bool disponible)
    {
        NombreApellido = nombreApellido;
        Matricula = matricula;
        Especialidad = especialidad;
        Disponible = disponible;
    }

    public override string ToString()
    {
        return $"{NombreApellido} (Matrícula: {Matricula}) - {Especialidad} - {(Disponible ? "Disponible" : "No disponible")}";
    }
}