using ElvenTools;

namespace Day8
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(8, "Handheld Halting", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}