using System;
using System.Data;

class BusinessLogic
{
    private readonly DataTier database = new DataTier();
    private readonly GuiTier gui = new GuiTier();

    public void Run()
    {
        bool _continue = true;
        Staff staff = gui.Login();

        if (database.LoginCheck(staff))
        {
            while (_continue)
            {
                int option = gui.Dashboard(staff);
                switch (option)
                {
                    case 1:
                        Console.WriteLine("Enter Resident Full Name:");
                        string fullName = Console.ReadLine();
                        Console.WriteLine("Enter Unit Number:");
                        int unit = Convert.ToInt32(Console.ReadLine());
                        DataTable history = database.GetPackageHistory(fullName, unit);
                        gui.DisplayPackageHistory(history);
                        break;

                    case 2:
                        Console.WriteLine("Enter Resident ID:");
                        int residentId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Tracking Number:");
                        int tracking = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Delivery Date (YYYY-MM-DD):");
                        DateTime date = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("Enter Status (Pending/PickedUp):");
                        string status = Console.ReadLine();
                        Console.WriteLine("Enter Postal Agency (UPS, USPS, FedEx):");
                        string agency = Console.ReadLine();

                        bool success = database.AddPackage(residentId, tracking, date, status, agency);
                        if (success)
                        {
                            Console.WriteLine("Package added successfully.");
                            if (status.ToLower() == "pending")
                            {
                                string email = database.GetResidentEmail(residentId);
                                if (!string.IsNullOrEmpty(email))
                                {
                                    EmailSender.SendEmail(
                                        senderEmail: "noreply@buffteks.org",
                                        password: "cidm4360fall2024@*",
                                        toEmail: email,
                                        subject: "Package Pickup Notification"
                                    );
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Failed to add package.");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Enter Name on Package:");
                        string unknownName = Console.ReadLine();
                        Console.WriteLine("Enter Postal Agency:");
                        string unknownAgency = Console.ReadLine();
                        Console.WriteLine("Enter Delivery Date (YYYY-MM-DD):");
                        DateTime unknownDate = Convert.ToDateTime(Console.ReadLine());

                        bool unknownAdded = database.AddUnknownPackage(unknownName, unknownAgency, unknownDate);
                        Console.WriteLine(unknownAdded ? "Unknown package added." : "Failed to add unknown package.");
                        break;

                    case 4:
                        Console.WriteLine("Enter Resident Full Name:");
                        string nameForUpdate = Console.ReadLine();
                        Console.WriteLine("Enter Package ID:");
                        int pkgId = Convert.ToInt32(Console.ReadLine());

                        bool updated = database.UpdatePackageStatus(nameForUpdate, pkgId, "Delivered");
                        Console.WriteLine(updated ? "Package status updated to Delivered." : "Failed to update package status.");
                        break;

                    case 5:
                        DataTable unknownTable = database.GetUnknownPackages();
                        gui.DisplayUnknownPackages(unknownTable);
                        break;

                    case 6:
                        _continue = false;
                        Console.WriteLine("Logged out. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Login Failed. Goodbye.");
        }
    }
}
