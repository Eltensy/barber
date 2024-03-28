﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuinessLogicLayer.DTOs
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public int fk_BarberId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public TimeOnly Duration {  get; set; }
        public int Price { get; set; }
    }
}
