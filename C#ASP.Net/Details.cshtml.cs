using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CIDM_3312_FinalProject.Models;

namespace CIDM_3312_FinalProject.Pages_Tickets
{
    public class DetailsModel : PageModel
    {
        private readonly CIDM_3312_FinalProject.Models.TicketDbContext _context;

        public DetailsModel(CIDM_3312_FinalProject.Models.TicketDbContext context)
        {
            _context = context;
        }

        public Ticket Ticket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.Include(t => t.User).Include(t => t.Technician).Include(t => t.Comments).FirstOrDefaultAsync(t => t.TicketID == id);   

            if (ticket is not null)
            {
                Ticket = ticket;

                return Page();
            }

            return NotFound();
        }
    }
}
