using ElvenTools;

namespace Day7
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(7, "Handy Haversacks", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}