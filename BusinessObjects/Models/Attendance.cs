using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly WorkDate { get; set; }

    public DateTime? CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }

    public string WorkStatus { get; set; } = null!;

    public decimal? WorkHours { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
