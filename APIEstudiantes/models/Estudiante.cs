public class Estudiante
{
    public int Id { get; set; }
    public required string Nombre { get; set; }    
    public required string Apellido { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public required string Mail { get; set; }
}