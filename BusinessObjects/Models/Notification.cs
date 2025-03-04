using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int ReceiverId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string NotificationType { get; set; } = null!;

    public bool IsRead { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Employee Receiver { get; set; } = null!;
}
