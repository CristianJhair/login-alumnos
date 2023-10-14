using System;
using System.Collections.Generic;

namespace Matricula.Models;

public partial class TbAula
{
    public int NIdAula { get; set; }

    public int NCapacidad { get; set; }

    public virtual ICollection<TbSeccion> TbSeccions { get; set; } = new List<TbSeccion>();
}
