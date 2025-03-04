using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
