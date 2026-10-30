using System.Data;

namespace GSTS.Helpers;

// Small helpers shared by the forms (NFR1 – consistent screens).
public static class UiHelper
{
    // Shows a DataTable in a grid and hides the internal id columns.
    public static void FillGrid(DataGridView grid, DataTable table, params string[] hiddenColumns)
    {
        grid.DataSource = table;
        foreach (string column in hiddenColumns)
        {
            if (grid.Columns.Contains(column))
            {
                grid.Columns[column]!.Visible = false;
            }
        }
    }

    // Returns an int value from the selected grid row, or null when nothing is selected.
    public static int? SelectedId(DataGridView grid, string column)
    {
        if (grid.CurrentRow == null || grid.CurrentRow.Cells[column].Value is DBNull)
        {
            return null;
        }
        return Convert.ToInt32(grid.CurrentRow.Cells[column].Value);
    }

    // Returns a text value from the selected grid row, or "" when nothing is selected.
    public static string SelectedText(DataGridView grid, string column)
    {
        if (grid.CurrentRow == null)
        {
            return "";
        }
        return Convert.ToString(grid.CurrentRow.Cells[column].Value) ?? "";
    }
}
