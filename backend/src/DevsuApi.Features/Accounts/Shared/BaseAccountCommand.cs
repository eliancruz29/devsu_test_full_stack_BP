namespace DevsuApi.Features.Accounts.Shared;

public class BaseAccountCommand
{
    public string AccountNumber { get; set; } = string.Empty;
    
    public int OpeningBalance { get; set; }
}
