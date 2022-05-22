using System;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt32(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }

            if (str == "null")
            {
                return 0;
            }

            return Convert.ToInt32(str);
        }
        public static bool StringIsNullOrEmpty(this string val)
        {
            return string.IsNullOrEmpty(val);
        }
        public static bool StringNotNullOrEmpty(this string val)
        {
            return !string.IsNullOrEmpty(val);
        }
        public static string GetFileContentType(this string contentType)
        {
            return contentType == "video/quicktime" ? "video/mp4" : contentType;
        }
        public static DateTime UnixTimestampToDateTime(double timeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timeStamp).ToLocalTime();
        }
    }
}
