namespace ShoopStock.Core.Massages;

public class LoggMassages
{
    public const string FailedStart = "The application failed to start correctly";

    public const string AppStart = "Application Starting Up";

    public const string UploadFailed = "Upload failed for {0} object with id = {1}";

    public const string CloudServiceUploadError = "Failed to upload{massage}";

    public const string CloudServiceGetError = "Failed to get{massage}";

    public const string TwilioError = "Twilio error! Error message: {ExMessage}";

    public const string EmailServiceConfirmCodeError = "Failed to send code{confirmationCode} to this email{email}";

    public const string EmailServiceError = "Failed to send message, {userId} with email {email}";
}
