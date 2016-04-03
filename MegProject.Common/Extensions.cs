using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;


namespace MegProject.Common
{
  
    public static class Extensions
    {
        #region [ CONSTATNS ]

        public static CultureInfo _en = new CultureInfo("en-GB");
        public static CultureInfo _tr = new CultureInfo("tr-TR");
        public static readonly CultureInfo Tr = new CultureInfo("tr-TR");
        private const string RegexPatternURL = @"([\s-,.(:]+)((?#Protocol)(?:(?:ht|f)tp(?:s?)\:\/\/|~\/|\/)(?#Username:Password)(?:\w+:\w+@)?(?#Subdomains)(?:(?:[-\w]+\.)+(?#TopLevel Domains)(?:com|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum|travel|[a-z]{2}))(?#Port)(?::[\d]{1,5})?(?#Directories)(?:(?:(?:\/(?:[-\w~!$+|.,=]|%[a-fA-F\d]{2})+)+|\/)+|\?|#)?(?#Query)(?:(?:\?(?:[-\w~!$+|.,*:#/]|%[a-fA-F\d{2}])+=?(?:[-\w~!$+|.,*:=]|%[a-fA-F\d]{2})*)(?:&(?:[-\w~!$+|.,*:]|%[a-fA-F\d{2}])+=?(?:[-\w~!$+|.,*:=]|%[a-fA-F\d]{2})*)*)*(?#Anchor)(?:#(?:[-\w~!$+|.,*:=&]|%[a-fA-F\d]{2})*)?)";
        private const string RegexPatternEmail = @"([\s-,.(:]+)([a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)";

        #endregion

        #region [ EXTENSION METHODS ]

        public static string ConvertToXML(this object emp)
        {
            MemoryStream stream = null;
            TextWriter writer = null;
            try
            {
                stream = new MemoryStream(); // read xml in memory

                writer = new StreamWriter(stream, Encoding.Unicode);
                // get serialise object

                XmlSerializer serializer = new XmlSerializer(emp.GetType());
                serializer.Serialize(writer, emp); // read object

                int count = (int)stream.Length; // saves object in memory stream

                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                // copy stream contents in byte array

                stream.Read(arr, 0, count);
                UnicodeEncoding utf = new UnicodeEncoding(); // convert byte array to string

                return utf.GetString(arr).Trim();
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (stream != null) stream.Close();
                if (writer != null) writer.Close();
            }
        }

        public static string Serialize(this object emp)
        {
            XmlSerializer ser = new XmlSerializer(emp.GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            ser.Serialize(writer, emp);

            return sb.ToString();
        }

        public static T Deserialize<T>(this string xml)
        {
            XmlDocument doc = new XmlDocument();
            XmlSerializer ser = new XmlSerializer(typeof(T));
            doc.LoadXml(xml);
            XmlNodeReader reader = new XmlNodeReader(doc.DocumentElement);
            object obj = ser.Deserialize(reader);
            return (T)obj;
        }


        public static string Encrypt(this string str)
        {
            return Cryptor.EncryptString(str);
        }

        public static string Decrypt(this string str)
        {
            return Cryptor.DecryptString(str);
        }

        public static bool Contains(this string[] data, string value)
        {
            return data.Any(i => i == value);
        }

        public static bool Contains(this int[] data, int value)
        {
            return data.Any(i => i == value);
        }

        public static string CString(this object str)
        {
            string retval = string.Empty;
            try
            {
                retval = Convert.ToString(str, _tr);
            }
            catch
            {

            }
            return retval;
        }

        public static string ToOtsDate(this object str)
        {
            string retval = string.Empty;
            try
            {
                string[] arr = str.CString().Split('-');
                retval = arr[2] + arr[1] + arr[0];
            }
            catch
            {

            }
            return retval;
        }

        public static string AddZeroForDigit(this int digit)
        {
            string retval = digit.CString();
            try
            {
                retval = digit.ToString("#,0");
            }
            catch
            {

            }
            return retval;
        }

        public static string AddZeroesBegining(this string Text, int Lenght)
        {

            string Default = "00000000000000000000";//20 Zeros 
            string MixedText = Default + Text;

            return MixedText.Substring(MixedText.Length - Lenght);
        }
        public static char CChar(this object str)
        {
            char retval = ' ';
            try
            {
                retval = Convert.ToChar(str, _tr);
            }
            catch
            {

            }
            return retval;
        }

        public static string CStringDisplay(this object str)
        {
            string retval = string.Empty;
            try
            {
                if (str is decimal)
                {
                    return CStringDecimal((decimal)str);
                }
                else if (str is double)
                {
                    return CStringDouble((double)str);
                }
                else if (str is int)
                {
                    return CStringInt((int)str);
                }
                else
                {

                    retval = Convert.ToString(str, Tr);
                }
            }
            catch
            {

            }
            return retval;
        }


        public static string CStringDouble(this double val)
        {
            string retval = string.Empty;
            try
            {
                retval = string.Format(Constants.Format.DoubleFormatString, val);
            }
            catch
            {

            }
            return retval;
        }

        public static string CStringDecimal(this decimal val)
        {
            string retval = string.Empty;
            try
            {
                //retval = val.ToString("N2", _tr);
                retval = string.Format(Constants.Format.CurrencyFormatString, val);
                // retval = string.Format(new CultureInfo(Constants.Culture.TR), Constants.Format.CurrencyFormatString, val);
            }
            catch
            {

            }
            return retval;

        }

        public static string GetDateForSAP(this DateTime tmpDate)
        {
            return tmpDate.Year.ToString("0000.") + tmpDate.Month.ToString("00.") + tmpDate.Day.ToString("00.");
        }

        public static string FormatDateForSAPWS(this string datestr)
        {
            datestr = datestr.Substring(0, 4) + datestr.Substring(4, 2) + datestr.Substring(6, 2);
            return datestr;
        }

        public static string GetNumberFitted(this decimal data, int decimalPlace, int toFit)
        {
            string retval = string.Empty;
            string format = string.Empty;
            for (int i = 0; i < toFit; i++)
            {
                format += "0";
            }
            if (decimalPlace > 0)
            {
                format += ".";
                for (int i = 0; i < decimalPlace; i++)
                {
                    format += "0";
                }
            }
            retval = data.ToString(format);
            retval = retval.Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, ".");
            return retval;
        }

