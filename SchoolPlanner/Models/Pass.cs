using System;
using System.Collections.Generic;

namespace SchoolPlanner.Models;

public partial class Pass
{
    public int Id { get; set; }

    public int IdReason { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public string? Note { get; set; }

    public virtual Reason IdReasonNavigation { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
