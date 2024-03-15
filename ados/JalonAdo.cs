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
    public class JalonAdo
    {
        public static void AjouterJalon(Jalon j)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "INSERT INTO Jalon(titre_jalon,fk_id_tp) VALUES(@param1,@param2)";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = j.Titre;
                cmd.Parameters.Add("@param2", SqlDbType.Int).Value = j.Tp.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }

        public static List<Jalon> VoirJalons()
        {
            List<Jalon> jalons = new List<Jalon>();
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "SELECT * FROM Jalon";
            SqlCommand command = new SqlCommand(query, sqlc);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Jalon j = new Jalon();
                    j.Id = reader.GetInt32(0);
                    j.Titre = reader.GetString(1);
                    TP tp = TPAdo.VoirTp().First(x => x.Id == reader.GetInt32(2));
                    tp.Jalons.Add(j);
                    j.Tp = tp;
                    jalons.Add(j);
                }
            }
            return jalons;
        }

        public static void SupprimerJalon(Jalon j)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "DELETE FROM Jalon WHERE id_jalon=@param1";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = j.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }

        public static void ModifierJalon(Jalon j)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "UPDATE Jalon SET titre_jalon = @param1, fk_id_tp = @param2 WHERE id_jalon = @param4;";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = j.Titre;
                cmd.Parameters.Add("@param2", SqlDbType.Int).Value = j.Tp;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }
    }
}
