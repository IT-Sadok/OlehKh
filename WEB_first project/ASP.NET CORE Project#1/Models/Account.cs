﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_CORE_Project_1.Models
{
    [Table(nameof(Account))]
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }
    }
}
