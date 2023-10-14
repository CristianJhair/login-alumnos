using System;
using System.Collections.Generic;

namespace Matricula.Models;

public partial class TbCiclo
{
    public int NIdCiclo { get; set; }

    public string? NombreCiclo { get; set; }

    public virtual ICollection<TbAlumno> TbAlumnos { get; set; } = new List<TbAlumno>();

    public virtual ICollection<TbCurso> TbCursos { get; set; } = new List<TbCurso>();
}
