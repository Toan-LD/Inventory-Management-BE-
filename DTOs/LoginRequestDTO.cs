namespace InventoryManagement_BE_.DTOs
{
	public class LoginRequestDTO
	{
		public string Username { get; set; } = null!;
		public string Password { get; set; } = null!;
		public bool RememberMe { get; set; } = false;
	}
}
