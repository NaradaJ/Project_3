using System.Text.RegularExpressions;

namespace GSTS.Helpers;

// FR1 – Input validation rules used by RegisterUser and ManageOfficer.
public static class Validator
{
    // Sri Lankan NIC: old format = 9 digits + V or X (e.g. 901234567V),
    // new format = 12 digits (e.g. 199012345678).
    public static bool ValidNIC(string nic)
    {
        return Regex.IsMatch(nic, @"^(\d{9}[VvXx]|\d{12})$");
    }

    // Phone number: 10 digits starting with 0 (e.g. 0771234567).
    public static bool ValidPhone(string contact)
    {
        return Regex.IsMatch(contact, @"^0\d{9}$");
    }

    // Password must be at least 8 characters (pseudocode: LENGTH(password) < 8 is invalid).
    public static bool ValidPassword(string password)
    {
        return password.Length >= 8;
    }
}
