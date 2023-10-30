//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace DO;

public record Task
(
    int Id,
    string Description,
    string Alias,
    bool Milestone=false,
    DateTime? CreatedAt=null,
    DateTime? Start = null,
    DateTime? Forecast = null,
    DateTime? DedLine = null,
    DateTime? Complete = null,
    string? Deliverables=null,
    string? Remarks=null,
    int? EngineerId=null,
    EngineerExperience? ComplexityLevel=null
    
    );
