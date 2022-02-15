using System;
using System.ComponentModel.DataAnnotations;

namespace ItServiceApp.Core.Entities.Abstracts
{
    public class BaseEntity<TKey>: IEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [StringLength(128)]
        public string CreatedUser  { get; set; }
        public DateTime? UpdateDate { get; set; }
        [StringLength(128)]
        public string UpdateUser { get; set; }
    }
}
