using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanTeenManagement.CO
{
    class staticVarClass
    {
        #region time
        public static int time_autoOff = 5 * 1000;

        public static int time_autoOffLogIn = 1 * 1000;
        #endregion

        #region Link file server IP.
        public static string linkFile_serverIP = "Server.txt";
        public static string linkFile_account = "Account.txt";
        #endregion

        #region server.
        public static string server_Host = "127.0.0.1";

        public static string server_serverDirectory = @"\\" + server_Host + @"\CanteenManagement\";

        public static string server_connectSQLServer = server_Host;

        public static string sever_connectSQLServerCatalog = "QLCanTin";

        public static string server_connectSQLServerUser = "THIENLY";

        public static string server_connectSQLServerPass = "HOANGHANH2703";
        #endregion

        #region food type.
        public static int foodType_one= 1;
        public static int foodType_two = 2;
        public static int foodType_three = 3;
        #endregion

        #region account.
        public static string account_userName = string.Empty;

        public static string account_password = string.Empty;
        #endregion

        #region ftp.
        public static string ftp_userName = "CanteenManagement";

        public static string ftp_password = "123456aA123456";

        public static string ftp_Server = @"ftp://" + server_Host + "/";
        #endregion

        #region Role
        public static string role_admin = "Admin";
        public static string role_member = "Member";
        public static string role_staff = "Staff";
        #endregion

        #region status.
        public static string status_waiting = "Đang chờ";
        public static string status_notComplete = "Hết món";
        public static string status_done = "Xong";
        #endregion
    
        #region email.
        public static int email_portEmail = 587;

        public static string email_hostEmail = "smtp.gmail.com";
        #endregion

        #region gmail.
        public static string gmail_user = "ISE.NNDK.13@gmail.com";
        public static string gmail_password = "123456aA123456";
        #endregion

        // Hàm lấy host.
        public static string getHost()
        {
            string str_FilePathLocal = staticVarClass.linkFile_serverIP;
            string str_HostLocal = string.Empty;

            if (System.IO.File.Exists(str_FilePathLocal))
            {
                // FileStream fs = new FileStream(str_FilePathLocal, FileMode.Open);

                StreamReader strRd = new StreamReader(str_FilePathLocal);

                str_HostLocal = strRd.ReadLine();

                strRd.Close();
            }
            else
            {
                return null;
            }

            return str_HostLocal;
        }
    }
}
