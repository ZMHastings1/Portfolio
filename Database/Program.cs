using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
class Program
{
    static void Main(string[] args)
    {
        BusinessLogic app = new BusinessLogic();
        app.Run();
    }
}
