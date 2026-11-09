using System.Data;
using MySqlConnector;

namespace GSTS.Data;

// Database access helper. Every query in the system goes through these methods.
// Security: all values are passed as parameters (@name), never joined into the SQL text,
// which prevents SQL injection.
public static class Db
{
    // Opens and returns a new connection. Always use it inside a "using" block
    // so the connection is closed automatically.
    public static MySqlConnection GetConnection()
    {
        var connection = new MySqlConnection(AppConfig.ConnectionString);
        connection.Open();
        return connection;
    }

    // Creates a parameter. C# null becomes SQL NULL.
    public static MySqlParameter P(string name, object? value)
    {
        return new MySqlParameter(name, value ?? DBNull.Value);
    }

    // ---- Methods that work inside an existing connection / transaction ----

    // Runs a SELECT and returns all rows as a DataTable.
    public static DataTable Query(MySqlConnection conn, MySqlTransaction? tx, string sql, params MySqlParameter[] parameters)
    {
        using var command = CreateCommand(conn, tx, sql, parameters);
        using var reader = command.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    // Runs a SELECT and returns the first row, or null when there are no rows.
    public static DataRow? QueryRow(MySqlConnection conn, MySqlTransaction? tx, string sql, params MySqlParameter[] parameters)
    {
        DataTable table = Query(conn, tx, sql, parameters);
        return table.Rows.Count > 0 ? table.Rows[0] : null;
    }

    // Runs a SELECT that returns a single value (e.g. COUNT(*)).
    public static object? Scalar(MySqlConnection conn, MySqlTransaction? tx, string sql, params MySqlParameter[] parameters)
    {
        using var command = CreateCommand(conn, tx, sql, parameters);
        object? result = command.ExecuteScalar();
        return result is DBNull ? null : result;
    }

    // Runs an UPDATE or DELETE and returns the number of rows changed.
    public static int Execute(MySqlConnection conn, MySqlTransaction? tx, string sql, params MySqlParameter[] parameters)
    {
        using var command = CreateCommand(conn, tx, sql, parameters);
        return command.ExecuteNonQuery();
    }

    // Runs an INSERT and returns the new AUTO_INCREMENT id.
    public static int Insert(MySqlConnection conn, MySqlTransaction? tx, string sql, params MySqlParameter[] parameters)
    {
        using var command = CreateCommand(conn, tx, sql, parameters);
        command.ExecuteNonQuery();
        return (int)command.LastInsertedId;
    }

    // ---- Short versions that open their own connection (for single statements) ----

    public static DataTable Query(string sql, params MySqlParameter[] parameters)
    {
        using var conn = GetConnection();
        return Query(conn, null, sql, parameters);
    }

    public static DataRow? QueryRow(string sql, params MySqlParameter[] parameters)
    {
        using var conn = GetConnection();
        return QueryRow(conn, null, sql, parameters);
    }

    public static object? Scalar(string sql, params MySqlParameter[] parameters)
    {
        using var conn = GetConnection();
        return Scalar(conn, null, sql, parameters);
    }

    public static int Execute(string sql, params MySqlParameter[] parameters)
    {
        using var conn = GetConnection();
        return Execute(conn, null, sql, parameters);
    }

    // Used at start-up to check that XAMPP MySQL is running (NFR Reliability).
    public static void TestConnection()
    {
        using var conn = GetConnection();
    }

    private static MySqlCommand CreateCommand(MySqlConnection conn, MySqlTransaction? tx, string sql, MySqlParameter[] parameters)
    {
        var command = new MySqlCommand(sql, conn, tx);
        command.Parameters.AddRange(parameters);
        return command;
    }
}
