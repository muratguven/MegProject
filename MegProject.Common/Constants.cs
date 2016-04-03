using System.IO;
using System.Web;

namespace MegProject.Common
{
    public static class Constants
    {
        #region FIX
        public struct Culture
        {
            public const string TR = "tr-TR";
            public const string ENG = "en-EN";
        }

        public struct Currency
        {
            public const string TRY = "TRY";
            public const string USD = "USD";
            public const string TL = "TL";
        }


        public struct Format
        {
            public string HTMLSpace
            {
                get
                {
                    StringWriter sWriter = new StringWriter();
                    HttpContext.Current.Server.HtmlDecode("&nbsp;", sWriter);
                    return sWriter.ToString();
                }
            }
            public const string DoubleFormatString = "{0:#,0.000000}";
            public const string CurrencyFormatString = "{0:#,0.00}";

        }
        #endregion


        #region HTML Message for adminLTE or Bootstrap
        public struct MessageTypes
        {
            public const string Success = "success";
            public const string Danger = "danger";
            public const string Warning = "warning";
            public const string Info = "info";
        }


        public struct Messages
        {
            public const string Success = "İşlem başarıyla tamamlandı.";
            public const string Error = "İşlem sırasında hata oluştu!";
            public const string Empty = "Veriler boş gönderilemez!";
        }
        #endregion


        #region STATUS

        public enum OrderStatus
        {
            Deleted = -1,
            Active = 0,
            Finished = 1

        }

        public enum Status
        {
            Deleted = -1,
            Active = 0,
            Passive = 1
        }

        #endregion
    }
}