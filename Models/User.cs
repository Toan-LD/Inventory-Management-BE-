using System;
using System.Collections.Generic;

namespace InventoryManagement.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime CreatedAt { get; set; }
	public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
	public virtual ICollection<Transaction>? Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<WarehouseTask>? WarehouseTasks { get; set; } = new List<WarehouseTask>();
}
