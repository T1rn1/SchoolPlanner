using System;
using System.Collections.Generic;

namespace SchoolPlanner.Models;

public partial class Cycliccommission
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
