using CanTeenManagement.View;
using CanTeenManagement.ViewModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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

        public static string getFormatBefore(string path)
        {
            int l_index = path.IndexOf("_");
            int foundPos = 0;
            string dot = ".";
            string difDot = "_";

            string extension = string.Empty;

            if (l_index == -1)
            {
                foundPos = path.LastIndexOf(dot);
                extension = path.Substring(foundPos + 1, path.Length - foundPos - 1);

                return dot + extension;
            }

            foundPos = path.LastIndexOf(difDot);
            extension = path.Substring(foundPos + 1, path.Length - foundPos - 1);

            return difDot + extension;
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

        public static Image GenerateAvtarImage(String text, Font font, Color textColor, Color backColor, string filename)
        {
            //first, create a dummy bitmap just to get a graphics object  
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be  
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object  
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size  
            img = new Bitmap(300, 300);

            drawing = Graphics.FromImage(img);

            //paint the background  
            drawing.Clear(backColor);

            //create a brush for the text  
            Brush textBrush = new SolidBrush(textColor);

            //drawing.DrawString(text, font, textBrush, 0, 0);  
            drawing.DrawString(text, font, textBrush, new Rectangle(40, 80, 250, 250));

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            img.Save(staticVarClass.server_serverDirectory + filename + staticVarClass.format_JPG);

            return img;
        }

        public static List<string> ColorsCode()
        {
            List<string> list = new List<string>();
            list.Add("#EEAD0E");
            list.Add("#8bbf61");

            list.Add("#DC143C");
            list.Add("#CD6889");
            list.Add("#8B8386");
            list.Add("#800080");
            list.Add("#9932CC");
            list.Add("#009ACD");
            list.Add("#00CED1");
            list.Add("#03A89E");

            list.Add("#00C78C");
            list.Add("#00CD66");
            list.Add("#66CD00");
            list.Add("#EEB422");
            list.Add("#FF8C00");
            list.Add("#EE4000");

            list.Add("#388E8E");
            list.Add("#8E8E38");
            list.Add("#7171C6");

            return list;
        }

        public static void CreateProfilePicture(string name, string path, int size)
        {
            Font font = new Font(System.Drawing.FontFamily.GenericSerif, size, System.Drawing.FontStyle.Bold);
            System.Drawing.Color fontcolor = ColorTranslator.FromHtml("#FFF");
            Color bgcolor = ColorTranslator.FromHtml(staticVarClass.lst_Color.OrderBy(a => Guid.NewGuid()).FirstOrDefault());
            staticFunctionClass.GenerateAvtarImage(name, font, fontcolor, bgcolor, path);
        }

        public static string StringNormalization(string source)
        {
            if (source == null || source == "")
            {
                return "";
            }
            var source_String = source;
            const string Space = " ";

            var tokens = source_String.Split(new string[] { Space },
                StringSplitOptions.RemoveEmptyEntries).ToList();

            tokens[0] = tokens[0].Trim().ToLower();
            var firstChar = tokens[0].Substring(0, 1).ToUpper();
            var remaining = tokens[0].Substring(1, tokens[0].Length - 1);
            tokens[0] = firstChar + remaining;

            var builder = new StringBuilder();

            builder.Append(tokens[0]);
            builder.Append(Space);

            for (int i = 1; i < tokens.Count(); i++)
            {
                tokens[i] = tokens[i].Trim().ToLower();
                builder.Append(tokens[i]);
                builder.Append(Space);
            }

            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }

        public static string getIDFronExcel()
        {
            var package = new ExcelPackage(new FileInfo("Barcode_Test.xlsx"));
            ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
            int i = workSheet.Dimension.End.Row;
            int j = workSheet.Dimension.End.Column;


            string temp = workSheet.Cells[i, j].Value.ToString();

            if (staticVarClass.ID_currCustomer == temp)
                return string.Empty;

            return temp;
        }
    }
}
