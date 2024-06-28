using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Subscription
{
    [Key]
    public int IdSubscription { get; set; }
    public int IdUser { get; set; }
    public int IdService { get; set; }
}