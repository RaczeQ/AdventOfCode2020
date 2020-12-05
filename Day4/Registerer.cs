using ElvenTools;

namespace Day4
{
    public class Registerer : ActionRegisterer
    {
        public Registerer()
            : base(4, "Passport Processing", new FirstSolver().Calculate, new SecondSolver().Calculate)
        {
        }
    }
}