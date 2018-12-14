using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CanTeenManagement.View;
using CanTeenManagement.ViewModel;
using System.Windows.Media.Imaging;

namespace CanTeenManagement.CO
{
    class staticFunctionClass
    {
        public static Bitmap LoadBitmap1(string path)
        {
            if (File.Exists(path))
            {
                // open file in read only mode
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                // get a binary reader for the file stream
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    // copy the content of the file into a memory stream
                    var memoryStream = new MemoryStream(reader.ReadBytes((int)stream.Length));
                    // make a new Bitmap object the owner of the MemoryStream
                    return new Bitmap(memoryStream);
                }
            }
            else
            {
                //XtraMessageBox.Show("Error loading file!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Load bitmap.
        public static BitmapImage LoadBitmap(string path)
        {
            if (File.Exists(path))
            {
                BitmapImage bitmapImg = new BitmapImage();

                bitmapImg.BeginInit();

                bitmapImg.CacheOption = BitmapCacheOption.OnLoad;

                bitmapImg.UriSource = new Uri(path);

                bitmapImg.EndInit();

                return bitmapImg;
            }

            return null;
        }

        public static bool deleteFile(string fileName)
        {
            myFTP ftp = new myFTP(staticVarClass.ftp_Server, staticVarClass.ftp_userName, staticVarClass.ftp_password);

            return ftp.delete(fileName);
        }

        public static string getFormat(string path)
        {
            var dot = ".";
            var foundPos = path.LastIndexOf(dot);
            var extension = path.Substring(foundPos + 1, path.Length - foundPos - 1);

            return dot + extension;
        }

        public static bool uploadFile(string fileName, string path)
        {
            myFTP ftp = new myFTP(staticVarClass.ftp_Server, staticVarClass.ftp_userName, staticVarClass.ftp_password);

            return ftp.upload(fileName, path);
        }

        public static string TimeAgo(string strDateTime)
        {
            DateTime dateTime = DateTime.Parse(strDateTime);
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} giây trước", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ?
                    String.Format("Khoảng {0} phút trước", timeSpan.Minutes) :
                    "Khoảng 1 phút trước";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ?
                    String.Format("Khoảng {0} giờ trước", timeSpan.Hours) :
                    "Khoảng 1 giờ trước";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ?
                    String.Format("Khoảng {0} ngày trước", timeSpan.Days) :
                    "Ngày hôm qua";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ?
                    String.Format("Khoảng {0} tháng trước", timeSpan.Days / 30) :
                    "Khoảng 1 tháng trước";
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    String.Format("Khoảng {0} năm trước", timeSpan.Days / 365) :
                    "Khoảng 1 năm trước";
            }

            return result;
        }

        public static void showStatusView(bool status, string content)
        {
            StatusView statusV = new StatusView();

            if (statusV.DataContext == null)
                return;

            var l_statusVM = statusV.DataContext as StatusViewModel;

            l_statusVM.g_b_isSuccessful = status;
            l_statusVM.g_str_text = content;

            statusV.Topmost = true;
            statusV.ShowDialog();
        }
    }
}
