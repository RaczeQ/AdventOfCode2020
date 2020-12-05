using ElvenTools;

namespace Day5
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(5, "Binary Boarding", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}