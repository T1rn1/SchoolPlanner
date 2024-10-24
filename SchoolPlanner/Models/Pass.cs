using System;
using System.Collections.Generic;

namespace SchoolPlanner.Models;

public partial class Pass
{
    public int Id { get; set; }

    public int IdReason { get; set; }

    public string Note { get; set; } = null!;

    public virtual Reason IdReasonNavigation { get; set; } = null!;
}
