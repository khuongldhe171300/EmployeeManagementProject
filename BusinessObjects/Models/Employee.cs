using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeCode { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Avatar { get; set; }

    public int? DepartmentId { get; set; }

    public DateOnly StartDate { get; set; }

    public string WorkStatus { get; set; } = null!;

    public int PositionId { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();

    public virtual Position Position { get; set; } = null!;

    public virtual User? User { get; set; }
}
