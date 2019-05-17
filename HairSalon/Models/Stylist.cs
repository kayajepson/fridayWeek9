using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Stylist
  {
    private string _name;
    private string _specialty;
    private int _id;

    public Stylist(string stylistName, string stylistSpecialty, int id = 0)
    {
      _name = stylistName;
      _specialty = stylistSpecialty;
      _id = id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetSpecialty()
    {
      return _specialty;
    }

    public int GetId()
    {
      return _id;
    }


    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int StylistId = rdr.GetInt32(0);
        string StylistName = rdr.GetString(1);
        string stylistSpecialty = rdr.GetString(2);
        Stylist newStylist = new Stylist(StylistName, stylistSpecialty, StylistId);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }

    public static Stylist Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int StylistId = 0;
        string StylistName = "";
        string stylistSpecialty = "";
        while(rdr.Read())
        {
          StylistId = rdr.GetInt32(0);
          StylistName = rdr.GetString(1);
          stylistSpecialty = rdr.GetString(2);
        }
        Stylist newStylist = new Stylist(StylistName, stylistSpecialty, StylistId);
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return newStylist;
      }

      public List<Client> GetClients()
      {
        List<Client> allStylistClients = new List<Client>{};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM clients WHERE stylistId = @stylistId;";
        MySqlParameter stylistId = new MySqlParameter();
        stylistId.ParameterName = "@stylistId";
        stylistId.Value = this._id;
        cmd.Parameters.Add(stylistId);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
           int clientId = rdr.GetInt32(0);
           string nameClient = rdr.GetString(1);
           int clientStylistId = rdr.GetInt32(2);
           Client newClient = new Client(nameClient, clientStylistId, clientId);
           allStylistClients.Add(newClient);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allStylistClients;
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
        bool idEquality = this.GetId().Equals(newStylist.GetId());
        bool nameEquality = this.GetName().Equals(newStylist.GetName());
        return (idEquality && nameEquality);
      }
    }

      public void Save()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO stylists (name, specialty) VALUES (@name, @specialty);";
        MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@name";
        name.Value = this._name;
        cmd.Parameters.Add(name);
        MySqlParameter specialty = new MySqlParameter();
        specialty.ParameterName = "@specialty";
        specialty.Value = this._specialty;
        cmd.Parameters.Add(specialty);
        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public void DeleteStylist(int stylistId)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;

        Stylist selectedStylist = Stylist.Find(stylistId);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Client> stylistClients = selectedStylist.GetClients();
        model.Add("stylist", selectedStylist);

        foreach (Client client in stylistClients)
        {
          client.Delete();
        }

        cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId;";
        MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = _id;
        cmd.Parameters.Add(thisId);
        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

  }
}