        public static string GetNumberFitted_AKB(this decimal data, int decimalPlace, int toFit)
        {
            string retval = string.Empty;
            string format = string.Empty;
            for (int i = 0; i < toFit; i++)
            {
                format += "0";
            }
            if (decimalPlace > 0)
            {
                format += ".";
                for (int i = 0; i < decimalPlace; i++)
                {
                    format += "0";
                }
            }
            retval = data.ToString(format);
            retval = retval.Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, String.Empty);


            return retval;
        }

        public static string GetNumberFitted(this int data, int toFit)
        {
            string retval = string.Empty;
            string format = string.Empty;
            for (int i = 0; i < toFit; i++)
            {
                format += "0";
            }

            retval = data.ToString(format);
            return retval;
        }

        public static string GetTextFitted(this string data, int toFit)
        {
            string retval = string.Empty;

            if (data.Length > toFit)
            {
                data = data.Substring(0, toFit);
            }

            retval += data.PadRight(toFit);
            return ReplaceTRChars(retval);
        }

        public static string GetDateFormated(this DateTime data)
        {
            string retval = string.Empty;
            retval += data.Year.ToString("0000") + data.Month.ToString("00") + data.Day.ToString("00");
            return retval;
        }

        public static string GetTextFitted_AKB(this string data, int toFit)
        {
            string retval = string.Empty;

            // data = data.RemoveStarting0s();
            data = data.Substring(2, 8);

            if (data.Length > toFit)
            {
                data = data.Substring(0, toFit);
            }
            retval += data.PadRight(toFit);

            return ReplaceTRChars(retval);
        }

        public static DateTime GetSAPDateToDatetime(this string sdate)
        {
            string[] d = sdate.Split("-".ToCharArray());
            return new DateTime(Convert.ToInt32(d[0]), Convert.ToInt32(d[1]), Convert.ToInt32(d[2]));
        }
        public static string GetDateFormated_AKB(this DateTime data)
        {
            string retval = string.Empty;
            retval += data.Day.ToString("00") + "." + data.Month.ToString("00") + "." + data.Year.ToString("0000");
            return retval;
        }

        public static string ReplaceTRChars(string val)
        {
            val = val.Replace("Ü", "U");
            val = val.Replace("Ç", "C");
            val = val.Replace("İ", "I");
            val = val.Replace("Ğ", "G");
            val = val.Replace("Ö", "O");
            val = val.Replace("ı", "I");
            val = val.Replace("Ş", "S");
            return val;
        }

        public static string CStringDecimalForLabels(this decimal val)
        {
            string retval = string.Empty;
            try
            {
                //retval = val.ToString("N2", _tr);
                retval = string.Format(new CultureInfo(Constants.Culture.TR), Constants.Format.CurrencyFormatString, val);
            }
            catch
            {

            }
            return retval;


        }

        public static string CStringInt(this int val)
        {
            string retval = string.Empty;
            try
            {
                retval = val.ToString("N0", _tr);
            }
            catch
            {

            }
            return retval;
        }

        public static int CInt32(this object i)
        {
            int retval = 0;

            try
            {
                retval = Convert.ToInt32(i);
            }
            catch
            {

            }
            return retval;
        }

        public static short CInt16(this object i)
        {
            short retval = 0;

            try
            {
                retval = Convert.ToInt16(i);
            }
            catch
            {

            }
            return retval;
        }
        public static int? CInt32NullIfZero(this object i)
        {
            int? retval;

            try
            {
                retval = Convert.ToInt32(i);
            }
            catch
            {
                retval = null;
            }
            if (retval.HasValue && retval.Value == 0)
            {
                retval = null;
            }
            return retval;
        }

        public static long CInt64(this object i)
        {
            long retval = 0;

            try
            {
                retval = Convert.ToInt64(i);
            }
            catch
            {

            }
            return retval;
        }

        public static bool CBoolean(this object i)
        {
            bool retval = false;
            try
            {
                retval = Convert.ToBoolean(i);
            }
            catch
            {

            }
            return retval;
        }

        public static decimal CDecimal(this object i)
        {
            decimal retval = 0;
            try
            {
                if (typeof(string) == i.GetType())
                {
                    retval = i.CString().ParseToDecimal();
                }
                else
                {
                    retval = Convert.ToDecimal(i);
                }
            }
            catch
            {
            }
            return retval;
        }

        public static string CStringImaginePart(this decimal n, bool addZeroEnd)
        {
            string retval;
            try
            {
                int fraction = Convert.ToInt32((Math.Abs((decimal)n) % 1).ToString(CultureInfo.InvariantCulture).Substring(2));
                retval = addZeroEnd ? (fraction < 10 ? fraction.CString() + "0" : fraction.CString()) : fraction.CString();
            }
            catch
            {
                retval = "00";
            }
            return retval;
        }

