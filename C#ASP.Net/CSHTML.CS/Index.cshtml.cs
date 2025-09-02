using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CIDM_3312_FinalProject.Models;

namespace CIDM_3312_FinalProject.Pages;

    public class IndexModel : PageModel
    {
        private readonly CIDM_3312_FinalProject.Models.TicketDbContext _context;

        public IndexModel(CIDM_3312_FinalProject.Models.TicketDbContext context)
        {
            _context = context;
        }

        public List<Ticket> Ticket { get;set; } = default!;
        
        
        public List<Comment> Comments {get; set;} = default!;

        public List<Technician> Technicians {get; set;} = default!;

        public List<User> Users {get; set;} = default!;

        public async Task OnGetAsync()
    {
        Ticket = await _context.Tickets.Include(t => t.User).Include(t => t.Technician).ToListAsync();
        Technicians = await _context.Technicians.ToListAsync();
        Users       = await _context.Users.ToListAsync();
        Comments    = await _context.Comments.ToListAsync();
    }
        
    }


