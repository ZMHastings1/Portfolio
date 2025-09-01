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
    public class IndexModel : PageModel
    {
        private readonly CIDM_3312_FinalProject.Models.TicketDbContext _context;

        public IndexModel(CIDM_3312_FinalProject.Models.TicketDbContext context)
        {
            _context = context;
        }

        public IList<Ticket> Ticket { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int PageNum {get; set;} = 1;
        public int PageSize {get; set;} = 5;
        public int TotalPages {get; set;}

        [BindProperty(SupportsGet = true)]
        public string CurrentSort { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string CurrentSearch {get; set;} = string.Empty;

        public async Task OnGetAsync()
        {
            var query = _context.Tickets.Include(t => t.User).Include(t => t.Technician).Select(s => s);

            if (!string.IsNullOrEmpty(CurrentSearch))
            {
                query = query.Where(t => t.User.Name.ToUpper().Contains(CurrentSearch.ToUpper()) || t.Technician.TechName.ToUpper().Contains(CurrentSearch.ToUpper()));

            }

            switch (CurrentSort)
            {
                case "SbDate_asc":
                    query = query.OrderBy(t => t.SubmitDate);
                    break;
                case "SbDate_desc":
                    query = query.OrderByDescending(t => t.SubmitDate);
                    break;
                case "Status_asc":
                    query = query.OrderBy(t => t.Status);
                    break;
                case "Status_desc":
                    query = query.OrderByDescending(t => t.Status);
                    break;
            }

            TotalPages = (int)Math.Ceiling(query.Count() / (double)PageSize);

            Ticket = await query.Skip((PageNum-1)*PageSize).Take(PageSize).ToListAsync();
            
        }
    }
}
