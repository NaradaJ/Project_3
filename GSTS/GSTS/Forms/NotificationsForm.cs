using GSTS.Helpers;
using GSTS.Services;

namespace GSTS.Forms;

// FR8 – An officer reads the notifications sent to them and marks them as read.
// The full message (including the flagging officer's note) is shown below the list.
// D15 – a flag notification is answered with Accept Flag or Deny Flag and a note.
public partial class NotificationsForm : Form
{
    public NotificationsForm()
    {
        UiHelper.FitOnScreen(this);
        InitializeComponent();
    }

    private void NotificationsForm_Load(object sender, EventArgs e)
    {
        LoadNotifications();
    }

    private void LoadNotifications()
    {
        UiHelper.FillGrid(gridNotifications, NotificationService.ViewNotifications(Session.OfficerId),
            "notification_id", "can_reply");
        lblCount.Text = NotificationService.UnreadCount(Session.OfficerId) + " unread";
        ShowSelected();
    }

    // Shows the whole message of the selected notification and enables the
    // reply buttons only for a flag that is still waiting for this officer's reply.
    private void ShowSelected()
    {
        txtMessage.Text = UiHelper.SelectedText(gridNotifications, "Message");
        bool canReply = gridNotifications.CurrentRow != null
                        && UiHelper.SelectedId(gridNotifications, "can_reply") == 1;
        btnAcceptFlag.Enabled = canReply;
        btnDenyFlag.Enabled = canReply;
    }

    private void gridNotifications_SelectionChanged(object sender, EventArgs e)
    {
        ShowSelected();
    }

    // The grid selects its first row only once it is on screen, so show that row's message then.
    private void NotificationsForm_Shown(object sender, EventArgs e)
    {
        ShowSelected();
    }

    // WHEN the user opens a notification: mark it as read.
    private void MarkSelectedAsRead()
    {
        int? id = UiHelper.SelectedId(gridNotifications, "notification_id");
        if (id == null)
        {
            Messages.ShowError("Please select a notification");
            return;
        }
        NotificationService.MarkAsRead(id.Value, Session.OfficerId);
        LoadNotifications();
    }

    // D15 – reply to the selected flag with a note.
    private void ReplyToFlag(bool accept)
    {
        int? id = UiHelper.SelectedId(gridNotifications, "notification_id");
        if (id == null)
        {
            Messages.ShowError("Please select a notification");
            return;
        }

        Result result = ReviewService.ReplyToFlag(Session.OfficerId, id.Value, accept, txtReply.Text);
        if (!result.Success)
        {
            Messages.ShowError(result.Message);
            LoadNotifications();
            return;
        }

        Messages.ShowInfo(result.Message);
        txtReply.Clear();
        LoadNotifications();
    }

    private void btnMarkRead_Click(object sender, EventArgs e)
    {
        MarkSelectedAsRead();
    }

    private void btnAcceptFlag_Click(object sender, EventArgs e)
    {
        ReplyToFlag(accept: true);
    }

    private void btnDenyFlag_Click(object sender, EventArgs e)
    {
        ReplyToFlag(accept: false);
    }

    private void gridNotifications_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            MarkSelectedAsRead();
        }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
