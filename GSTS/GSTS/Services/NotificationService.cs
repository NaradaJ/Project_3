using System.Data;
using GSTS.Data;

namespace GSTS.Services;

// FR8 – an officer reads the notifications sent to them.
public static class NotificationService
{
    public static DataTable ViewNotifications(int officerId)
    {
        return Db.Query(
            @"SELECT n.notification_id, n.created_at AS `Date`, t.ticket_no AS `Ticket No`,
                     o.name AS `From`, n.message AS `Message`, IF(n.is_read, 'Yes', 'No') AS `Read`,
                     CASE WHEN n.needs_reply = 0 THEN ''
                          WHEN n.replied_at IS NOT NULL THEN 'Replied'
                          WHEN t.status = 'Flagged' THEN 'Awaiting your reply'
                          ELSE 'Resolved' END AS `Reply`,
                     (n.needs_reply = 1 AND n.replied_at IS NULL AND t.status = 'Flagged') AS can_reply
              FROM Notifications n
              JOIN Tickets t ON t.ticket_id = n.ticket_id
              JOIN Officers o ON o.officer_id = n.from_officer_id
              WHERE n.to_officer_id = @officer
              ORDER BY n.notification_id DESC",
            Db.P("@officer", officerId));
    }

    // Marks a notification as read. The officer id is checked so an officer
    // can only change their own notifications.
    public static void MarkAsRead(int notificationId, int officerId)
    {
        Db.Execute(
            "UPDATE Notifications SET is_read = TRUE WHERE notification_id = @id AND to_officer_id = @officer",
            Db.P("@id", notificationId),
            Db.P("@officer", officerId));
    }

    // Shown on the dashboard, e.g. "Notifications (3 unread)".
    public static int UnreadCount(int officerId)
    {
        return Convert.ToInt32(Db.Scalar(
            "SELECT COUNT(*) FROM Notifications WHERE to_officer_id = @officer AND is_read = FALSE",
            Db.P("@officer", officerId)));
    }
}
