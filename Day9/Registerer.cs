using ElvenTools;

namespace Day9
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(9, "Encoding Error", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}