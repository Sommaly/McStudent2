using GTP.classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTP.ados
{
    public class UtilisateurAdo
    {
        public static void AjouterUtilisateur(Utilisateur p)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "INSERT INTO Utilisateur(nom,prenom,identifiant,fk_id_promotion) VALUES(@param1,@param2,@param3,@param4)";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = p.Nom;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar).Value = p.Prenom;
                cmd.Parameters.Add("@param3", SqlDbType.VarChar).Value = p.Identifiant;
                cmd.Parameters.Add("@param4", SqlDbType.Int).Value = p.Promotion.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }

        public static List<Utilisateur> VoirUtilisateurs()
        {
            List<Utilisateur> utilisateurs = new List<Utilisateur>();
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "SELECT * FROM Utilisateur";
            SqlCommand command = new SqlCommand(query, sqlc);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Utilisateur p = new Utilisateur();
                    p.Id = reader.GetInt32(0);
                    p.Nom = reader.GetString(1);
                    p.Prenom = reader.GetString(2);
                    p.Identifiant = reader.GetString(3);
                    Promotion promotion = PromotionAdo.VoirPromotions().First(x => x.Id == reader.GetInt32(4));
                    promotion.Utilisateurs.Add(p);
                    p.Promotion = promotion;
                    utilisateurs.Add(p);
                }
            }
            return utilisateurs;
        }

        public static void SupprimerUtilisateur(Utilisateur p)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "DELETE FROM Utilisateur WHERE id_utilisateur=@param1";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = p.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }

        public static void ModifierUtilisateur(Utilisateur p)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "UPDATE Utilisateur SET nom = @param1, prenom = @param2,identifiant = @param3, fk_id_promotion = @param4 WHERE id_utilisateur = @param5;";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = p.Nom;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar).Value = p.Prenom;
                cmd.Parameters.Add("@param3", SqlDbType.VarChar).Value = p.Identifiant;
                cmd.Parameters.Add("@param4", SqlDbType.Int).Value = p.Promotion;
                cmd.Parameters.Add("@param5", SqlDbType.Int).Value = p.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }
    }
}
