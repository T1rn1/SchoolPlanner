using System;
using System.Collections.Generic;

namespace SchoolPlanner.Models;

public partial class Homework
{
    public int IdSubject { get; set; }

    public string Tusk { get; set; } = null!;

    public string Note { get; set; } = null!;

    public virtual Subject IdSubjectNavigation { get; set; } = null!;
}
