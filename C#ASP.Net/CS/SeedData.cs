using Microsoft.EntityFrameworkCore;

namespace CIDM_3312_FinalProject.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new TicketDbContext(serviceProvider.GetRequiredService<DbContextOptions<TicketDbContext>>());

                    if (context.Tickets.Any())
            {
                return;
            }

            context.Tickets.AddRange(
                new Ticket
                {
                    Category    = "Software",
                    Description = "Unable to install Visual Studio.",
                    SubmitDate  = new DateTime(2025, 5, 10),
                    Status      = "Open",
                    Priority    = "High",
                    User = new User
                    {
                        Name       = "Alice Johnson",
                        Email      = "alice.johnson@example.com",
                        Department = "Human Resources"
                    },
                    Technician = new Technician
                    {
                        TechName = "Bob Smith"
                    },
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            CommentText = "Alice Johnson: I'm still unable to install Visual Studio after rebooting.",
                            PostedDate  = new DateTime(2025, 5, 11)
                        }
                    }
                },
                new Ticket
                {
                    Category    = "Hardware",
                    Description = "Printer not responding.",
                    SubmitDate  = new DateTime(2025, 5, 9),
                    Status      = "Open",
                    Priority    = "Medium",
                    User = new User
                    {
                        Name       = "Carlos Martinez",
                        Email      = "carlos.martinez@example.com",
                        Department = "Finance"
                    },
                    Technician = new Technician
                    {
                        TechName = "Daniela Pierce"
                    },
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            CommentText = "Daniela Pierce: Cleared paper jam, please test the printer.",
                            PostedDate  = new DateTime(2025, 5, 10)
                        }
                    }
                },
                new Ticket
                {
                    Category    = "Network",
                    Description = "Cannot connect to VPN.",
                    SubmitDate  = new DateTime(2025, 5, 8),
                    Status      = "Closed",
                    Priority    = "High",
                    User = new User
                    {
                        Name       = "Emily Davis",
                        Email      = "emily.davis@example.com",
                        Department = "Marketing"
                    },
                    Technician = new Technician
                    {
                        TechName = "Frank Nguyen"
                    }
                },
                new Ticket
                {
                    Category    = "Account",
                    Description = "Password reset required.",
                    SubmitDate  = new DateTime(2025, 5, 7),
                    Status      = "Open",
                    Priority    = "Low",
                    User = new User
                    {
                        Name       = "Grace Lee",
                        Email      = "grace.lee@example.com",
                        Department = "IT"
                    },
                    Technician = new Technician
                    {
                        TechName = "Hannah Patel"
                    },
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            CommentText = "Hannah Patel: Password reset email sent, please check your inbox.",
                            PostedDate  = new DateTime(2025, 5, 8)
                        }
                    }
                },
                new Ticket
                {
                    Category    = "Software",
                    Description = "Email client crashes intermittently.",
                    SubmitDate  = new DateTime(2025, 5, 6),
                    Status      = "Open",
                    Priority    = "Medium",
                    User = new User
                    {
                        Name       = "Ivan Rossi",
                        Email      = "ivan.rossi@example.com",
                        Department = "Sales"
                    },
                    Technician = new Technician
                    {
                        TechName = "Jack Wilson"
                    }
                }
            );

            context.SaveChanges();
    }

    
}