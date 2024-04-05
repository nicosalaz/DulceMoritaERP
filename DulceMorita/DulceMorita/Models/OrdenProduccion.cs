using System;
using System.Collections.Generic;

namespace DulceMorita.Models;

public partial class OrdenProduccion
{
    public int IdOrden { get; set; }

    public int ProduccionTotal { get; set; }

    public string FechaCreacion { get; set; } = null!;

    public int? FkProducto { get; set; }

    public virtual Producto? FkProductoNavigation { get; set; }

    public virtual ICollection<LoteProduccion> LoteProduccions { get; set; } = new List<LoteProduccion>();
}
