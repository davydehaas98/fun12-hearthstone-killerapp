using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HearthstoneKillerApp
{
    class Database
    {
        //Setup Connection
        private static string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=" + System.IO.Path.GetFullPath(@"DatabaseKillerappWPF.mdf") + @";Integrated Security = True;";

        private SqlConnection conn = new SqlConnection(connectionString);

        //Lists for Cards, Keywords and Rarity
        public List<Card> cards = new List<Card>();
        public List<Keyword> keywords = new List<Keyword>();
        public List<Rarity> raritys = new List<Rarity>();
        public void GetCards(string filter, string order)
        {
            cards.Clear();
            //Open DB connection and create SQL command with query
            string query = "SELECT [Type],[Name],[ManaCost],[Class],[Race] FROM Cards";
            if (order != "" && filter == "")
            {
                query += " ORDER BY [" + order + "]";
            }
            else if (order != "" && filter != "")
            {
                query += " WHERE [Name] LIKE '%" + filter + "%' ORDER BY [" + order + "]";
            }
            else
            {
                query += " ORDER BY [ManaCost]";
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                //Read the query
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //cards.Clear();
                        Card card = new Card();

                        //Read the information of the Card
                        card.Type = reader.GetString(0);
                        card.Name = reader.GetString(1);
                        card.Manacost = reader.GetInt32(2);
                        card.Klasse = reader.GetString(3);
                        try
                        {
                            card.Race = reader.GetString(4);
                        }
                        catch (Exception)
                        {
                            card.Race = "";
                        }
                        //Add to the local list
                        cards.Add(card);
                    }
                }
                conn.Close();
            }
            catch { conn.Close(); }
        }
        public void GetKeywords(string selectedcard)
        {
            try
            {
                keywords.Clear();
                //Open DB connection and create SQL command with the query
                string query = "";
                if (selectedcard != "")
                {
                    //Inner Join the tables Cards and Keywords of the selected card
                    query = "SELECT Keywords.[Name], Keywords.[Description] FROM Card_Keywords " +
                    "INNER JOIN Cards ON Card_Keywords.IDCard = Cards.IDCard " +
                    "INNER JOIN Keywords ON Card_Keywords.IDKeyword = Keywords.IDKeyword " +
                    "WHERE cards.[IDCard] = (SELECT IDCard FROM Cards WHERE [Name] = \'" + selectedcard + "') ORDER BY Keywords.[Name]";
                }
                else
                {
                    query = "SELECT [Name], [Description] FROM Keywords ORDER BY [Name]";
                }
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                //Read the query
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Keyword keyword = new Keyword();

                        //Read the information of the Keyword
                        keyword.Name = reader.GetString(0);
                        keyword.Description = reader.GetString(1);

                        //Add to the local list
                        keywords.Add(keyword);
                    }
                }
                conn.Close();
            }
            catch { conn.Close(); }

        }
        public void GetRarity(string selectedcard)
        {
            try
            {
                raritys.Clear();
                //Open DB connection and create SQL command with the query
                string query = "SELECT Rarity.[Name],Rarity.[DustCost],Rarity.[Disenchant] FROM Cards " +
                    "INNER JOIN Rarity ON Cards.IDRarity = Rarity.IDRarity " +
                    "WHERE cards.[IDCard] = (SELECT IDCard FROM Cards WHERE [Name] = \'" + selectedcard + "')";

                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                //Read the query
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Rarity rarity = new Rarity();
                        //Read the information of the mechanic
                        rarity.Name = reader.GetString(0);
                        rarity.Dustcost = reader.GetInt32(1);
                        rarity.Disenchant = reader.GetInt32(2);

                        //Add to the local list
                        raritys.Add(rarity);
                    }
                }
                conn.Close();
            }
            catch (Exception) { conn.Close(); }
        }
        public void AddCard(string rarity, string type, string naam, int manacost, string klasse, string race)
        {
            try
            {
                //Open DB connection and create SQL command with the query
                string query = "";
                if (naam != "")
                {
                    if (type == "Spell")
                    {
                        query = string.Format("INSERT INTO Cards([IDRarity], [Type], [Name], [ManaCost], [CLass]) " +
                            "VALUES ((SELECT[IDRarity] FROM Rarity WHERE Rarity.[Name] = '{0}'),'{1}','{2}','{3}','{4}');", rarity, type, naam, manacost, klasse);
                    }
                    else if (type == "Weapon")
                    {
                        string.Format("INSERT INTO Cards([IDRarity], [Type], [Name], [ManaCost], [CLass]) " +
                            "VALUES ((SELECT[IDRarity] FROM Rarity WHERE Rarity.[Name] = '{0}'),'{1}','{2}','{3}','{4}');", rarity, type, naam, manacost, klasse);
                    }
                    else if (type == "Minion")
                    {
                        query = string.Format("INSERT INTO Cards([IDRarity], [Type], [Name], [ManaCost], [CLass]) " +
                            "VALUES ((SELECT[IDRarity] FROM Rarity WHERE Rarity.[Name] = '{0}'),'{1}','{2}','{3}','{4}','{5}');", rarity, type, naam, manacost, klasse, race);
                    }
                }
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                //Send the query
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception) { conn.Close(); }
        }
    }
}
