namespace AnnapurnaEnterprises.Api.DTOs
{
    public class AdminLoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAtUtc { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}