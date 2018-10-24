using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MyService
{
    /// <summary>
    /// Summary description for UserService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserService : System.Web.Services.WebService
    {

        static string connectString = "TetrisMVC.Properties.Settings.DataTetrisConnectionString";

        [WebMethod]
        public bool checkTV(string username)
        {
            // Tạo kết nối
            string _str = ConfigurationManager.ConnectionStrings[connectString].ConnectionString;
            SqlConnection connect = new SqlConnection(_str);

            // Tạo câu lệnh
            String _query = "SELECT 1 FROM ThanhVien WHERE username ='" + username + "'";
            SqlCommand command = new SqlCommand(_query, connect);
            connect.Open();
            try
            {
                int KetQua = (int)command.ExecuteScalar();
                if (KetQua == 1)
                    return false;
                return true;
            }
            catch (NullReferenceException ex)
            {
                return true;
            }
        }

        [WebMethod]
        public bool login(string username, string password)
        {
            // Tạo kết nối
            string _str = ConfigurationManager.ConnectionStrings[connectString].ConnectionString;
            SqlConnection connect = new SqlConnection(_str);

            // Tạo câu lệnh
            String _query = "SELECT 1 FROM ThanhVien WHERE username ='" + username + "' AND password ='" + password + "'";
            SqlCommand command = new SqlCommand(_query, connect);
            connect.Open();
            try
            {
                int KetQua = (int)command.ExecuteScalar();
                if (KetQua == 1)
                    return false;
                return true;
            }
            catch (NullReferenceException ex)
            {
                return true;
            }
        }

        [WebMethod]
        public User setSignUp(string username, string password, string fullname, int score)
        {
            User user = new User();
            user.setUsername(username);
            user.setPassword(password);
            user.setFullname(fullname);
            user.setScore(score);
            return user;
        }

        [WebMethod]
        public string getfullname(string username, string password)
        {
            // Tạo kết nối
            string _str = ConfigurationManager.ConnectionStrings[connectString].ConnectionString;
            SqlConnection connect = new SqlConnection(_str);

            // Tạo câu lệnh
            String _query = "SELECT fullname FROM ThanhVien WHERE username ='" + username + "' AND password ='" + password + "'";
            SqlCommand command = new SqlCommand(_query, connect);
            connect.Open();

            string KetQua = (string)command.ExecuteScalar();
            return KetQua;
        }

        [WebMethod]
        public int getid(string username, string password)
        {
            // Tạo kết nối
            string _str = ConfigurationManager.ConnectionStrings[connectString].ConnectionString;
            SqlConnection connect = new SqlConnection(_str);

            // Tạo câu lệnh
            String _query = "SELECT id FROM ThanhVien WHERE username ='" + username + "' AND password ='" + password + "'";
            SqlCommand command = new SqlCommand(_query, connect);
            connect.Open();

            int KetQua = (int)command.ExecuteScalar();
            return KetQua;
        }

        [WebMethod]
        public int getScore(int id)
        {
            // Tạo kết nối
            string _str = ConfigurationManager.ConnectionStrings[connectString].ConnectionString;
            SqlConnection connect = new SqlConnection(_str);
            // Tạo câu lệnh
            String _query = "SELECT score FROM ThanhVien WHERE id ='" + id + "'";
            SqlCommand command = new SqlCommand(_query, connect);
            connect.Open();
            int KetQua = (int)command.ExecuteScalar();
            return KetQua;
        }

        [WebMethod]
        public bool insertTV(string username, string password, string fullname, int score)
        {
            // Tạo kết nối
            string _str = ConfigurationManager.ConnectionStrings[connectString].ConnectionString;
            SqlConnection connect = new SqlConnection(_str);

            // Tạo câu lệnh
            String _query = "INSERT INTO dbo.ThanhVien VALUES (@username, @password,@score, @fullname)";
            SqlCommand command = new SqlCommand(_query, connect);

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@score", score);
            command.Parameters.AddWithValue("@fullname", fullname);
            connect.Open();

            int KetQuaTruyVan = command.ExecuteNonQuery();
            if (KetQuaTruyVan <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        [WebMethod]
        public bool updateScore(int id, int score)
        {
            string _str = ConfigurationManager.ConnectionStrings[connectString].ConnectionString;
            SqlConnection connect = new SqlConnection(_str);

            // Tạo câu lệnh
            String _query = "UPDATE ThanhVien SET score='" + score.ToString() + "' WHERE id='" + id.ToString() + "'";
            SqlCommand command = new SqlCommand(_query, connect);
            connect.Open();
            int KetQuaTruyVan = command.ExecuteNonQuery();
            if (KetQuaTruyVan <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [WebMethod]
        public DataTable SetDataTable(DataTable dataTable)
        {
            string _str = ConfigurationManager.ConnectionStrings["TetrisMVC.Properties.Settings.DataTetrisConnectionString"].ConnectionString;
            SqlConnection connect = new SqlConnection(_str);
            String _query = "SELECT fullname,score FROM ThanhVien ORDER BY score DESC";
            SqlCommand command = new SqlCommand(_query, connect);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            sda.Fill(dataTable);
            return dataTable;
        }
    }
}
