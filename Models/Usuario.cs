using Postgrest.Attributes;
using Postgrest.Models;

namespace proyecto_final_movil.Models;

[Table("Usuario")]
public class Usuario : BaseModel
{
    [PrimaryKey("id_usuario", false)]
    public int IdUsuario { get; set; }

    [Column("nombreCompleto")]
    public string? NombreCompleto { get; set; }

    [Column("edad")]
    public int? Edad { get; set; }

    [Column("domicilio")]
    public string? Domicilio { get; set; }

    [Column("correo")]
    public string? Correo { get; set; }

    [Column("nombreUsuario")]
    public string? NombreUsuario { get; set; }

    [Column("contrasnenia")]
    public string? ContraseniaHash { get; set; }

    [Column("telefono")]
    public string? NumeroTelefono { get; set; }

    [Column("fechaCreacion")]
    public DateTime? FechaCreacion { get; set; }
}
