using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class User
{
    [Key]
    public int IdUser { get; set; }
    public string FisrstName { get; set; }
    public string LastName { get; set; }
    
}