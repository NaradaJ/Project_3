using System.Data;

namespace GSTS.Models;

// One row of the Officers table, together with the officer's name from Persons.
public class Officer
{
    public int OfficerId { get; set; }
    public int PersonId { get; set; }
    public string Name { get; set; } = "";
    public string Nic { get; set; } = "";
    public string Contact { get; set; } = "";
    public string Designation { get; set; } = "";
    public string Department { get; set; } = "";
    public string HandlesStep { get; set; } = "";
    public bool IsActive { get; set; }

    // Expects a row from: SELECT o.*, p.name, p.nic, p.contact FROM Officers o JOIN Persons p ...
    public static Officer FromRow(DataRow row)
    {
        return new Officer
        {
            OfficerId = Convert.ToInt32(row["officer_id"]),
            PersonId = Convert.ToInt32(row["person_id"]),
            Name = Convert.ToString(row["name"]) ?? "",
            Nic = Convert.ToString(row["nic"]) ?? "",
            Contact = Convert.ToString(row["contact"]) ?? "",
            Designation = Convert.ToString(row["designation"]) ?? "",
            Department = Convert.ToString(row["department"]) ?? "",
            HandlesStep = Convert.ToString(row["handles_step"]) ?? "",
            IsActive = Convert.ToBoolean(row["is_active"])
        };
    }
}
