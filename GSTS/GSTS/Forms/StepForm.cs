using GSTS.Models;

namespace GSTS.Forms;

// FR4 – enter a step or document, its stage, and whether it is compulsory (Flowchart 2).
// The values are saved by TicketForm through StepService.
public partial class StepForm : Form
{
    public string Description => txtDescription.Text;
    public int Stage => (int)numStage.Value;
    public bool IsCompulsory => chkCompulsory.Checked;

    // existing = the step being edited, or null to add a new step.
    // currentStage = the ticket's current stage (steps cannot go into an earlier stage).
    public StepForm(Step? existing, int currentStage)
    {
        InitializeComponent();
        numStage.Minimum = currentStage;
        numStage.Value = currentStage;

        if (existing != null)
        {
            Text = "GSTS – Edit Step";
            lblTitle.Text = "Edit Step";
            txtDescription.Text = existing.Description;
            numStage.Value = Math.Max(existing.Stage, currentStage);
            chkCompulsory.Checked = existing.IsCompulsory;
        }
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}
