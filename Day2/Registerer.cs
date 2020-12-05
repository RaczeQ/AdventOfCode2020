using ElvenTools;

namespace Day2
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(2, "Password Philosophy", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}