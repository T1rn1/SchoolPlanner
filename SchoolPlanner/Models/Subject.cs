using System;
using System.Collections.Generic;

namespace SchoolPlanner.Models;

public partial class Subject
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? IdTeacher { get; set; }

    public int IdCyclicCommission { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();

    public virtual Cycliccommission IdCyclicCommissionNavigation { get; set; } = null!;

    public virtual Teacher? IdTeacherNavigation { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
