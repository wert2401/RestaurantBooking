﻿using RestaurantBooking.Api.Models.Table;
using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Api.Models.Restaurant
{
    public class RestaurantModelDetailed : BaseModel
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public int OwnerUserId { get; set; }
        public string? MenuPath { get; set; }
        public string? SchemeImage { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public int TablesCount { get; set; }
        [Required]
        public int VacantTablesCount { get; set; }
        public double Rating { get; set; }
        public ICollection<TableModel>? Tables { get; set; }

        [Required]
        public TimeSpan OpenFrom { get; set; }
        [Required]
        public TimeSpan OpenTo { get; set; }
    }
}
