

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
    int EngineerId=0,
    EngineerExperience? ComplexityLevel=null
    
    );
