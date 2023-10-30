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
    bool Milestone,
    DateTime CreatedAt,
    DateTime Start,
    DateTime Forecast,
    DateTime DedLine,
    DateTime Complete,
    string Deliverables,
    string Remarks,
    int EngineerId
    //  להוסיף מהנדס
    );
