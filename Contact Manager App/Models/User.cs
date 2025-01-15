using System.ComponentModel.DataAnnotations;

namespace Contact_Manager_App.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public DateTime DateOfBirth { get; set; }
    
    [Required]
    public bool IsMarried { get; set; }
    
    [Required]
    [Phone]
    public string Phone { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Salary { get; set; }
    
}