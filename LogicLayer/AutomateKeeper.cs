using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class AutomateKeeper
    {
        private Stack<AutomateMemento> states = new Stack<AutomateMemento>();
        private Automate automate;

        public void Keep(Automate p)
        {
            states.Clear();
            automate = p;
        }
        public void Do()
        {
            states.Push(automate.Save());
        }
        public void Undo()
        {
            automate.Restore(states.Pop());
        }
    }
}
