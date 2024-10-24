using System;
using System.Collections.Generic;

namespace SchoolPlanner.Models;

public partial class Reason
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Pass> Passes { get; set; } = new List<Pass>();
}
