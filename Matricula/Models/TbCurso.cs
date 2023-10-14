using System;
using System.Collections.Generic;

namespace Matricula.Models;

public partial class TbCurso
{
    public int NIdCurso { get; set; }

    public string? SDesCurso { get; set; }

    public int? NIdCiclo { get; set; }

    public virtual TbCiclo? NIdCicloNavigation { get; set; }

    public virtual ICollection<TbMatricula> TbMatriculas { get; set; } = new List<TbMatricula>();

    public virtual ICollection<TbSeccion> TbSeccions { get; set; } = new List<TbSeccion>();
}
