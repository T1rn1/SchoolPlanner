using System;
using System.Collections.Generic;

namespace SchoolPlanner.Models;

public partial class Teacher
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public int TelephoneNumber { get; set; }

    public string WorkingHours { get; set; } = null!;

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
