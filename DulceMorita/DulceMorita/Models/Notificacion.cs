using System;
using System.Collections.Generic;

namespace DulceMorita.Models;

public partial class Notificacion
{
    public int IdNotificacion { get; set; }

    public int? FkLote { get; set; }

    public int? FkOpe { get; set; }

    public int Buenas { get; set; }

    public int Malas { get; set; }

    public DateTime FInicio { get; set; }

    public DateTime FFin { get; set; }

    public int GastosAdicionales { get; set; }

    public string? Obseraciones { get; set; }

    public virtual LoteProduccion? FkLoteNavigation { get; set; }

    public virtual Operario? FkOpeNavigation { get; set; }
}
