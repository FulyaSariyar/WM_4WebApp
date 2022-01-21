﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ItServiceApp.Models.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [StringLength(128)]
        public string CreatedUser  { get; set; }
        public DateTime? UpdateDate { get; set; }
        [StringLength(128)]
        public string UpdateUser { get; set; }


    }
}
