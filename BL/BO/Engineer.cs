

namespace BO
{
    public class Engineer
    {
        public int Id { get; init; }
        public required string Name { get; set; } 
        public required string Email { get; set; }
        public EngineerExperience? Level { get; set; } = null;
        public double Cost { get; set; }
        public Tuple<int,string>? CurrentTask { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
