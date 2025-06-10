public class Doctor
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Matricula { get; set; }
    public string Especialidad { get; set; }
    public bool Disponible { get; set; }

    public Doctor(string nombre, string apellido, string matricula, string especialidad, bool disponible)
    {
        Nombre = nombre;
        Apellido = apellido;
        Matricula = matricula;
        Especialidad = especialidad;
        Disponible = disponible;
    }

    public string NombreCompleto => $"{Apellido}, {Nombre}";
}