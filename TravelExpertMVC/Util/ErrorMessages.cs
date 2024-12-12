namespace TravelExpertMVC.Util;

public static class ErrorMessages
{
    // general
    public const string GENERIC = "An error occurred while processing your request. Please try again later.";

    // wallet
    public const string WALLET_NOT_FOUND = "Can't find wallet. Please contact support.";
    public const string INSUFFICIENT_FUND =
        "Insufficient funds in your wallet. Please add more money to your wallet or use a different payment method.";

    // email
    public const string EMAIL_SENDING_FAILED = "Failed to send email. Please try again later.";
}
