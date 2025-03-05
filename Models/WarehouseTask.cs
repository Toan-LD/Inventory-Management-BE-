using System;
using System.Collections.Generic;

namespace InventoryManagement.Models;

public partial class WarehouseTask
{
    public int TaskId { get; set; }

    public int? OrderId { get; set; }

    public int? UserId { get; set; }

    public string TaskType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public virtual Order? Order { get; set; }

    public virtual User? User { get; set; }
}
