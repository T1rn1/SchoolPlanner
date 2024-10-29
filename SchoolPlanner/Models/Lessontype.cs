using System;
using System.Collections.Generic;

namespace SchoolPlanner.Models;

public partial class Lessontype
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
