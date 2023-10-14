using System;
using System.Collections.Generic;

namespace Matricula.Models;

public partial class TbSeccion
{
    public int NIdSeccion { get; set; }

    public int? NIdAula { get; set; }

    public int? NIdCurso { get; set; }

    public string STurno { get; set; } = null!;

    public TimeSpan THoraInicio { get; set; }

    public TimeSpan THoraFin { get; set; }

    public virtual TbAula? NIdAulaNavigation { get; set; }

    public virtual TbCurso? NIdCursoNavigation { get; set; }

    public virtual ICollection<TbMatricula> TbMatriculas { get; set; } = new List<TbMatricula>();
}