        /// <summary>
        /// Parses a string to decimal regardless of culture.
        /// The last comma "," or dot "." is considered to be the decimal separator.
        /// All other dots or commas are neglected.
        /// Maybe the matematicians wont like this, but it works.
        /// </summary>
        /// <param name="value">May include [+-][\d,.]*</param>
        /// <returns>decimal</returns>
        /// <exception cref="System.FormatException"></exception>

        public static decimal ParseToDecimal(this string value)
        {
            /*
             *  Pattern:
             *          ^([+-]?)([\d,.]*)([,.])(\d*)$|^([+-]?\d*)$
             *         
             *          ^ Beginning of string
             *          ([+-]?) May start with one + or -
             *          ([\d,.]*) Any combination of 0-9 and , and .
             *          ([,.]) One , or .
             *          (\d*) Any number digits
             *          $ End of string
             *         
             *          | OR
             *          ^ Beginning of string
             *          [+-]? May start with one + or -
             *          \d* Any number of digits
             *          $ End of string
             *         
             *          () are used to capture small parts of the pattern as subpatterns
             *          to be able to replace characters in the subpatterns only or to remove
             *          the culture variant decimal separator
             */
            value = value.Trim();
            const string pattern = @"^([+-]?)([\d,.]*)([,.])(\d*)$|^([+-]?\d*)$";
            var r = new Regex(pattern);
            var matches = r.Match(value);
            var cleanedValue = new StringBuilder();
            cleanedValue.Append(matches.Groups[1].Value);
            cleanedValue.Append(string.IsNullOrEmpty(matches.Groups[2].Value)
                                    ? matches.Groups[5].Value
                                    : Regex.Replace(matches.Groups[2].Value, "[,.]", ""));

            cleanedValue.Append(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);
            cleanedValue.Append(matches.Groups[4].Value);
            return Decimal.Parse(cleanedValue.ToString(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
        }

        public static double CDouble(this object i)
        {
            double retval = 0;
            try
            {
                retval = Convert.ToDouble(i);
            }
            catch
            {

            }
            return retval;
        }

        public static DateTime? CDate(this object i)
        {
            DateTime? retval;
            try
            {
                retval = Convert.ToDateTime(i, _tr);
            }
            catch
            {
                retval = null;
            }
            return retval;
        }

        public static string CDate(this object dt, string culture, string format)
        {
            DateTime? dt1 = CDate(dt);
            if (dt1.HasValue)
            {
                CultureInfo c = new CultureInfo(culture);
                return dt1.Value.ToString(format, c);
            }
            else
            {
                return string.Empty;
            }
        }

        public static List<Control> GetAllControls(this ControlCollection cc)
        {
            List<Control> lc = new List<Control>();

            foreach (Control c in cc)
            {
                lc.Add(c);
                if (c.HasControls())
                {
                    lc.AddRange(GetAllControls(c.Controls));
                }
            }

            return lc;
        }

        public static List<WebControl> GetAllWebControls(this ControlCollection cc)
        {
            List<WebControl> lc = new List<WebControl>();

            foreach (Control c in cc)
            {
                if (c is WebControl)
                {
                    lc.Add((WebControl)c);
                }

                if (c.HasControls())
                {
                    lc.AddRange(GetAllWebControls(c.Controls));
                }
            }

            return lc;
        }

        public static Control FindControlRecursive(this Control parentControl, string childControlID)
        {
            List<Control> controls = GetAllControls(parentControl.Controls);
            return controls.FirstOrDefault(c => c.ID == childControlID);
        }

        public static List<Control> FindControlsRecursive(this Control parentControl, string childControlID)
        {
            List<Control> controls = GetAllControls(parentControl.Controls);
            return controls.Where(c => c.ID == childControlID).ToList();
        }

        public static string ReadResource(this string resourceName)
        {
            string cont = string.Empty;
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                StreamReader reader = new StreamReader(stream);
                cont = reader.ReadToEnd();
            }

            return cont;
        }

        public static Control GetResourceControl(this Control container, string resourceName)
        {
            string content = ReadResource(resourceName);
            Control control = container.Page.ParseControl(content);
            return control;
        }

        public static void RenderControlID(this Control container)
        {
            if (container != null)
            {
                List<Control> controls = GetAllControls(container.Controls);

                foreach (Control c in controls)
                {
                    if (c is WebControl || c is HtmlControl)
                    {
                        c.ID = container.ID + "_" + c.ID;
                    }
                }
            }
        }

        public static string SetEmptyMessage(Dictionary<Control, string> checkList)
        {
            if (checkList.Any())
            {
                string message = string.Empty;
                foreach (KeyValuePair<Control, string> keyValuePair in checkList)
                {
                    if (keyValuePair.Key is TextBox)
                    {
                        TextBox txt = keyValuePair.Key as TextBox;
                        if (string.IsNullOrEmpty(txt.Text))
                        {
                            message = keyValuePair.Value;
                        }
                    }
                    else if (keyValuePair.Key is DropDownList)
                    {
                        DropDownList drpList = keyValuePair.Key as DropDownList;

                        if (string.IsNullOrEmpty(drpList.SelectedValue))
                        {
                            message = keyValuePair.Value;
                        }
                    }
                }
                return message;
            }

            return null;
        }



        public static bool IsSimpleType(
        this Type type)
        {
            return
                type.IsValueType ||
                type.IsPrimitive ||
                new Type[] { 
				typeof(String),
				typeof(Decimal),
				typeof(DateTime),
				typeof(DateTimeOffset),
				typeof(TimeSpan),
				typeof(Guid)
			}.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }

        public static void BindDropDown(this DropDownList drp, DataTable dt, string textField, string valueField)
        {
            drp.SelectedIndex = -1;
            drp.DataSource = dt;
            drp.DataTextField = textField;
            drp.DataValueField = valueField;
            drp.DataBind();
        }

        public static void FillNumberDropdown(this DropDownList drp, int start, int end, int interval, int strLength)
        {
            for (int i = start; i <= end; i += interval)
            {
                drp.Items.Add(new ListItem(String.Format("{0:00}", i), i.CString()));
            }
        }

        public static void ClearInputs(this Control c)
        {
            List<Control> lc = GetAllControls(c.Controls);

            foreach (Control cont in lc)
            {
                if (cont is TextBox)
                {
                    TextBox txt = cont as TextBox;
                    txt.Text = string.Empty;
                }
                else if (cont is DropDownList)
                {
                    DropDownList drp = cont as DropDownList;
                    if (drp.Items.Count > 0)
                    {
                        drp.SelectedIndex = 0;
                    }
                }
                else if (cont is CheckBox)
                {
                    CheckBox chk = cont as CheckBox;
                    chk.Checked = false;
                }
                else if (cont is RadioButton)
                {
                    RadioButton rd = cont as RadioButton;
                    rd.Checked = false;
                }
            }
        }

        public static void AddItemToDropdown(this DropDownList drp, int index, string text, string itemValue)
        {
            if (drp.Items.FindByValue(itemValue) == null)
            {
                drp.Items.Insert(index, new ListItem(text, itemValue));
            }
        }

        public static DateTime GetFirstDateOfWeek(this DateTime date)
        {
            DateTime retval = date;
            int dif = (int)retval.DayOfWeek - 1;
            if (dif < 0)
            {
                dif = 7 + dif;
            }
            retval = retval.AddDays(-1 * dif);
            return retval;
        }

        public static DateTime GetLastDateOfWeek(this DateTime date)
        {
            DateTime retval = date;
            int dif = (7 - (int)retval.DayOfWeek) % 7;
            retval = retval.AddDays(dif);
            return retval;
        }

        public static int GetWeekNumber(this DateTime date)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        public static string DateTimeToShort(this DateTime? dt)
        {
            if (dt.HasValue)
            {
                return dt.Value.ToString("d", _tr);
            }
            else
            {
                return null;
            }
        }

        public static string DateTimeToShort(this DateTime dt)
        {
            return dt.ToString("d", _tr);
        }

        public static string DateTimeToYMDString(this DateTime dt, string seperator)
        {
            return dt.ToString("yyyy" + seperator + "MM" + seperator + "dd");
        }

        public static string DateTimeToNumberString(this DateTime dt)
        {
            return Regex.Replace(dt.ToString("d", _tr), "[^0-9]", "");
        }

        public static DataTable SelectDistinct(this DataTable sourceTable, params string[] fieldNames)
        {
            if (fieldNames == null || fieldNames.Length == 0)
                throw new ArgumentNullException("fieldNames");

            object[] lastValues = new object[fieldNames.Length];
            DataTable newTable = new DataTable();

            foreach (string fieldName in fieldNames)
                newTable.Columns.Add(fieldName, sourceTable.Columns[fieldName].DataType);

            DataRow[] orderedRows = sourceTable.Select("", string.Join(", ", fieldNames));

            foreach (DataRow row in orderedRows)
            {
                if (!FieldValuesAreEqual(lastValues, row, fieldNames))
                {
                    newTable.Rows.Add(CreateRowClone(row, newTable.NewRow(), fieldNames));

                    SetLastValues(lastValues, row, fieldNames);
                }
            }

            return newTable;
        }

        private static bool FieldValuesAreEqual(object[] lastValues, DataRow currentRow, string[] fieldNames)
        {
            bool areEqual = true;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                if (lastValues[i] == null || !lastValues[i].Equals(currentRow[fieldNames[i]]))
                {
                    areEqual = false;
                    break;
                }
            }

            return areEqual;
        }

