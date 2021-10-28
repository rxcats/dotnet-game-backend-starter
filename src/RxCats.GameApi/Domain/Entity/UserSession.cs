using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RxCats.GameApi.Domain.Entity;

[Table("tb_user_session")]
public record UserSession : BaseEntity
{
    [Key]
    [Column("user_id")]
    public long UserId { get; set; }

    [Column("access_token")]
    public string AccessToken { get; set; }

    [Column("expiry_datetime")]
    public DateTime? ExpiryDateTime { get; set; }

    [Column("api_path")]
    public string? ApiPath { get; set; }

    [Column("ip_address")]
    public string? IpAddress { get; set; }

    public bool IsExpired() => ExpiryDateTime > DateTime.Now;

    public bool IsValid(string token) => token == AccessToken;

    public void UpdateExpiryDateTime(long expiryDays = 7)
    {
        ExpiryDateTime = DateTime.Now.AddDays(expiryDays);
    }
}