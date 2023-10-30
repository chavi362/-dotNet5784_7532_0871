

namespace DO;

public record Engineer( 
    int Id, 
    string Name,
    string Email,
    EngineerExperience? Level=null,
    double? Cost=null
    );
