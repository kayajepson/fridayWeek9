using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Specialty
  {
    private int _id;
    private int _stylistId;
    private string _name;

    public Specialty (int stylistId, string name, int id = 0)
    {
      _stylistId = stylistId;
      _name = name;
      _id = id;
    }

    public string GetNameSpecialty()
    {
      return _name;
    }

    public void SetNameSpecialty(string newNameSpecialty)
    {
      _name = newNameSpecialty;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }


    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialties = new List<Specialty> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int stylistId = rdr.GetInt32(1);
        string name = rdr.GetString(2);
        Specialty newSpecialty = new Specialty(stylistId, name, id);
        allSpecialties.Add(newSpecialty);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allSpecialties;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        bool idEquality = (this.GetId() == newSpecialty.GetId());
        bool nameEquality = (this.GetNameSpecialty() == newSpecialty.GetNameSpecialty());
        return (idEquality && nameEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialties (stylist_id, name) VALUES (@stylistId, @NameSpecialty);";
      cmd.Parameters.AddWithValue("@NameSpecialty", _name);
      cmd.Parameters.AddWithValue("@stylistId", _stylistId);
      cmd.ExecuteNonQuery();

      _id = (int) cmd.LastInsertedId;

      var cmd2 = conn.CreateCommand() as MySqlCommand;
      cmd2.CommandText = @"INSERT INTO specialties_stylists (stylist_id, specialty_id) VALUES (@stylistId, @SpecialtyId);";
      cmd2.Parameters.AddWithValue("@SpecialtyId", _id);
      cmd2.Parameters.AddWithValue("@stylistId", _stylistId);
      cmd2.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Specialty Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties WHERE id = (@searchId);";
      cmd.Parameters.AddWithValue("@searchId", id);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int specialtyId = 0;
      string name = "";
      int stylistId = 0;
      while(rdr.Read())
      {
        specialtyId = rdr.GetInt32(0);
        stylistId = rdr.GetInt32(1);
        name = rdr.GetString(2);
      }

      Specialty newSpecialty = new Specialty(stylistId, name, specialtyId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newSpecialty;
    }

    public List<Stylist> GetStylists()
    {
      List<Stylist> allStylists = new List<Stylist>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.id, stylists.name FROM specialties_stylists JOIN stylists ON (specialties_stylists.stylist_id = stylists.id) WHERE specialties_stylists.specialty_id = @specialtyId;";
      cmd.Parameters.AddWithValue("@specialtyId", _id);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int thisStylistId = rdr.GetInt32(0);
        string nameStylist = rdr.GetString(1);
        Stylist newStylist = new Stylist(nameStylist, thisStylistId);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }


    public void Edit(string newNameSpecialty)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE specialties SET name = @newNameSpecialty WHERE id = @searchId;";
      cmd.Parameters.AddWithValue("@searchId", _id);
      cmd.Parameters.AddWithValue("@newNameSpecialty", newNameSpecialty);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void AssignStylist(int specialtyId, int stylistId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialties_stylists (stylist_id, specialty_id) VALUES (@stylistId, @specialtyId);";
      cmd.Parameters.AddWithValue("@stylistId", stylistId);
      cmd.Parameters.AddWithValue("@specialtyId", specialtyId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties WHERE id = @thisId;";
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
