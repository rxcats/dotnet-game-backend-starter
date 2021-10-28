using System.ComponentModel.DataAnnotations.Schema;

namespace RxCats.GameApi.Domain.Entity;

public abstract record BaseEntity
{
    [Column("created_datetime")]
    public DateTime? CreatedDateTime { get; set; }

    [Column("updated_datetime")]
    public DateTime? UpdatedDateTime { get; set; }
}