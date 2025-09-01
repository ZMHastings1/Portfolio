using System.ComponentModel.DataAnnotations;

namespace CIDM_3312_FinalProject.Models;

public class Comment
{
    public int CommentID {get; set;}

    public string CommentText {get; set;} = string.Empty;

    [Display(Name = "Date Posted")] 
    [DataType(DataType.Date)]
    public DateTime PostedDate {get; set;}

    public int TicketID {get; set;}
    public Ticket? Ticket {get; set;} = default!;
}