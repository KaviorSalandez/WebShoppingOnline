using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace WebShoppingOnline.Models.Common
{
    public class ThongKeTruyCap
    {
        public static string strConnect = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public static ThongKeViewModel ThongKe()
        {
            using (var connect = new SqlConnection(strConnect))
            {
                // Dùng thư viện dapper để gọi procedure
                // thực thi câu lệnh store bằng dapper
                // câu lệnh trên thực hiện một truy vấn vào cơ sở dữ liệu bằng cách gọi stored procedure "sp_ThongKe" và trả về một đối tượng ThongKeViewModel đầu tiên từ kết quả truy vấn.
                var item = connect.QueryFirstOrDefault<ThongKeViewModel>("sp_ThongKe",commandType: CommandType.StoredProcedure);
                return item;
            }
        }
    }
}