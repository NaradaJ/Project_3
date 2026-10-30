using System.Data;
using GSTS.Data;

namespace GSTS.Services;

// FR8 – Notifications (Report §3.4 ViewNotifications)
public static class NotificationService
{
    public static DataTable ViewNotifications(int personId)
    {
        return Db.Query(
            @"SELECT n.notification_id, n.created_at AS `Date`, t.ticket_no AS `Ticket No`,
                     n.message AS `Message`, IF(n.is_read, 'Yes', 'No') AS `Read`
              FROM Notifications n
              LEFT JOIN Tickets t ON t.ticket_id = n.ticket_id
              WHERE n.person_id = @person
              ORDER BY n.created_at DESC, n.notification_id DESC",
            Db.P("@person", personId));
    }

    // WHEN the user opens a notification: mark it as read.
    // person_id is checked so a user can only change their own notifications.
    public static void MarkAsRead(int notificationId, int personId)
    {
        Db.Execute(
            "UPDATE Notifications SET is_read = TRUE WHERE notification_id = @id AND person_id = @person",
            Db.P("@id", notificationId),
            Db.P("@person", personId));
    }

    // Shown on every dashboard (e.g. "Notifications (3 unread)").
    public static int UnreadCount(int personId)
    {
        return Convert.ToInt32(Db.Scalar(
            "SELECT COUNT(*) FROM Notifications WHERE person_id = @person AND is_read = FALSE",
            Db.P("@person", personId)));
    }
}
