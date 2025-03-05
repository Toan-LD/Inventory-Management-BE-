using System;
using System.Collections.Generic;

namespace InventoryManagement.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int ItemId { get; set; }

    public int BinId { get; set; }

    public int Quantity { get; set; }

    public string TransactionType { get; set; } = null!;

    public int? OrderId { get; set; }

    public int? UserId { get; set; }

    public DateTime TransactionDate { get; set; }

    public virtual Location Bin { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;

    public virtual Order? Order { get; set; }

    public virtual User? User { get; set; }
}
