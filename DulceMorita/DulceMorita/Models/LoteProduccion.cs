using System;
using System.Collections.Generic;

namespace DulceMorita.Models;

public partial class LoteProduccion
{
    public int IdLote { get; set; }

    public int? FkOrden { get; set; }

    public int CantidadProduccion { get; set; }

    public string FechaRegistro { get; set; } = null!;

    public virtual OrdenProduccion? FkOrdenNavigation { get; set; }

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();
}
