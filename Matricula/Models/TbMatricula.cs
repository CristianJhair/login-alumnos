using System;
using System.Collections.Generic;

namespace Matricula.Models;

public partial class TbMatricula
{
    public int NIdAlumno { get; set; }

    public int NIdSeccion { get; set; }

    public int? NIdCurso { get; set; }

    public virtual TbAlumno NIdAlumnoNavigation { get; set; } = null!;

    public virtual TbCurso? NIdCursoNavigation { get; set; }

    public virtual TbSeccion NIdSeccionNavigation { get; set; } = null!;
}
