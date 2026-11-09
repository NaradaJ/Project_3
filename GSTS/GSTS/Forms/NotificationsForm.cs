using GSTS.Helpers;
using GSTS.Services;

namespace GSTS.Forms;

// FR8 – An officer reads the notifications sent to them and marks them as read.
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
        UiHelper.FillGrid(gridNotifications, NotificationService.ViewNotifications(Session.OfficerId), "notification_id");
        lblCount.Text = NotificationService.UnreadCount(Session.OfficerId) + " unread";
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
