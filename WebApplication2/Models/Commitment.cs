using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Commitment
{
    [Key] 
    public int IdCommitment { get; set; }
    public DateTime PaymentDeadline { get; set; }
    public float LeftToPay { get; set; }
    public int IdSubscription { get; set; }
}