        private static DataRow CreateRowClone(DataRow sourceRow, DataRow newRow, IEnumerable<string> fieldNames)
        {
            foreach (string field in fieldNames)
                newRow[field] = sourceRow[field];

            return newRow;
        }

        private static void SetLastValues(object[] lastValues, DataRow sourceRow, string[] fieldNames)
        {
            for (int i = 0; i < fieldNames.Length; i++)
                lastValues[i] = sourceRow[fieldNames[i]];
        }

        public static void SelectDropDownValue(this DropDownList drp, string value)
        {
            try
            {
                drp.SelectedValue = value;
            }
            catch
            {
                if (drp.Items.Count > 0)
                {
                    drp.SelectedIndex = 0;
                }
            }
        }

        public static string StripHTMLTags(this string str)
        {
            const string pattern = @"<(.|\n)*?>";
            return Regex.Replace(str, pattern, string.Empty);
        }

        public static string ExtractPageNameFromURL(this string url)
        {
            int startIndex = url.LastIndexOf('/');
            startIndex = startIndex + 1;
            int endindex = url.LastIndexOf('?');
            if (endindex == -1)
            {
                endindex = url.Length;
            }
            return ToLowerEnglish(url.Substring(startIndex, (endindex - startIndex)));
        }

        public static string ToLowerEnglish(this string str)
        {
            return str.ToLower(_en);
        }

