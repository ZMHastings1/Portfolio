using System.ComponentModel.DataAnnotations;

namespace CIDM_3312_FinalProject.Models;

public class User
{
    public int UserID { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;

    public List<Ticket> Tickets { get; set; } = default!;
    
}
