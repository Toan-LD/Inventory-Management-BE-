using System;
using System.Collections.Generic;

namespace InventoryManagement.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public string LocationType { get; set; } = null!;

    public int? ParentLocationId { get; set; }

    public int? Capacity { get; set; }

    public string? Barcode { get; set; }

    public virtual ICollection<Location> InverseParentLocation { get; set; } = new List<Location>();

    public virtual Location? ParentLocation { get; set; }

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
