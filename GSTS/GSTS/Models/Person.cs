using System.Data;

namespace GSTS.Models;

// One row of the Persons table.
public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; } = "";
    public string Nic { get; set; } = "";
    public string Contact { get; set; } = "";
    public string Address { get; set; } = "";
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string Role { get; set; } = "";
    public bool IsActive { get; set; }

    public static Person FromRow(DataRow row)
    {
        return new Person
        {
            PersonId = Convert.ToInt32(row["person_id"]),
            Name = Convert.ToString(row["name"]) ?? "",
            Nic = Convert.ToString(row["nic"]) ?? "",
            Contact = Convert.ToString(row["contact"]) ?? "",
            Address = Convert.ToString(row["address"]) ?? "",
            Username = Convert.ToString(row["username"]) ?? "",
            PasswordHash = Convert.ToString(row["password_hash"]) ?? "",
            Role = Convert.ToString(row["role"]) ?? "",
            IsActive = Convert.ToBoolean(row["is_active"])
        };
    }
}
