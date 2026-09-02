using LogicLayer;
using System.Collections.ObjectModel;

namespace ViewModels
{
    public class AutomateVM : BaseViewModel
    {
        private readonly AutomateKeeper keeper;
        private double etatRadius = 75;
        private string title;

        public Automate Metier { get; set; }

        public ObservableCollection<EtatVM> Etats { get; } = new();
        public ObservableCollection<TransitionVM> Transitions { get; } = new();

        public double EtatRadius
        {
            get => etatRadius;
            set
            {
                etatRadius = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public AutomateVM(Automate automate)
        {
            Metier = automate;
            keeper = new AutomateKeeper();
            keeper.Keep(Metier);

            RecupAutomate();
        }

        // -------------------------
        // Undo
        // -------------------------

        public void Undo()
        {
            keeper.Undo();
            RecupAutomate();
        }

        // -------------------------
        // Etats
        // -------------------------

        public void AjouterEtatNormal(double x, double y)
        {
            AjouterEtat(x, y, false, false);
        }

        public void AjouterEtatInitial(double x, double y)
        {
            foreach (var etat in Etats)
                etat.EstInitial = false;

            AjouterEtat(x, y, true, false);
        }

        public void AjouterEtatFinal(double x, double y)
        {
            AjouterEtat(x, y, false, true);
        }

        private void SaveState()
        {
            ConstruireAutomateDepuisVM();
            keeper.Do();
        }
        private void AjouterEtat(
            double x,
            double y,
            bool initial,
            bool final)
        {
            Etat etat = new Etat
            {
                Nom = NouveauNomEtat(),
                Position = new Position(x, y),
                EstInitial = initial,
                EstFinal = final
            };

            Etats.Add(new EtatVM(etat)
            {
                EtatRadius = EtatRadius
            });

            this.SaveState();
        }

        private string NouveauNomEtat()
        {
            int index = 0;

            while (Etats.Any(e => e.Nom == $"Etat {index}"))
                index++;

            return $"Etat {index}";
        }

        public bool CheckOverlap(double x, double y)
        {
            return Etats.Any(e => e.CheckOverlap(x, y));
        }

        public void SupprimerEtat(EtatVM etat)
        {
            this.SaveState();

            foreach (var transition in Transitions
                .Where(t => t.EtatDepart == etat || t.EtatArrivee == etat)
                .ToList())
            {
                SupprimerTransition(transition);
            }

            Etats.Remove(etat);
        }

        // -------------------------
        // Transitions
        // -------------------------

        public void AjouterTransition(EtatVM debut, EtatVM fin)
        {
            this.SaveState();
            var transition = new TransitionVM(debut, fin);

            debut.TransitionsOut.Add(transition);
            fin.TransitionsIn.Add(transition);

            transition.PreviousTransition = debut.TransitionsOut
                .Where(t => t.EtatArrivee == fin && t != transition)
                .LastOrDefault();

            transition.Condition = NouveauNomCondition();

            Transitions.Add(transition);

            transition.RefreshGeometry();
        }

        private string NouveauNomCondition()
        {
            int index = 0;

            while (Transitions.Any(t => t.Condition == $"Condition {index}"))
                index++;

            return $"Condition {index}";
        }

        public void SupprimerTransition(TransitionVM transition)
        {
            this.SaveState();
            transition.EtatDepart.TransitionsOut.Remove(transition);
            transition.EtatArrivee.TransitionsIn.Remove(transition);

            Transitions.Remove(transition);
        }

        // -------------------------
        // Synchronisation
        // -------------------------

        public void ConstruireAutomateDepuisVM()
        {
            Metier.Etats.Clear();
            Metier.Transitions.Clear();

            foreach (var etat in Etats)
                Metier.Etats.Add(etat.Metier);

            foreach (var transition in Transitions)
                Metier.Transitions.Add(transition.Metier);
        }

        public void RecupAutomate()
        {
            Title = Metier.Nom;

            Etats.Clear();
            Transitions.Clear();

            var map = new Dictionary<Etat, EtatVM>();

            foreach (var etat in Metier.Etats)
            {
                var vm = new EtatVM(etat)
                {
                    EtatRadius = EtatRadius
                };

                Etats.Add(vm);
                map[etat] = vm;
            }

            foreach (var transition in Metier.Transitions)
            {
                if (!map.TryGetValue(transition.EtatDebut, out var debut) ||
                    !map.TryGetValue(transition.EtatFinal, out var fin))
                    continue;

                var vm = new TransitionVM(debut, fin, transition)
                {
                    Condition = transition.Condition
                };

                Transitions.Add(vm);
                debut.TransitionsOut.Add(vm);
                fin.TransitionsIn.Add(vm);
            }
        }
    }
}
