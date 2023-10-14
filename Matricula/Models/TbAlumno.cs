using System;
using System.Collections.Generic;

namespace Matricula.Models;

public partial class TbAlumno
{
    public int NIdAlumno { get; set; }

    public string NombreAlumno { get; set; } = null!;

    public string ApellidoAlumno { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? NIdCiclo { get; set; }

    public virtual TbCiclo? NIdCicloNavigation { get; set; }

    public virtual ICollection<TbMatricula> TbMatriculas { get; set; } = new List<TbMatricula>();
}
