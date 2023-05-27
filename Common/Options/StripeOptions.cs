namespace Common.Options;

public class StripeOptions
{
    public string PublishableKey { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public string WebhookSecret { get; set; } = null!;
    public string MonthlyPriceId { get; set; } = null!;
    public string YearlyPriceId { get; set; } = null!;
    public string SuccessUrl { get; set; } = null!;
    public string CancelUrl { get; set; } = null!;
}