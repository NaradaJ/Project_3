using GSTS.Models;

namespace GSTS;

// Holds the user who is currently logged in (pseudocode: SESSION.user).
public static class Session
{
    public static Person? CurrentUser { get; private set; }

    // Set only when the logged-in user is an officer.
    public static int? OfficerId { get; private set; }

    public static int PersonId => CurrentUser!.PersonId;
    public static string Role => CurrentUser?.Role ?? "";

    public static void Start(Person user, int? officerId)
    {
        CurrentUser = user;
        OfficerId = officerId;
    }

    // FR16 – Logout: CLEAR SESSION
    public static void Clear()
    {
        CurrentUser = null;
        OfficerId = null;
    }
}
