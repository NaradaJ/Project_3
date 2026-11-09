using System.Data;

namespace GSTS.Models;

// One row of the Officers table (a pre-registered officer account).
public class Officer
{
    public int OfficerId { get; set; }
    public string Name { get; set; } = "";
    public string Designation { get; set; } = "";
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public bool IsActive { get; set; }

    public static Officer FromRow(DataRow row)
    {
        return new Officer
        {
            OfficerId = Convert.ToInt32(row["officer_id"]),
            Name = Convert.ToString(row["name"]) ?? "",
            Designation = Convert.ToString(row["designation"]) ?? "",
            Username = Convert.ToString(row["username"]) ?? "",
            PasswordHash = Convert.ToString(row["password_hash"]) ?? "",
            IsActive = Convert.ToBoolean(row["is_active"])
        };
    }
}
