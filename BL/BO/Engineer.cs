﻿
using DO;

namespace BO
{
    public class Engineer
    {
        public int Id { get; init; }
        public required string Name { get; set; } 
        public required string Email { get; set; }
        public EngineerExperience? Level { get; set; } = null;
        public double Cost { get; set; }
        public TaskInEngineer? Task { get; set; }
    }
}
