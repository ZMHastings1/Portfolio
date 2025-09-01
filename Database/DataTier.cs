using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
class DataTier{
    public string connStr = "server=34.69.59.37;user=zmhastings1;database=zmhastings1;port=8080;password=zmhastings1";

    // perform login check using Stored Procedure "LoginCount" in Database based on given user' studentID and Password
    public bool LoginCheck(Staff staff)
{
    MySqlConnection conn = new MySqlConnection(connStr);
    try
    {
        conn.Open();
        MySqlCommand cmd = new MySqlCommand("LoginCount", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@inputstaffUsername", staff.staffUsername);
        cmd.Parameters.AddWithValue("@inputstaffPassword", staff.staffPassword);
        cmd.Parameters.Add("@staffCount", MySqlDbType.Int32).Direction = ParameterDirection.Output;
        MySqlDataReader rdr = cmd.ExecuteReader();
        int returnCount = (int)cmd.Parameters["@staffCount"].Value;
        rdr.Close();
        conn.Close();
        return returnCount == 1;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        conn.Close();
        return false;
    }
}

public bool AddPackage(int residentId, int trackingNumber, DateTime deliveryDate, string status, string agency)
{
    MySqlConnection conn = new MySqlConnection(connStr);
    try
    {
        conn.Open();
        string sql = "INSERT INTO Package (id, trackingNumber, deliveryDate, packageStatus, postalAgency) VALUES (@id, @trackingNumber, @deliveryDate, @packageStatus, @postalAgency)";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", residentId);
        cmd.Parameters.AddWithValue("@trackingNumber", trackingNumber);
        cmd.Parameters.AddWithValue("@deliveryDate", deliveryDate);
        cmd.Parameters.AddWithValue("@packageStatus", status);
        cmd.Parameters.AddWithValue("@postalAgency", agency);
        int result = cmd.ExecuteNonQuery();
        conn.Close();
        return result > 0;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        conn.Close();
        return false;
    }
}

public bool AddUnknownPackage(string name, string agency, DateTime date)
{
    MySqlConnection conn = new MySqlConnection(connStr);
    try
    {
        conn.Open();
        string sql = "INSERT INTO Unknown (UnknownName, UnknownPostalAgency, UnknownDeliveryDate) VALUES (@name, @agency, @date)";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@agency", agency);
        cmd.Parameters.AddWithValue("@date", date);
        int result = cmd.ExecuteNonQuery();
        conn.Close();
        return result > 0;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        conn.Close();
        return false;
    }
}

public string GetResidentEmail(int residentId)
{
    MySqlConnection conn = new MySqlConnection(connStr);
    try
    {
        conn.Open();
        string sql = "SELECT email FROM Residents WHERE id = @id";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", residentId);
        object result = cmd.ExecuteScalar();
        conn.Close();
        return result?.ToString() ?? "";
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        conn.Close();
        return "";
    }
}

public DataTable GetPackageHistory(string fullName, int unitNumber)
{
    MySqlConnection conn = new MySqlConnection(connStr);
    DataTable dt = new DataTable();
    try
    {
        conn.Open();
        MySqlCommand cmd = new MySqlCommand("ViewPackage", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@inputunit_number", unitNumber);
        cmd.Parameters.AddWithValue("@inputfull_name", fullName);
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        da.Fill(dt);
        conn.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
        conn.Close();
    }
    return dt;
}

public DataTable GetUnknownPackages()
{
    MySqlConnection conn = new MySqlConnection(connStr);
    DataTable dt = new DataTable();
    try
    {
        conn.Open();
        string sql = "SELECT * FROM UnknownPackage";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        da.Fill(dt);
        conn.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
        conn.Close();
    }
    return dt;
}

public bool UpdatePackageStatus(string fullName, int packageId, string newStatus)
{
    MySqlConnection conn = new MySqlConnection(connStr);
    try
    {
        conn.Open();
        string sql = @"UPDATE Package 
                      JOIN Residents ON Package.id = Residents.id
                      SET Package.packageStatus = @newStatus
                      WHERE Residents.full_name = @fullName AND Package.PackageID = @packageId";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@newStatus", newStatus);
        cmd.Parameters.AddWithValue("@fullName", fullName);
        cmd.Parameters.AddWithValue("@packageId", packageId);
        int result = cmd.ExecuteNonQuery();
        conn.Close();
        return result > 0;
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
        conn.Close();
        return false;
    }
}


}