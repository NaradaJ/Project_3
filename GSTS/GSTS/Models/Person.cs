using System.Data;

namespace GSTS.Models;

// One row of the Persons table: the citizen who brings a request (FR1).
public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; } = "";
    public string IdNumber { get; set; } = "";
    public string PhoneNumber { get; set; } = "";

    public static Person FromRow(DataRow row)
    {
        return new Person
        {
            PersonId = Convert.ToInt32(row["person_id"]),
            Name = Convert.ToString(row["name"]) ?? "",
            IdNumber = Convert.ToString(row["id_number"]) ?? "",
            PhoneNumber = Convert.ToString(row["phone_number"]) ?? ""
        };
    }
}
