using System;
using System.Collections.Generic;

namespace InventoryManagement.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string OrderType { get; set; } = null!;

    public int? CustomerId { get; set; }

    public int? SupplierId { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Supplier? Supplier { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<WarehouseTask> WarehouseTasks { get; set; } = new List<WarehouseTask>();
}
