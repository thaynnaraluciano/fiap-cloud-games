namespace CrossCutting.Configuration.AppModels
{
    public class SmtpSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Password { get; set; }

        public string MailFromName { get; set; }

        public string MailFromEmail { get; set; }
    }
}
