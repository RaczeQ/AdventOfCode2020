using ElvenTools;

namespace Day6
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(6, "Custom Customs", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}