using System.Runtime.InteropServices.JavaScript;

namespace INKIPER.Dtos;

public class DepositDto
{
    public record AccountRecord(string Uuid, string Name);
    public AccountRecord Account { get; set; }
    public string AmountDeposited { get; set; }
    
    public string CurrentAccountBalance { get; set; }
    public DateTime? DepositedDate { get; set; }
    
    public string Description { get; set; }
}