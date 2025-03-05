using System;
using System.Collections.Generic;

namespace InventoryManagement.Models;

public partial class Stock
{
    public int StockId { get; set; }

    public int ItemId { get; set; }

    public int BinId { get; set; }

    public int Quantity { get; set; }

    public virtual Location Bin { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
