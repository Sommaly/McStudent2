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
    public class TPAdo
    {
        public static void AjouterTP(TP p)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "INSERT INTO Tp(titre_tp,desc_titre,fk_id_promotion) VALUES(@param1,@param2,@param3)";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = p.Titre;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar).Value = p.Description;
                cmd.Parameters.Add("@param3", SqlDbType.Int).Value = p.Promotion.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }

        public static List<TP> VoirTp()
        {
            List<TP> tps = new List<TP>();
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "SELECT * FROM Tp";
            SqlCommand command = new SqlCommand(query, sqlc);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    TP tp = new TP();
                    tp.Id = reader.GetInt32(0);
                    tp.Titre = reader.GetString(1);
                    tp.Description = reader.GetString(2);
                    Promotion promotion = PromotionAdo.VoirPromotions().First(x => x.Id == reader.GetInt32(3));
                    promotion.Tps.Add(tp);
                    tp.Promotion = promotion;
                    tps.Add(tp);
                }
            }
            return tps;
        }

        public static void SupprimerTp(TP p)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "DELETE FROM Tp WHERE id_tp=@param1";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = p.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }

        public static void ModifierTp(TP p)
        {
            SqlConnection sqlc = Ado.OpenConnexion();
            string query = "UPDATE Tp SET titre_tp = @param1, desc_titre = @param2,fk_id_promotion = @param3 WHERE id_tp = @param4;";
            using (SqlCommand cmd = new SqlCommand(query, sqlc))
            {
                cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = p.Titre;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar).Value = p.Description;
                cmd.Parameters.Add("@param3", SqlDbType.Int).Value = p.Promotion;
                cmd.Parameters.Add("@param4", SqlDbType.Int).Value = p.Id;
                cmd.ExecuteNonQuery();
            }
            Ado.CloseConnexion(sqlc);
        }
    }
}
