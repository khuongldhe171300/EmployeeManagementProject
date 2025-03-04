using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Payroll
{
    public int PayrollId { get; set; }

    public int EmployeeId { get; set; }

    public int PayrollMonth { get; set; }

    public decimal BasicSalary { get; set; }

    public decimal? Allowance { get; set; }

    public decimal? Bonus { get; set; }

    public decimal TotalSalary { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
