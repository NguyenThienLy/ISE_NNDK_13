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
        public static int timeAutoOff = 5 * 1000;

        public static int timeAutoOffFormLogIn = 1 * 1000;
        #endregion

        #region Link file server IP.
        public static string linkFile_ServerIP = "Server.txt";
        public static string linkFile_Account = "Account.txt";
        #endregion

        #region server.
        public static string server_Host = getHost();

        public static string server_ServerDirectory = @"\\" + server_Host + @"\Server\";

        public static string server_ConnectSQLServer = server_Host;

        public static string sever_ConnectSQLServerCatalog = "QLCanTin";

        public static string server_ConnectSQLServerUser = "THIENLY";

        public static string server_ConnectSQLServerPass = "HOANGHANH2703";
        #endregion

        #region account.
        public static string account_Username = string.Empty;

        public static string account_Password = string.Empty;
        #endregion

        #region ftp.
        public static string ftp_Username = "ly";

        public static string ftp_Password = "1";

        public static string ftp_Server = @"ftp://" + server_Host + "/";
        #endregion

        #region Role
        public static string role_Admin = "Admin";
        public static string role_Member = "Member";
        public static string role_Staff = "Staff";
        #endregion

        #region status.
        public static string status_Waiting = "Đang chờ";
        public static string status_NotComplete = "Hết món";
        public static string status_Overdue = "Xong";
        public static string status_Active = "Đang hoạt đông";
        public static string status_NotActive = "Không hoạt động";
        #endregion

        #region type.
        public static string type_Normal = "Normal";
        public static string type_AdminApproval = "Admin approval";
        #endregion

        #region POSMProject.
        public static string POSM_POSMProject = "Yes";

        public static string POSM_NotPOSMProject = "No";
        #endregion

        #region other.
        public static int maxStage = 20;

        public static int numberOfDept = 8;

        public static string completeProgress = "100";
        #endregion

        #region email.
        public static int email_PortEmail = 587;

        public static string email_HostEmail = "smtp.gmail.com";
        #endregion

        #region gmail.
        public static string gmail_User = "ISE.NNDK.13@gmail.com";
        public static string gmail_Password = "123456aA123456";
        #endregion

        // Hàm lấy host.
        public static string getHost()
        {
            string str_FilePathLocal = staticVarClass.linkFile_ServerIP;
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
