namespace AfricanShopLviv.BLL.Logger
{
    using System.IO;

    public class Logger
    {
        public static string Write(string path, string message, bool isReWrite = true)
        {
            string pth = string.Empty, res = string.Empty;
            pth = path.Contains(".txt") ? path : path + ".txt";
            using (var sw = new StreamWriter(pth, isReWrite))
            {
                sw.WriteLine(System.DateTime.Now + " => " + message);
                res = "Success!";
            }
            return res;
        }
    }
}
