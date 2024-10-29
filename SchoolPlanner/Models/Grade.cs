using System;
using System.Collections.Generic;

namespace SchoolPlanner.Models;

public partial class Grade
{
    public int Id { get; set; }

    public int Grade1 { get; set; }

    public DateOnly Date { get; set; }

    public int IdSubject { get; set; }

    public int IdLessonType { get; set; }

    public virtual Lessontype IdLessonTypeNavigation { get; set; } = null!;

    public virtual Subject IdSubjectNavigation { get; set; } = null!;
}
