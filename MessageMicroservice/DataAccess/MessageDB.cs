using MessageMicroservice.Models;
using MessageUtil.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MessageMicroservice.DataAccess
{
    ////Staticka klasa koja sadrzi potrebnu logiku za obradu odredjenih zahteva
    public static class MessageDB
    {
        private static string AllColumnSelect
        {
            get
            {
                return @"[id_poruke],
	                [vreme],
	                [tekst],
                    [id_kanala],
                    [id_ucesnik]";
            }
        }

        //Metoda koja vrsi mapiranje podataka koji su vraceni iz baze na model
        private static Message ReadRow(SqlDataReader reader)
        {
            Message retVal = new Message();

            retVal.id_poruke= (int)reader["id_poruke"];
            retVal.vreme = (DateTime) reader["vreme"];
            retVal.tekst = reader["tekst"] as string;
            retVal.id_kanala = (int)reader["id_kanala"];
            retVal.id_ucesnik = (int)reader["id_ucesnik"];

            return retVal;
        }

        //Metoda koja obradjuje zahtev za vracanje korisnika iz baze po ID-ju
        public static Message GetMessageById(int messageID) //Ulazni parametar je ID koji je prosledjen u samoj ruti
        {
            try
            {
                //Kreiranje praznog modela
                Message retVal = new Message();

                //Kreiranje konekcije na osnovu connection string-a iz Web.config-a
                using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString)) 
                {
                    //Kreiranje SQL komande nad datom konekcijom i dodavanje SQL-a koji ce se izvrsiti nad bazom
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = String.Format(@"
                        SELECT
                            {0}
                        FROM
                            [poruka]
                        WHERE
                            [id_poruke] = @Id
                    ", AllColumnSelect);

                    //Dodavanje parametara u SQL. Metoda AddParameter se nalazi u Util projektu.
                    SqlParameter idParameter = new SqlParameter("@Id", messageID);
                    command.Parameters.Add(idParameter);


                    //Otvaranje konekcije nad bazom
                    connection.Open();

                    //Izvrsavanje SQL komande i vracanje podataka iz baze
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //Provera da li su podaci vraceni i popunjavanje modela uz pomoc metode ReadRow
                        if (reader.Read())
                        {
                            retVal = ReadRow(reader);
                        }
                    }
                }
                return retVal; //Vracanje popunjenog modela (ukoliko je trazeni korisnik u bazi)
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}