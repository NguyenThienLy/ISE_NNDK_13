using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CanTeenManagement.CO
{
    class staticVarClass
    {
        #region format.
        public static string format_PNG = ".png";
        public static string format_JPG = ".jpg";
        public static string format_GIF = ".gif";
        #endregion

        #region list.
        public static List<string> lst_Color = staticFunctionClass.ColorsCode();
        #endregion

        #region linkImage.
        public static string linkImg_empty = @"\\127.0.0.1\CanteenManagement\empty.default.png";
        #endregion

        #region gender.
        public static string gender_male = "Nam";
        public static string gender_feMale = "Nữ";
        public static string gender_different = "Khác";
        #endregion

        #region image source.
        public static ImageSource imgSrc_empty = staticFunctionClass.LoadBitmap(linkImg_empty);
        #endregion

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
        public static int foodType_one = 1;
        public static int foodType_two = 2;
        public static int foodType_three = 3;
        #endregion

        #region food type string.
        public static string foodTypeStr_one = "Món cơm";
        public static string foodTypeStr_two = "Món nước";
        public static string foodTypeStr_three = "Thức ăn có sẵn";
        #endregion

        #region mode.
        public static string mode_addCash = "Nạp tiền";
        public static string mode_subCash = "Rút tiền";
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
        public static string status_still = "Còn";
        public static string status_waiting = "Đang chờ";
        public static string status_soldOut = "Hết món";
        public static string status_done = "Xong";
        public static string status_skip = "Bỏ qua";
        #endregion

        #region email.
        public static int email_portEmail = 587;

        public static string email_hostEmail = "smtp.gmail.com";
        #endregion

        #region gmail.
        public static string gmail_user = "ISE.NNDK.13@gmail.com";
        public static string gmail_password = "123456aA123456";
        #endregion

        #region visibility.
        public static string visibility_hidden = "Hidden";
        public static string visibility_visible = "Visible";
        #endregion

        //#region screen.
        //public static int screen_width = (int)System.Windows.SystemParameters.WorkArea.Width;
        //public static int screen_height = (int)System.Windows.SystemParameters.WorkArea.Height;
        //#endregion

        #region screen.
        public static int screen_Top = 0;
        public static int screen_Left = 0;
        #endregion

        #region quantity
        public static int quantity_statusView = 2;
        #endregion   
    }
}
