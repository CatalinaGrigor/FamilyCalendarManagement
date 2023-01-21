namespace tpCatalinaV4.Models
{
    public class Utilisateur
    {
        public int UtilisateurID { get; set; }
        public string NomComplet { get; set; }
        public string Role { get; set; }


        public ICollection<Horaire> Horaire { get; set; }
    }
}