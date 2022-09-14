using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrentPaper
{
    public static class MainEngine
    {
        #region Public Fields

        public const int Offset = 24;

        #endregion

        #region Public Methods

        public static string[] GetCurrentWallpapers()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop"))
                {
                    int count = (int)key.GetValue("TranscodedImageCount");
                    string valueName = "TranscodedImageCache";
                    var resultList = new List<string>();

                    if (count < 2)
                    {
                        AddTranscodedImageCache(key, valueName, resultList);
                    }
                    else
                    {
                        for (int i = 0; i < count; ++i)
                        {
                            AddTranscodedImageCache(key, string.Format("{0}_{1:D3}", valueName, i), resultList);
                        }
                    }

                    return resultList.ToArray();
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Private Methods

        private static void AddTranscodedImageCache(RegistryKey key, string valueName, List<string> list)
        {
            byte[] bytes;

            try
            {
                bytes = key.GetValue(valueName) as byte[];
            }
            catch
            {
                throw;
            }

            int length = bytes.Length;

            if (length <= Offset)
            {
                return;
            }

            string text;

            try
            {
                text = Encoding.Unicode.GetString(bytes, Offset, (length - Offset));
            }
            catch
            {
                list.Add("???");
                return;
            }

            int startIndex;
            bool found = false;

            for (startIndex = 0; startIndex < text.Length; ++startIndex)
            {
                char c = text[startIndex];

                if ((c < 'A') || (c > 'z'))
                {
                    continue;
                }

                found = true;
                break;
            }

            string value;

            if (!found)
            {
                value = "???";
            }
            else
            {
                try
                {
                    value = text.Substring(startIndex).Trim();
                }
                catch
                {
                    value = "???";
                }
            }

            list.Add(value);
        }

        #endregion
    }
}
