using System.Data;
using MySql.Data.MySqlClient;
class GuiTier{
    Staff staff = new Staff();
    DataTier database = new DataTier();

    // print login page
    public Staff Login(){
        Console.WriteLine("------Welcome to the Mailroom Management Application------");
        Console.WriteLine("Please username: ");
        staff.staffUsername = Console.ReadLine();
        Console.WriteLine("Please input password: ");
        staff.staffPassword = Console.ReadLine();
        return staff;
    }

    // print Dashboard after user logs in successfully
    public int Dashboard(Staff staff){
        DateTime localDate = DateTime.Now;
        Console.WriteLine("---------------Dashboard-------------------");
        Console.WriteLine($"Hello: {staff.staffUsername}; Date/Time: {localDate.ToString()}");
        Console.WriteLine("Please select an option to continue:");
        Console.WriteLine("1. View Package History");
        Console.WriteLine("2. Add a Package");
        Console.WriteLine("3. Mark an Unknown Package");
        Console.WriteLine("4. Update Package Status to Delivered");
        Console.WriteLine("5. View Unknown Package Table");
        Console.WriteLine("6. Log Out");
        int option = Convert.ToInt16(Console.ReadLine());
        return option;
    }

    public void DisplayPackageHistory(DataTable tablePackages){
        Console.WriteLine("---------------Package History-------------------");
        foreach(DataRow row in tablePackages.Rows){
            Console.WriteLine($"PackageID: {row["PackageID"]} \t TrackingNumber: {row["trackingNumber"]} \t DeliveryDate: {Convert.ToDateTime(row["deliveryDate"]).ToShortDateString()} \t Status: {row["packageStatus"]} \t Carrier: {row["postalAgency"]}");
        }
    }

    public void DisplayUnknownPackages(DataTable tableUnknown){
        Console.WriteLine("---------------Unknown Packages-------------------");
        foreach(DataRow row in tableUnknown.Rows){
            Console.WriteLine($"UnknownID: {row["UnknownID"]} \t Name: {row["UnknownName"]} \t Carrier: {row["UnknownPostalAgency"]} \t DeliveryDate: {Convert.ToDateTime(row["UnknownDeliveryDate"]).ToShortDateString()}");
        }
    }
}