        public static string ToUpperEnglish(this string str)
        {
            return str.ToUpper(_en);
        }

        public static string ToLowerTurkish(this string str)
        {
            return str.ToLower(_tr);
        }

        public static string ToLowerAlphaNumericEnglish(this string str)
        {
            str = str.ToLowerEnglish().Replace("(", "");
            string charsToExclude = "()!'^+%&/=?_-*@<>|.:,;£@#$[]{\\} ";
            foreach (char c in charsToExclude)
            {
                str = str.Replace(c.ToString(), "");
            }
            str = str.Replace("ö", "o");
            str = str.Replace("ç", "c");
            str = str.Replace("ş", "s");
            str = str.Replace("ı", "i");
            str = str.Replace("ü", "u");
            str = str.Replace("ğ", "g");
            return str;
        }

        public static string ToTitleCaseTurkish(this string str)
        {
            return _tr.TextInfo.ToTitleCase(str.ToLowerEnglish()).Replace("_", " ");
        }

        public static string ToTitleCaseTurkishTurkishText(this string str)
        {
            return _tr.TextInfo.ToTitleCase(str.ToLowerTurkish()).Replace("_", " ");
        }

        public static List<int> GetCheckedIDs(this Control rpt, string controlName)
        {
            var src = from Control c in rpt.Controls
                      select ((HtmlInputCheckBox)c.FindControl(controlName));
            src = src.Where(i => i.Checked);
            return src.Select(i => CInt32(i.Value)).ToList();
        }

        public static List<string> GetCheckedIDsString(this Control rpt, string controlName)
        {
            var src = from Control c in rpt.Controls
                      select ((HtmlInputCheckBox)c.FindControl(controlName));
            src = src.Where(i => i.Checked);
            return src.Select(i => i.Value).ToList();
        }

        public static bool IsValidEmail(this string inputEmail)
        {
            const string regexEmail = @"([a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)";

            inputEmail = inputEmail ?? "";
            Regex re = new Regex(regexEmail);

            if (re.IsMatch(inputEmail))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static int TakeNumber(this string str)
        {
            const string regex = "[^0-9 -]";

            Regex rgx = new Regex(regex);
            str = rgx.Replace(str, "");

            return str.CInt32();
        }

        public static string GetFirstChars(this string s, int length)
        {
            if (length > s.Length)
            {
                length = s.Length;
            }

            return string.IsNullOrEmpty(s.Trim()) ? string.Empty : s.Substring(0, length);
        }

        public static string ToHyphenIfEmpty(this string s)
        {
            return string.IsNullOrEmpty(s.Trim()) ? "-" : s;
        }

        public static int ToMonthNumber(this string s)
        {
            int retval = 0;
            switch (s.ToLower())
            {
                case "ocak":
                    retval = 1;
                    break;
                case "şubat":
                    retval = 2;
                    break;
                case "mart":
                    retval = 3;
                    break;
                case "nisan":
                    retval = 4;
                    break;
                case "mayıs":
                    retval = 5;
                    break;
                case "haziran":
                    retval = 6;
                    break;
                case "temmuz":
                    retval = 7;
                    break;
                case "ağustos":
                    retval = 8;
                    break;
                case "eylül":
                    retval = 9;
                    break;
                case "ekim":
                    retval = 10;
                    break;
                case "kasım":
                    retval = 11;
                    break;
                case "aralık":
                    retval = 12;
                    break;
            }
            return retval;
        }

        public static string ToMonthName(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                string[] months = new[]
                {
                    "Ocak",
                    "Şubat",
                    "Mart",
                    "Nisan",
                    "Mayıs",
                    "Haziran",
                    "Temmuz",
                    "Ağustos",
                    "Eylül",
                    "Ekim",
                    "Kasım",
                    "Aralık"            
                };
                if (s.CInt32() <= 12)
                {
                    return months[s.CInt32() - 1];
                }
                else
                {
                    return "-";
                }


            }
            return "";
        }

