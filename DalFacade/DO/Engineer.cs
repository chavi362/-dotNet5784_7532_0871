
namespace DO;

    public record Engineer(
        int Id,
        string Name,
        string Email,
        EngineerExperience? Level = null,
        double? Cost = null
    )
    {
        // Parameterless constructor required for XmlSerializer
        public Engineer() : this(0, "", "", null, null) { }
    }


