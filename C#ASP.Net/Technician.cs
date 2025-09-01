using System.ComponentModel.DataAnnotations;

namespace CIDM_3312_FinalProject.Models;

public class Technician
{
    public int TechnicianID { get; set; }

    [Display(Name = "Technician Name")]
    public string TechName { get; set; } = string.Empty;

    public List<Ticket> Tickets { get; set; } = default!;

}