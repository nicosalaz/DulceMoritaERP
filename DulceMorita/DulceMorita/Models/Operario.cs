using System;
using System.Collections.Generic;

namespace DulceMorita.Models;

public partial class Operario
{
    public int IdOperario { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();
}
