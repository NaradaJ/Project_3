using System.Text.RegularExpressions;

namespace GSTS.Helpers;

// Input checks used when a ticket is created (FR1).
public static class Validator
{
    // Sri Lankan NIC: old format = 9 digits + V or X (e.g. 901234567V),
    // new format = 12 digits (e.g. 199012345678).
    // Only the format is checked: real ID verification is out of scope (Report 1.4).
    public static bool ValidIdNumber(string idNumber)
    {
        return Regex.IsMatch(idNumber, @"^(\d{9}[VvXx]|\d{12})$");
    }

    // Phone number: 10 digits starting with 0 (e.g. 0771234567).
    public static bool ValidPhone(string phone)
    {
        return Regex.IsMatch(phone, @"^0\d{9}$");
    }
}
