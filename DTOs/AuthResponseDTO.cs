namespace InventoryManagement_BE_.DTOs
{
	public class AuthResponseDTO
	{
		public string Token { get; set; } = null!;
		public string RefreshToken { get; set; }
		public DateTime ExpiresAt { get; set; }
	}
}
