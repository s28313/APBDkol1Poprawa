using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Provider
{
    [Key]
    public int IdProvider { get; set; }
    public string Name { get; set; }
}