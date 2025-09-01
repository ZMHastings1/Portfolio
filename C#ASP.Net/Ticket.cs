using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CIDM_3312_FinalProject.Models;

public class Ticket
{
    public int TicketID {get; set;}

    public string Category {get; set;} = string.Empty;

    public string Description {get; set;} = string.Empty;

    [Display(Name = "Ticket Submission Date")] 
    [DataType(DataType.Date)]
    public DateTime SubmitDate {get; set;}

    public string Status {get; set;} = string.Empty;

    public string Priority {get; set;} = string.Empty;

    public List<Comment>? Comments { get; set; } = default!;

    public int UserID { get; set; }
    public User? User   { get; set; } = default!;

    public int TechnicianID { get; set; }
    public Technician? Technician { get; set; } = default!;
}