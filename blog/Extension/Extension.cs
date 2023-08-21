using System.Text.RegularExpressions;

namespace blog.Extension
{
    public static class Extension
    {
        public static string ToVnd(this double donGia)
        {
            return donGia.ToString("#,##0") + " đ"; // 5.000đ

        }
        public static string ToUrlFriendly(this string url)
        {
            var result = url.ToLower().Trim();
            result = Regex.Replace(result, "áàạảãâấầậẩẫăắằặẳẵ","a");
            result = Regex.Replace(result, "éèẹẻẽêếềểễ", "e");
            result = Regex.Replace(result, "óòọỏõốồộổỗ", "o");
            result = Regex.Replace(result, "úùụủũứừựửữ", "u");
            result = Regex.Replace(result, "íìỉĩị", "i");
            result = Regex.Replace(result, "ýỳỵỷỹ", "y");
            result = Regex.Replace(result, "đ", "d");
            result = Regex.Replace(result, "[^a-z0-9-]", "");
            result = Regex.Replace(result, "(-)+", "-");

            // chào bạn  --> chao-ban
            return result;
        }
    }
}
