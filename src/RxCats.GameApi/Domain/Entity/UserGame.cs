using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RxCats.GameApi.Domain.Entity;

[Table("tb_user_game")]
public record UserGame : BaseEntity
{
    [Key]
    [Column("user_id")]
    public long UserId { get; set; }

    [Column("heart")]
    public int Heart { get; set; }

    [Column("play_cnt")]
    public int PlayCount { get; set; }
}