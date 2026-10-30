using GSTS.Helpers;
using GSTS.Services;

namespace GSTS.Forms;

// FR8 – View notifications and mark them as read (Report §3.4 ViewNotifications)
public partial class NotificationsForm : Form
{
    public NotificationsForm()
    {
        InitializeComponent();
    }

    private void NotificationsForm_Load(object sender, EventArgs e)
    {
        LoadNotifications();
    }

    private void LoadNotifications()
    {
        UiHelper.FillGrid(gridNotifications, NotificationService.ViewNotifications(Session.PersonId), "notification_id");
        lblCount.Text = NotificationService.UnreadCount(Session.PersonId) + " unread";
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
        NotificationService.MarkAsRead(id.Value, Session.PersonId);
        LoadNotifications();
    }

    private void btnMarkRead_Click(object sender, EventArgs e)
    {
        MarkSelectedAsRead();
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