        public static string ToDayName(this DateTime? d)
        {
            DateTime date = (DateTime)d;
            string str = string.Empty;
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    str = "Cuma";
                    break;
                case DayOfWeek.Monday:
                    str = "Pazartesi";
                    break;
                case DayOfWeek.Saturday:
                    str = "Cumartesi";
                    break;
                case DayOfWeek.Sunday:
                    str = "Pazar";
                    break;
                case DayOfWeek.Thursday:
                    str = "Perşembe";
                    break;
                case DayOfWeek.Tuesday:
                    str = "Salı";
                    break;
                case DayOfWeek.Wednesday:
                    str = "Çarşamba";
                    break;
            }
            return str;
        }

        public static string Base64Encode(this string data)
        {
            try
            {
                byte[] encDataByte = Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encDataByte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in Base64Encode" + e.Message);
            }
        }

        public static string Base64Decode(this string data)
        {
            try
            {
                UTF8Encoding encoder = new UTF8Encoding();
                Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecodeByte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in Base64Decode" + e.Message);
            }
        }

        public static string ReplaceURLs(this string str)
        {
            string strInput = str;

            strInput = Regex.Replace(strInput, RegexPatternURL, "$1<a href='$2' target='_blank'>$2</a> ", RegexOptions.IgnoreCase);
            return strInput;
        }

        public static string ReplaceEmails(this string str)
        {
            string strInput = str;
            strInput = Regex.Replace(strInput, RegexPatternEmail, "$1<a href='mailto:$2' target='_blank'>$2</a> ", RegexOptions.IgnoreCase);
            return strInput;
        }

        public static string[] ExtractURLs(this string str)
        {
            MatchCollection matches = Regex.Matches(str, RegexPatternURL, RegexOptions.IgnoreCase);
            string[] matchList = new string[matches.Count];

            // Report on each match.
            int c = 0;
            foreach (Match match in matches)
            {
                matchList[c] = match.Value;
                c++;
            }

            return matchList;
        }

  
        public static string ToDateString(this DateTime dt, string separator)
        {
            return dt.ToString("yyyy" + separator + "MM" + separator + "dd");
        }

        public static IEnumerable<T> DistinctBy<T>(this IEnumerable<T> source, Func<T, object> uniqueCheckerMethod)
        {
            return source.Distinct(new GenericComparer<T>(uniqueCheckerMethod));
        }

        class GenericComparer<T> : IEqualityComparer<T>
        {
            public GenericComparer(Func<T, object> uniqueCheckerMethod)
            {
                this._uniqueCheckerMethod = uniqueCheckerMethod;
            }

            private Func<T, object> _uniqueCheckerMethod;

            bool IEqualityComparer<T>.Equals(T x, T y)
            {
                return this._uniqueCheckerMethod(x).Equals(this._uniqueCheckerMethod(y));
            }

            int IEqualityComparer<T>.GetHashCode(T obj)
            {
                return this._uniqueCheckerMethod(obj).GetHashCode();
            }
        }

        public static string[] DataTableToStringArray(this DataTable dt, string columnName)
        {
            string[] retval = new string[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                retval[i] = CString(dr[columnName]);
                i++;
            }
            return retval;
        }

        public static int[] DataTableToIntArray(this DataTable dt, string column)
        {
            int[] retval = new int[dt.Rows.Count];

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                retval[i] = CInt32(dr[column]);
                i++;
            }

            return retval;
        }

        public static void ToggleGridViewImageButtonsById(this GridView grd, bool enabled, string[] imageButtonIds)
        {

            foreach (GridViewRow row in grd.Rows)
            {
                foreach (string id in imageButtonIds)
                {
                    ImageButton btn = row.FindControl(id) as ImageButton;
                    btn.Enabled = (enabled) ? true : false;
                    btn.Visible = (enabled) ? true : false;
                }
            }
        }

        public static void ToogleRepeaterImageButtonsById(this Repeater rpt, bool enabled, string[] imageButtonIds)
        {
            foreach (RepeaterItem i in rpt.Items)
            {
                foreach (string id in imageButtonIds)
                {
                    ImageButton btn = i.FindControl(id) as ImageButton;
                    btn.Enabled = (enabled) ? true : false;
                    btn.Visible = (enabled) ? true : false;
                }
            }
        }
        public static DateTime GetFirstDateOfWeekByWeeknumber(this int weeknumber, int year)
        {

            DateTime jan1 = new DateTime(year, 1, 1);
            int daysoffset = DayOfWeek.Monday - jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysoffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstweek = cal.GetWeekOfYear(jan1, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            if (firstweek <= 1)
            {
                weeknumber -= 1;
            }
            DateTime result = firstMonday.AddDays(weeknumber * 7);
            return result;
        }

        public static void ToogleDataListImageButtonsById(this DataList dl, bool enabled, string[] imageButtonIds)
        {

            foreach (Control c in dl.Controls)
            {
                foreach (string id in imageButtonIds)
                {
                    ImageButton btn = c.FindControl(id) as ImageButton;
                    btn.Enabled = (enabled) ? true : false;
                    btn.Visible = (enabled) ? true : false;
                }
            }
        }
        public static string URLEncode(this string url)
        {
            return System.Web.HttpUtility.UrlEncode(url);
        }
        public static string URLDecode(this string url)
        {
            return System.Web.HttpUtility.UrlDecode(url);
        }


        public static DataTable ToDataTable<T>(this IEnumerable<T> varlist) where T : class
        {
            if (varlist is IQueryable)
            {
                varlist = varlist.ToList();
            }
            DataTable dtReturn = new DataTable();
            // Could add a check to verify that there is an element 0

            T topRec = varlist.ElementAtOrDefault(0);

            // Use reflection to get property names, to create table
            // column names
            if (topRec != null)
            {
                PropertyInfo[] oProps = topRec.GetType().GetProperties();

                foreach (PropertyInfo pi in oProps)
                {
                    Type colType = pi.PropertyType;
                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        colType = colType.GetGenericArguments()[0];
                    }

                    dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                }

                foreach (T rec in varlist)
                {
                    DataRow dr = dtReturn.NewRow();
                    foreach (PropertyInfo pi in oProps)
                    {
                        dr[pi.Name] = pi.GetValue(rec, null) ?? DBNull.Value; // pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null);
                    }
                    dtReturn.Rows.Add(dr);
                }
            }
            return (dtReturn);
        }



        #endregion

        #region [ STATIC METHODS ]

        public static bool IsDigit(string strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            const string strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            const string strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber) &&
            !objTwoMinusPattern.IsMatch(strNumber) &&
            objNumberPattern.IsMatch(strNumber);
        }

        public static DateTime GetFirstDayOfWeek(int year, int week)
        {
            DateTime dt = new DateTime(year, 1, 1).AddDays(7 * week);
            return GetFirstDateOfWeek(dt);
        }

        public static string SendEmail(string pSmtpClient, string from, string fromDisplayName, string to, string subject, string body)
        {
            string retval = string.Empty;
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(from, fromDisplayName);
                msg.Subject = subject;
                msg.Body = body;
                msg.To.Add(to);
                msg.BodyEncoding = Encoding.UTF8;
                msg.Priority = MailPriority.High;
                msg.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient(pSmtpClient);
                smtpClient.Send(msg);
            }
            catch (Exception)
            {
                //retval = Err.Message;
                retval = "Sorry, an error has occurred.";
            }
            return retval;
        }

        public static string GetVisitorIP()
        {
            // Look for a proxy address first
            string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            // If there is no proxy, get the standard remote address
            if (string.IsNullOrEmpty(ip) || ip.ToLower() == "unknown")
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            return ip;
        }

        public static string GenerateRandomCode(int seed)
        {
            //string[] abc = new[] { "a", "0", "b", "1", "c", "2", "d", "3", "e", "4", "f", "5", "g", "6", "h", "7", "i", "8", "j", "9", "k", "0", "l", "1", "m", "2", "n", "3", "o", "4", "p", "5", "q", "6", "r", "7", "v", "8", "w", "9", "y", "0", "z" };
            string[] abc = new[] { "a", "0", "b", "c", "2", "d", "3", "e", "4", "f", "5", "g", "6", "h", "7", "8", "j", "9", "k", "0", "m", "2", "n", "3", "o", "4", "p", "5", "q", "6", "r", "7", "v", "8", "w", "9", "y", "0", "z" };
            Random random = new Random(seed);
            string s = string.Empty;
            for (int i = 0; i < 6; i++)
            {
                s = string.Concat(s, abc[random.Next(abc.Length)]);
            }
            return s;
        }

        public static string GenerateRandomNumberCode(int seed)
        {
            string[] abc = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            Random random = new Random(seed);
            string s = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                s = string.Concat(s, abc[random.Next(abc.Length)]);
            }
            return s;
        }

        public static string GeneratePassword()
        {
            string[] abc = new[] { "a", "0", "b", "c", "2", "d", "3", "e", "4", "f", "5", "g", "6", "h", "7", "8", "j", "9", "k", "0", "m", "2", "n", "3", "o", "4", "p", "5", "q", "6", "r", "7", "v", "8", "w", "9", "y", "0", "z" };
            Random random = new Random(DateTime.Now.Millisecond);
            string s = string.Empty;
            for (int i = 0; i < 8; i++)
            {
                s = string.Concat(s, abc[random.Next(abc.Length)]);
            }
            return s;
        }

        /// <summary>
        /// Generate Order No From OrderID
        /// </summary>
        /// <param name="ORDERID"></param>
        /// <returns></returns>
        public static string CreateOrderNo(int ORDERID)
        {
            // orderid.Last + random 2 char + orderid.last-1 + Random 1 char + (OrderID + DefaultSayi).First2 + random 1 char + (OrderID + DefaultSayi).Rest + random 1 char
            string OrderNo = string.Empty;

            int SumOrderID = ORDERID + ORDERID;

            char[] orderid = ORDERID.ToString().ToArray();
            List<char> str = CreateRandomKey();
            string a = str.FirstOrDefault().ToString();
            string b = str[1].ToString();
            string c = str[2].ToString();
            string d = str[3].ToString();
            string e = str[4].ToString();
            List<char> strorder = SumOrderID.ToString().ToList();
            int len = strorder.Count;

            string s1 = new string(strorder.Take(1).ToArray());
            string s2 = new string(strorder.Skip(2).Take(len - 2).ToArray());
            string s3 = new string(strorder.Skip(1).Take(1).ToArray());
            return OrderNo = orderid.Last().ToString() + d + s3 + b + orderid[len - 2].ToString() + a + s2 + e + s1 + c;

        }

        public static List<char> CreateRandomKey()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var random = new Random();
            var result = new string(
            Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)])
            .ToArray());
            return result.ToList();
        }

        public static void SetCookie(string name, string val, int days)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = val;
            DateTime dtNow = DateTime.Now;
            TimeSpan tsMinute = new TimeSpan(days, 0, 0, 0);
            cookie.Expires = dtNow + tsMinute;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string GetCookie(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie != null)
            {
                return cookie.Value;
            }

            return null;
        }

        #endregion

        

        public static int GetIndex(this OrderedDictionary od, string key)
        {
            int retval = 0;
            int i = 0;
            foreach (DictionaryEntry dictionaryEntry in od)
            {
                if (dictionaryEntry.Key.CString() == key)
                {
                    retval = i;
                }
                i++;
            }
            return retval;
        }


        public static DataTable EditColumnNamesForExcel(DataTable dt, OrderedDictionary columns)
        {
            List<string> removeColums = new List<string>();
            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                if (!columns.Contains(dt.Columns[i].Caption))
                {
                    removeColums.Add(dt.Columns[i].Caption);
                }
            }
            foreach (string removeColum in removeColums)
            {
                dt.Columns.Remove(removeColum);
            }

            foreach (DictionaryEntry entry in columns)
            {
                if (dt.Columns.Contains(entry.Key.CString()))
                {
                    dt.Columns[entry.Key.CString()].SetOrdinal(columns.GetIndex(entry.Key.CString()));
                }
            }

            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                if (columns.Contains(dt.Columns[i].Caption.CString()))
                {
                    dt.Columns[i].ColumnName = columns[dt.Columns[i].Caption.CString()].CString();
                }
            }

            return dt;
        }

        public static string YaziyaCevir(this decimal tutar)
        {
            string sTutar = tutar.ToString("F2").Replace('.', ','); // Replace('.',',') ondalık ayracının . olma durumu için           
            string lira = sTutar.Substring(0, sTutar.IndexOf(',')); //tutarın tam kısmı
            string kurus = sTutar.Substring(sTutar.IndexOf(',') + 1, 2);
            string yazi = "";

            string[] birler = { "", "BİR", "İKİ", "Üç", "DÖRT", "BEŞ", "ALTI", "YEDİ", "SEKİZ", "DOKUZ" };
            string[] onlar = { "", "ON", "YİRMİ", "OTUZ", "KIRK", "ELLİ", "ALTMIŞ", "YETMİŞ", "SEKSEN", "DOKSAN" };
            string[] binler = { "KATRİLYON", "TRİLYON", "MİLYAR", "MİLYON", "BİN", "" }; //KATRİLYON'un önüne ekleme yapılarak artırabilir.

            int grupSayisi = 6; //sayıdaki 3'lü grup sayısı. katrilyon içi 6. (1.234,00 daki grup sayısı 2'dir.)
            //KATRİLYON'un başına ekleyeceğiniz her değer için grup sayısını artırınız.

            lira = lira.PadLeft(grupSayisi * 3, '0'); //sayının soluna '0' eklenerek sayı 'grup sayısı x 3' basakmaklı yapılıyor.           

            string grupDegeri;

            for (int i = 0; i < grupSayisi * 3; i += 3) //sayı 3'erli gruplar halinde ele alınıyor.
            {
                grupDegeri = "";

                if (lira.Substring(i, 1) != "0")
                    grupDegeri += birler[Convert.ToInt32(lira.Substring(i, 1))] + "YÜZ"; //yüzler               

                if (grupDegeri == "BİRYÜZ") //biryüz düzeltiliyor.
                    grupDegeri = "YÜZ";

                grupDegeri += onlar[Convert.ToInt32(lira.Substring(i + 1, 1))]; //onlar

                grupDegeri += birler[Convert.ToInt32(lira.Substring(i + 2, 1))]; //birler               

                if (grupDegeri != "") //binler
                    grupDegeri += binler[i / 3];

                if (grupDegeri == "BİRBİN") //birbin düzeltiliyor.
                    grupDegeri = "BİN";

                yazi += grupDegeri;
            }

            if (yazi != "")
                yazi += " TL ";

            int yaziUzunlugu = yazi.Length;

            if (kurus.Substring(0, 1) != "0") //kuruş onlar
                yazi += onlar[Convert.ToInt32(kurus.Substring(0, 1))];

            if (kurus.Substring(1, 1) != "0") //kuruş birler
                yazi += birler[Convert.ToInt32(kurus.Substring(1, 1))];

            if (yazi.Length > yaziUzunlugu)
                yazi += " Kr.";
            else
                yazi += "SIFIR Kr.";

            return yazi;
        }

    }

    public static class Cryptor
    {
        #region [ DEFAULT ENCRYPTION ]

        private static SymmetricAlgorithm _mCsp;
        private const string Key = "QhgUkIoeT95Fhd/oUrkHwar8/TY092Xc";//32 karakter
        private const string Iv = "BatXT5J1Ia4=";//12 karakter = ile bitmeli

        static Cryptor()
        {
            _mCsp = SetEnc();
            _mCsp.IV = Convert.FromBase64String(Iv);
            _mCsp.Key = Convert.FromBase64String(Key);
        }

        public static string CreateSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic
            // service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        public static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd =
                FormsAuthentication.HashPasswordForStoringInConfigFile(
                saltAndPwd, "SHA1");
            return hashedPwd;
        }

        public static string EncryptString(string value)
        {
            MemoryStream ms;
            CryptoStream cs;
            Byte[] byt;
            try
            {
                ICryptoTransform ct = _mCsp.CreateEncryptor(_mCsp.Key, _mCsp.IV);
                byt = Encoding.UTF8.GetBytes(value);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw (new Exception("Şifreleme esnasında hata oluştu.", ex));
            }
        }

        public static string DecryptString(string value)
        {
            MemoryStream ms;
            CryptoStream cs;
            Byte[] byt;
            try
            {
                ICryptoTransform ct = _mCsp.CreateDecryptor(_mCsp.Key, _mCsp.IV);
                byt = Convert.FromBase64String(value);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw (new Exception("Şifre çözme esnasında hata oluştu.", ex));
            }
        }

        private static SymmetricAlgorithm SetEnc()
        {
            return new TripleDESCryptoServiceProvider();
        }

        #endregion


        #region [ DIVA ENCRYPTION ]

        private const string DivaKey = "stuckinamomentyoucantgetoutofit";

        /// <summary>
        /// Encrypt a string using dual encryption method. Return a encrypted cipher Text
        /// </summary>
        /// <param name="toEncrypt">string to be encrypted</param>
        /// <param name="useHashing">use hashing? send to for extra secirity</param>
        /// <returns></returns>
        public static string EncryptStringDiva(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(DivaKey));
                hashmd5.Clear();
            }
            else
                keyArray = Encoding.UTF8.GetBytes(DivaKey);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
        /// <returns></returns>
        public static string DecryptStringDiva(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(DivaKey));
                hashmd5.Clear();
            }
            else
                keyArray = Encoding.UTF8.GetBytes(DivaKey);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return Encoding.UTF8.GetString(resultArray);
        }

        #endregion

    }
}
