﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantBooking.Data.Entities
{
    public class Restaurant : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public int OwnerUserId { get; set; }
        public string? MainPhoto { get; set; }
        public List<string>? PhoneNumbers { get; set; }
        [Required]
        [MaxLength(500)]
        public string Address { get; set; } = null!;

        public int TablesCount => Tables.Count;
        public int VacantTablesCount => Tables.Where(t => !t.TableClaims.Any(c => !c.IsExpired) || t.TableClaims.Count == 0).Count();
        public double Rating => Reviews.Count > 0 ? Reviews.Sum(r => r.Grade) / Reviews.Count : 0;

        public List<Table> Tables { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
        public List<User> FavoritedBy { get; set; } = new();
    }
}
