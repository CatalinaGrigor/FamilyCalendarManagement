using System.Diagnostics;

namespace tpCatalinaV4.Models
{
    public class Horaire
    {
        public int HoraireID { get; set; }
        public DateTime Date { get; set; }

        public int NbRepet { get; set; }

        public int UtilisateurID { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public int TacheID { get; set; }
        public Tache Tache { get; set; }
    }
}
