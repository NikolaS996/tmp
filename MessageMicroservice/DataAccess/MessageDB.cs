using MessageMicroservice.Models;
using MessageUtil.DataAccess;
using MessageUtil.Logging;
using MessageUtil;
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
            //Kreiranje praznog modela
            Message retVal = new Message();

            try
            {
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
                
            }
            catch (Exception ex)
            {
                //Log exception here
                LogHelper.LogException(LogTarget.File, ex, DataFormatUtil.GetFormatedLongDateTimeString(DateTime.Now));
            }

            return retVal; //Vracanje popunjenog modela (ukoliko je trazeni korisnik u bazi)
        }

        public static List<Message> GetAllMessages()
        {
            //Kreiranje prazne liste
            List<Message> retMessages = new List<Message>();

            try
            {
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
                    ", AllColumnSelect);
                   
                    //Otvaranje konekcije nad bazom
                    connection.Open();

                    //Izvrsavanje SQL komande i vracanje podataka iz baze
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //Provera da li su podaci vraceni i popunjavanje modela uz pomoc metode ReadRow
                        while(reader.Read())
                        {
                            Message message = ReadRow(reader);
                            retMessages.Add(message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log exception here
                LogHelper.LogException(LogTarget.File, ex, DataFormatUtil.GetFormatedLongDateTimeString(DateTime.Now));
            }

            return retMessages; //Vracanje popunjenog modela
        }

        public static Message CreateMessage(Message message)
        {
            int id = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString))
                {
                    //Kreiranje SQL komande nad datom konekcijom i dodavanje SQL-a koji ce se izvrsiti nad bazom
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = String.Format(@"
                        INSERT INTO poruka(vreme, tekst, id_kanala, id_ucesnik)
						values(@vreme, @tekst, @id_kanala, @id_ucesnik)
						set @Id = SCOPE_IDENTITY();
                        select @Id as Id
                    ");

                    //Dodavanje parametara u SQL. Metoda AddParameter se nalazi u Util projektu.
                    command.Parameters.Add("id", message.id_poruke);
                    command.Parameters.Add("@vreme", message.vreme);
                    command.Parameters.Add("@tekst", message.tekst);
                    command.Parameters.Add("@id_kanala", message.id_kanala);
                    command.Parameters.Add("@id_ucesnik", message.id_ucesnik);


                    //Otvaranje konekcije nad bazom
                    connection.Open();
                    //id = (int)command.ExecuteScalar();
                    //return GetMessageById(id);

                    ////Izvrsavanje SQL komande i vracanje podataka iz baze
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //Provera da li su podaci vraceni i popunjavanje modela uz pomoc metode ReadRow
                        if (reader.Read())
                        {
                            id = (int)reader["Id"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log exception here
                LogHelper.LogException(LogTarget.File, ex, DataFormatUtil.GetFormatedLongDateTimeString(DateTime.Now));
            }
        
            return GetMessageById(id);
        }

        public static bool DeleteUser(int id)
        {
            //Kreiranje praznog modela
            Message retVal = new Message();

            try
            {
                

                //Kreiranje konekcije na osnovu connection string-a iz Web.config-a
                using (SqlConnection connection = new SqlConnection(DBFunctions.ConnectionString))
                {
                    //Kreiranje SQL komande nad datom konekcijom i dodavanje SQL-a koji ce se izvrsiti nad bazom
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = String.Format(@"
                    DELETE FROM poruka WHERE id_poruke = @Id
                    ");

                    //Dodavanje parametara u SQL. Metoda AddParameter se nalazi u Util projektu.
                    command.Parameters.Add("@Id", id);

                    //Otvaranje konekcije nad bazom
                    connection.Open();

                    //Izvrsavanje SQL komande i vracanje broja dobijenih redova
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 1)
                    {
                        return false;
                    }

                }

            }
            catch (Exception ex)
            {
                //Log exception here
                LogHelper.LogException(LogTarget.File, ex, DataFormatUtil.GetFormatedLongDateTimeString(DateTime.Now));
            }

            return true;
        }
    }
}