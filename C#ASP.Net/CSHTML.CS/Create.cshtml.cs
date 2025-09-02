using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CIDM_3312_FinalProject.Models;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;

namespace CIDM_3312_FinalProject.Pages_Tickets
{
    public class CreateModel : PageModel
    {
        private readonly CIDM_3312_FinalProject.Models.TicketDbContext _context;

        public CreateModel(CIDM_3312_FinalProject.Models.TicketDbContext context)
        {
            _context = context;
        }
        
        public SelectList UserList {get; set;} = default!;
        public SelectList TechnicianList {get; set;} = default!;


        public IActionResult OnGet()
        {

            UserList = new SelectList(_context.Users.ToList(), "UserID", "Name");
            TechnicianList = new SelectList(_context.Technicians.ToList(), "TechnicianID", "TechName");

            return Page();
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var err in errors)
                    {
                        Console.WriteLine($"Binding error on {key}: {err.ErrorMessage}");
                    }
                }
                return Page();
            }

            _context.Tickets.Add(Ticket);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Tickets/Index");
        }
    }
}
