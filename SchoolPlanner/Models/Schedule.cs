using System;
using System.Collections.Generic;

namespace SchoolPlanner.Models;

public partial class Schedule
{
    public int Id { get; set; }

    public TimeOnly Time { get; set; }

    public DateOnly Date { get; set; }

    public int IdSubject { get; set; }

    public int IdPass { get; set; }

    public int Class { get; set; }

    public virtual Pass IdPassNavigation { get; set; } = null!;

    public virtual Subject IdSubjectNavigation { get; set; } = null!;
}
