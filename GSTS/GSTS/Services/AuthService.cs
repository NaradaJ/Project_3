using System.Data;
using GSTS.Data;
using GSTS.Helpers;
using GSTS.Models;

namespace GSTS.Services;

// Registration, login and password change (FR1, FR2, FR16).
public static class AuthService
{
    // FR1 – User Registration (Report §3.4 RegisterUser, Figure 1)
    public static Result RegisterUser(string name, string nic, string contact, string address, string password)
    {
        name = name.Trim();
        nic = nic.Trim().ToUpper();
        contact = contact.Trim();
        address = address.Trim();

        if (name == "" || nic == "" || contact == "" || address == "" || password == "")
        {
            return Result.Fail("All fields are required");
        }

        if (!Validator.ValidNIC(nic) || !Validator.ValidPhone(contact) || !Validator.ValidPassword(password))
        {
            return Result.Fail("Invalid NIC, contact number or password");
        }

        object? existing = Db.Scalar("SELECT 1 FROM Persons WHERE nic = @nic", Db.P("@nic", nic));
        if (existing != null)
        {
            return Result.Fail("This NIC is already registered");
        }

        string hash = PasswordHasher.HashPassword(password);

        // The citizen's username is their NIC (as in the pseudocode).
        Db.Execute(
            @"INSERT INTO Persons (name, nic, contact, address, username, password_hash, role, is_active)
              VALUES (@name, @nic, @contact, @address, @nic, @hash, 'Citizen', TRUE)",
            Db.P("@name", name),
            Db.P("@nic", nic),
            Db.P("@contact", contact),
            Db.P("@address", address),
            Db.P("@hash", hash));

        return Result.Ok("Registration successful. You can now log in");
    }

    // FR2 – Login (Report §3.4 Login, Figure 2)
    // Returns the user when the username and password are correct, otherwise null.
    // The 3-attempt limit is counted by the LoginForm, because it lasts for the session.
    public static Person? Login(string username, string password, out int? officerId)
    {
        officerId = null;

        DataRow? row = Db.QueryRow(
            "SELECT * FROM Persons WHERE username = @username AND is_active = TRUE",
            Db.P("@username", username.Trim()));

        if (row == null)
        {
            return null;
        }

        Person user = Person.FromRow(row);
        if (!PasswordHasher.VerifyHash(password, user.PasswordHash))
        {
            return null;
        }

        // An officer also needs their officer_id for the officer screens.
        if (user.Role == "Officer")
        {
            object? id = Db.Scalar(
                "SELECT officer_id FROM Officers WHERE person_id = @person AND is_active = TRUE",
                Db.P("@person", user.PersonId));
            if (id == null)
            {
                return null; // deactivated officer
            }
            officerId = Convert.ToInt32(id);
        }

        return user;
    }

    // FR16 – Change Password (Report §3.4 ChangePassword)
    public static Result ChangePassword(int personId, string oldPassword, string newPassword, string confirmPassword)
    {
        DataRow? row = Db.QueryRow("SELECT * FROM Persons WHERE person_id = @id", Db.P("@id", personId));
        if (row == null)
        {
            return Result.Fail("User not found");
        }

        Person user = Person.FromRow(row);
        if (!PasswordHasher.VerifyHash(oldPassword, user.PasswordHash))
        {
            return Result.Fail("Current password is incorrect");
        }

        if (newPassword != confirmPassword || !Validator.ValidPassword(newPassword))
        {
            return Result.Fail("New password is not valid");
        }

        Db.Execute("UPDATE Persons SET password_hash = @hash WHERE person_id = @id",
            Db.P("@hash", PasswordHasher.HashPassword(newPassword)),
            Db.P("@id", personId));

        return Result.Ok("Password changed");
    }
}
