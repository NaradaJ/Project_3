using System.Data;
using GSTS.Data;
using GSTS.Helpers;
using GSTS.Models;

namespace GSTS.Services;

// Officer sign-in. Officer accounts are pre-registered (Report 1.4); the application
// needs to know the current officer because every action is logged with the officer ID (FR9).
public static class AuthService
{
    // Returns the officer when the username and password are correct, otherwise null.
    public static Officer? Login(string username, string password)
    {
        DataRow? row = Db.QueryRow(
            "SELECT * FROM Officers WHERE username = @username AND is_active = TRUE",
            Db.P("@username", username.Trim()));

        if (row == null)
        {
            return null;
        }

        Officer officer = Officer.FromRow(row);
        return PasswordHasher.VerifyHash(password, officer.PasswordHash) ? officer : null;
    }
}
