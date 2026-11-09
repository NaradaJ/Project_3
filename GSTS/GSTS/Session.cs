using GSTS.Models;

namespace GSTS;

// Holds the officer who is using the application (pseudocode: current officer).
public static class Session
{
    public static Officer? CurrentOfficer { get; private set; }

    public static int OfficerId => CurrentOfficer!.OfficerId;

    public static void Start(Officer officer)
    {
        CurrentOfficer = officer;
    }

    public static void Clear()
    {
        CurrentOfficer = null;
    }
}
