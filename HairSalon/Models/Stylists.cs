using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {
    private int _id;
    private string _stylistName;
    private string _clientName;

    public Stylist(string stylistName, string _clientName, int id = 0)
    {
      _id = Id;
      _clientName = clientName;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetId() == newStylist.GetId());
        bool stylistNameEquality = (this.GetStylistName() == newStylistName.GetStylistName());
        bool clientNameEquality = (this.GetClientName() == newClientName.GetClientName());

        return (stylistNameEquality && clientNameEquality && idEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public string GetStylistName()
    {
      return _stylistName;
    }

    public string GetClientName()
    {
      return _clientName;
    }

    public int GetId()
    {
      return _id;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO hairsalon (stylist, client) VALUES (@stylist, @client);";

      MySqlParameter stylist = new MySqlParameter();
      stylist.ParameterName = "@stylist";
      stylist.Value = this.stylistName;
      cmd.Parameters.Add(stylist);

      MySqlParameter client = new MySqlParameter();
      client.ParameterName = "@client";
      client.Value = this.clientName;
      cmd.Parameters.Add(client);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM hairsalon;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);

        Stylist newStylist = new Stylist(stylistName, stylistId);
      }
      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = @"DELETE FROM hairsalon;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Stylist Find(int Id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `hairsalon` WHERE id = @thisId ORDER BY id DESC;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int stylistId = 0;
      string stylistName = "";

      while (rdr.Read())
      {
        stylistId = rdr.GetInt32(0);
        stylistName = rdr.GetString(1);
      }

      Stylist newStylist = new Stylist(stylistName, stylistId);
      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
      return newStylist;
    }

    public void UpdateName(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE hairsalon SET stylist = @newStylist WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter stylist = new MySqlParameter();
      stylist.ParameterName = "@newStylist";
      stylist.Value = newStylist;
      cmd.Parameters.Add(stylist);

      cmd.ExecuteNonQuery();
      _stylistName = newStylist;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void DeleteRestaurant()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM hairsalon WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
