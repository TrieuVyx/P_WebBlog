using System.Text;
using System.Text.RegularExpressions;
using blog.Models;
using Microsoft.EntityFrameworkCore;
namespace blog.Helpers
{
    public static class Utilities
    {
        public static async Task<string> UpLoadFile(IFormFile file, string sDirectory, string newname = null)
        {
            try
            {
                if (newname == null) newname = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory, newname);
                string path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory);
                if (!Directory.Exists(path2))
                {
                    Directory.CreateDirectory(path2);
                }
                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
                var fileExt = Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt.ToLower()))
                {
                    return null;
                }
                else
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }return newname;
                }
              
            }
            catch {
                return null;
            }
        }
        public static string REG(string url)
        {
            url = url.ToLower();
            url = Regex.Replace(url, @"[áàạảãâấầậẩẫăắằặẳẵ]", "a");
            url = Regex.Replace(url, @"[éèẹẻẽêếềểễ]", "e");
            url = Regex.Replace(url, @"[óòọỏõốồộổỗ]", "o");
            url = Regex.Replace(url, @"[úùụủũứừựửữ]", "u");
            url = Regex.Replace(url, @"[íìỉĩị]", "i");
            url = Regex.Replace(url, @"[ýỳỵỷỹ]", "y");
            url = Regex.Replace(url, @"[đ]", "d");

            // chỉ cho phép nhận : [0-9a-z-\s]
            url = Regex.Replace(url.Trim(), @"[^0-9a-z-\s]", "").Trim();
            // xử lý nhiều hơn 1 khoảng trắng 
            url = Regex.Replace(url.Trim(), @"\s+", "-");
            // thay khoảng trắng bằng -
            url = Regex.Replace(url, @"\s", "-");
            while (true)
            {
                if(url.IndexOf("--") != -1)
                {
                    url = url.Replace("--", "-");
                }
                else
                {
                    break;
                }
            }
            return url;

        }
  
        public static string GetRanDomKey(int length = 5)
        {
            string pattern = "@123456789zxcvbnmasdfghjklqwertyuiop[]{}:~!@#$%^&*()+";
            Random rd = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0,pattern.Length)]);

            }return sb.ToString();
        }
    
    }
}
