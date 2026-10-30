using MySqlConnector;

namespace GSTS.Helpers;

// NFR1 / NFR5 – One place for all message boxes, so every screen shows
// messages the same way.
public static class Messages
{
    public static void ShowInfo(string message)
    {
        MessageBox.Show(message, "GSTS", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public static void ShowError(string message)
    {
        MessageBox.Show(message, "GSTS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static bool Confirm(string message)
    {
        return MessageBox.Show(message, "GSTS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }

    // NFR5 – Turns an error into a clear message instead of crashing the application.
    public static void ShowException(Exception ex)
    {
        if (ex is MySqlException mysqlEx && mysqlEx.ErrorCode == MySqlErrorCode.UnableToConnectToHost)
        {
            ShowError("Cannot connect to the database.\n\n" +
                      "Please open the XAMPP Control Panel, start MySQL, and try again.");
        }
        else if (ex is MySqlException)
        {
            ShowError("A database error occurred:\n\n" + ex.Message);
        }
        else
        {
            ShowError("An unexpected error occurred:\n\n" + ex.Message);
        }
    }
}
