namespace GSTS;

// Database connection settings.
// NFR8 – Portability: the connection string is read from the file "gsts.config" next to
// GSTS.exe, so the installed application can point to any MariaDB/MySQL server without
// rebuilding. The installer writes this file. If the file is missing (for example when
// running from Visual Studio), the XAMPP defaults below are used: user "root", no password.
// See DEVIATIONS.md, T2.
public static class AppConfig
{
    public const string DefaultConnectionString =
        "Server=localhost;Port=3306;Database=gsts;User ID=root;Password=;";

    public static readonly string ConfigFilePath =
        Path.Combine(AppContext.BaseDirectory, "gsts.config");

    public static readonly string ConnectionString = LoadConnectionString();

    // Returns the first line of gsts.config that is not empty and not a comment (#).
    private static string LoadConnectionString()
    {
        if (!File.Exists(ConfigFilePath))
        {
            return DefaultConnectionString;
        }

        foreach (string line in File.ReadAllLines(ConfigFilePath))
        {
            string text = line.Trim();
            if (text.Length > 0 && !text.StartsWith('#'))
            {
                return text;
            }
        }

        return DefaultConnectionString;
    }
}
