namespace tpCatalinaV4.Models
{
    public class Tache
    {
        enum Frequence { hebdomadaire, journalier, mensuel, annuel }

        public int TacheID { get; set; }
        public string Description { get; set; }


        public double Duree { get; set; }

        public Boolean Repetitive { get; set; }



        public ICollection<Horaire> Horaire { get; set; }
    }
}