namespace HospitalCompleteSystem.Models;

public class EmailLog
{
    public int Id { get; internal set; }
    public int AppointmentId { get; private set; }
    public string ToEmail { get; private set; }
    public string Subject { get; private set; }
    public string Message { get; private set; }
    public bool IsSent { get; private set; }
    public string? ErrorMessage { get; private set; }
    public DateTime SentDate { get; private set; }

    public EmailLog(int appointmentId, string toEmail, string subject, string message)
    {
        AppointmentId = appointmentId;
        ToEmail = toEmail;
        Subject = subject;
        Message = message;
        SentDate = DateTime.Now;
    }

    public void MarkAsSent()
    {
        IsSent = true;
        ErrorMessage = null;
    }

    public void MarkAsFailed(string error)
    {
        IsSent = false;
        ErrorMessage = error;
    }
}
