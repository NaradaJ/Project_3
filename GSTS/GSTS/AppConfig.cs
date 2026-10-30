namespace GSTS;

// Database connection settings (XAMPP MariaDB defaults: user "root", no password).
// Change these values if your XAMPP MySQL uses a password or a different port.
public static class AppConfig
{
    public const string ConnectionString =
        "Server=localhost;Port=3306;Database=gsts;User ID=root;Password=;";
}
