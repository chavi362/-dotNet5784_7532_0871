﻿
using DO;

namespace BO
{
    internal class Engineer
    {
        public int Id { get; init; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public EngineerExperience? Level { get; set; }
        public double? Cost { get; set; }
    }
}
