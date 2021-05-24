namespace Zindagi.Infra.Options
{
    public class SmsOptions
    {
        public const string AppSettingsSection = "sms";

        public bool Disable { get; set; }
        public string ApiKey { get; set; } = string.Empty;
    }
}
