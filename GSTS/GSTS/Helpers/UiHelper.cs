using System.Data;

namespace GSTS.Helpers;

// Small helpers shared by the forms (NFR Usability – consistent screens).
public static class UiHelper
{
    // Shows a DataTable in a grid and hides the internal id columns.
    // Every grid in the application is filled here, so every grid gets the same
    // "nothing is cut off" layout (see SizeColumns).
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
        SizeColumns(grid);
    }

    // Makes every value in the grid fully readable:
    //   - text wraps onto more lines at spaces, and rows grow taller to show it;
    //   - a column is never narrower than its longest single word (or header word),
    //     so no word is ever cut;
    //   - the spare width goes to the columns with the most text (messages, notes, steps);
    //   - only if even the single words do not fit, a horizontal scroll bar appears.
    private static void SizeColumns(DataGridView grid)
    {
        // Measure the values on one line first, then switch wrapping on.
        grid.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        int longText = TextRenderer.MeasureText(new string('n', 36), grid.Font).Width;

        foreach (DataGridViewColumn column in grid.Columns)
        {
            if (!column.Visible)
            {
                continue;
            }
            int oneLine = column.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            int longestWord = WordWidth(column.HeaderText, grid.ColumnHeadersDefaultCellStyle.Font ?? grid.Font) + 10;
            foreach (DataGridViewRow row in grid.Rows)
            {
                string text = Convert.ToString(row.Cells[column.Index].FormattedValue) ?? "";
                longestWord = Math.Max(longestWord, WordWidth(text, grid.Font) + 8);
            }

            column.MinimumWidth = Math.Max(longestWord, 20);
            column.FillWeight = Math.Max(column.MinimumWidth, Math.Min(oneLine, longText * 2));
        }

        grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
        grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        grid.ScrollBars = ScrollBars.Both;
    }

    // The width of the widest single word in a text.
    private static int WordWidth(string text, Font font)
    {
        int widest = 0;
        foreach (string word in text.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            widest = Math.Max(widest, TextRenderer.MeasureText(word, font).Width);
        }
        return widest;
    }

    // Keeps every part of a window visible at any Windows display scaling (e.g. 150 % or 175 %).
    // Call it in the form's constructor, just BEFORE InitializeComponent().
    //
    // Why: InitializeComponent enlarges the controls for the scaling, but Windows will not
    // make a window taller or wider than the screen. Controls anchored to the bottom or right
    // edge are then pushed onto the controls above them, and the rest of the window is cut off.
    //
    // How: while InitializeComponent runs (before scaling), the designed size of the window
    // and the designed position of every control are recorded. When the window loads:
    //   - if the scaled design does not fit, every control is put at its designed position
    //     times the display scale, and the window gets scroll bars, so every part can be reached;
    //   - the window cannot be made smaller than its design (or the screen), so controls
    //     are never squeezed on top of each other;
    //   - the window is centred over its owner, inside the screen.
    public static void FitOnScreen(Form form)
    {
        Size design = Size.Empty;
        SizeF designFont = SizeF.Empty;
        var designBounds = new Dictionary<Control, Rectangle>();

        // The first size InitializeComponent gives the window is its designed size.
        void RecordSize(object? sender, EventArgs e)
        {
            design = form.ClientSize;
            designFont = form.AutoScaleDimensions;
            form.ClientSizeChanged -= RecordSize;
        }

        // Controls are added to the window before InitializeComponent scales them.
        void RecordControl(object? sender, ControlEventArgs e)
        {
            if (e.Control != null && !designBounds.ContainsKey(e.Control))
            {
                designBounds[e.Control] = e.Control.Bounds;
            }
        }

        form.ClientSizeChanged += RecordSize;
        form.ControlAdded += RecordControl;
        form.Load += (sender, e) =>
        {
            form.ControlAdded -= RecordControl;
            FitLoadedForm(form, design, designFont, designBounds);
        };
    }

    private static void FitLoadedForm(Form form, Size design, SizeF designFont, Dictionary<Control, Rectangle> designBounds)
    {
        Rectangle area = Screen.FromControl(form.Owner ?? form).WorkingArea;

        // The display scale, and the client size the scaled design needs.
        SizeF scale = designFont.Width > 0 && designFont.Height > 0
            ? new SizeF(form.CurrentAutoScaleDimensions.Width / designFont.Width,
                        form.CurrentAutoScaleDimensions.Height / designFont.Height)
            : new SizeF(1, 1);
        int neededWidth = (int)Math.Round(design.Width * scale.Width);
        int neededHeight = (int)Math.Round(design.Height * scale.Height);
        bool tooBig = neededWidth > form.ClientSize.Width || neededHeight > form.ClientSize.Height
                      || form.Width > area.Width || form.Height > area.Height;

        if (tooBig)
        {
            form.SuspendLayout();
            foreach (Control control in form.Controls)
            {
                control.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                if (designBounds.TryGetValue(control, out Rectangle b))
                {
                    control.Bounds = new Rectangle(
                        (int)Math.Round(b.X * scale.Width), (int)Math.Round(b.Y * scale.Height),
                        (int)Math.Round(b.Width * scale.Width), (int)Math.Round(b.Height * scale.Height));
                }
            }
            form.AutoScroll = true;
            form.ResumeLayout(true);
        }

        form.MinimumSize = new Size(Math.Min(form.Width, area.Width), Math.Min(form.Height, area.Height));
        form.Width = Math.Min(form.Width, area.Width);
        form.Height = Math.Min(form.Height, area.Height);

        Rectangle centreOn = form.Owner != null ? form.Owner.Bounds : area;
        int left = centreOn.Left + (centreOn.Width - form.Width) / 2;
        int top = centreOn.Top + (centreOn.Height - form.Height) / 2;
        form.Left = Math.Max(area.Left, Math.Min(left, area.Right - form.Width));
        form.Top = Math.Max(area.Top, Math.Min(top, area.Bottom - form.Height));
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
