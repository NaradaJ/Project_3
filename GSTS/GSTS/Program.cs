using GSTS.Data;
using GSTS.Forms;
using GSTS.Helpers;

namespace GSTS;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // NFR Reliability – any error inside a screen is shown as a clear message; the application keeps running.
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (sender, e) => Messages.ShowException(e.Exception);

        // NFR Reliability – check the database connection at start-up.
        try
        {
            Db.TestConnection();
        }
        catch (Exception ex)
        {
            Messages.ShowException(ex);
        }

        Application.Run(new LoginForm());
    }
}
