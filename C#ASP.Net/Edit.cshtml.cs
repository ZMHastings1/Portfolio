using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIDM_3312_FinalProject.Models;

namespace CIDM_3312_FinalProject.Pages_Tickets
{
    public class EditModel : PageModel
    {
        private readonly CIDM_3312_FinalProject.Models.TicketDbContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(CIDM_3312_FinalProject.Models.TicketDbContext context, ILogger<EditModel> logger)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        [BindProperty]
        public Comment NewComment { get; set; } = default!;

        
        public SelectList TechnicianList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.Include(t => t.User).Include(t => t.Technician).Include(t => t.Comments).FirstOrDefaultAsync(t => t.TicketID == id);
            if (ticket == null)
            {
                return NotFound();
            }
            TechnicianList = new SelectList(_context.Technicians.ToList(), "TechnicianID", "TechName");
            Ticket = ticket;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var e in allErrors)
                {
                    _logger.LogError($"Error: {e.ErrorMessage}");
                }

                return Page();
            }

            _context.Attach(Ticket).State = EntityState.Modified;

            try
                {
                    NewComment.TicketID = Ticket.TicketID;
                    NewComment.PostedDate = DateTime.Now;
                    _context.Comments.Add(NewComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(Ticket.TicketID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            return RedirectToPage("/Tickets/Index");
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.TicketID == id);
        }
    }
}
