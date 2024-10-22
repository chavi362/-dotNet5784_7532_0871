﻿

namespace DO;

public record Task
(
    int Id,
    string Description,
    string? Alias =null,
    bool Milestone=false,
    TimeSpan? RequiredEffortTime = null,
    DateTime? CreatedAtDate=null,
    DateTime? Start = null,
    DateTime? ScheduledDate=null,
    DateTime? Forecast = null,
    DateTime? DeadLineDate = null,
    DateTime? Complete = null,
    string? Deliverables=null,
    string? Remarks=null,
    int? EngineerId=null,
    EngineerExperience? ComplexityLevel=null
    
    )
{
    // Parameterless constructor required for XmlSerializer
    public Task() : this(0, "", "", false, TimeSpan.Zero) { }
}
