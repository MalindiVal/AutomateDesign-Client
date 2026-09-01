using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class AutomateMemento
    {
        internal string nom;
        internal List<Etat> etats;
        internal List<Transition> transitions;
        internal AutomateMemento(string nom, List<Etat> etats, List<Transition> transitions)
        {
            this.nom = nom;
            this.etats = new List<Etat>(etats);
            this.transitions = new List<Transition>(transitions);

        }
    }
}