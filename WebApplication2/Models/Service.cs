using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Service
{
    [Key]
    public int IdService { get; set; }
    public string Name { get; set; }
    public int IdProvider { get; set; }
}