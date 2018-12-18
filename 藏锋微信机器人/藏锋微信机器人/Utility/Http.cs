using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace 藏锋微信机器人
{
    public class Http
    {
        /// <summary>
        /// 整个Session的cookie
        /// </summary>
        public static CookieContainer CookiesContainer;
        /// <summary>
        /// Session带Cookie的HTTPGET
        /// </summary>
        /// <param name="getUrl"></param>
        /// <returns></returns>
        public static string WebGet(string getUrl)
        {
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getUrl);
                request.Method = "GET";
                if (CookiesContainer == null)
                {
                    CookiesContainer = new CookieContainer();
                }
				request.Proxy = null;
				request.CookieContainer = CookiesContainer;  //启用cookie
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                strResult = reader.ReadToEnd();
                response.Close();
            }
            catch (Exception ex)
            {
				throw new Exception("网络请求错误:\n\r" + ex.Message);
            }
            return strResult;
        }

		/// <summary>
		/// 向服务器发送get请求  返回服务器回复数据
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static byte[] Get(string url, bool video=false)
		{
			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				request.Method = "get";
				if (video)
				{
					//request.Headers = webHeaderCollection;
					//request.Headers.Add(HttpRequestHeader.Range, "bytes=0-");
					//request.AddRange(0, 1048575);
					request.AddRange(0);
				}
				
				if (CookiesContainer == null)
				{
					CookiesContainer = new CookieContainer();
				}

				request.CookieContainer = CookiesContainer;  //启用cookie

				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				Stream response_stream = response.GetResponseStream();

				int count = (int)response.ContentLength;
				int offset = 0;
				byte[] buf = new byte[count];
				while (count > 0)  //读取返回数据
				{
					int n = response_stream.Read(buf, offset, count);
					if (n == 0) break;
					count -= n;
					offset += n;
				}
				return buf;
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Session带Cookie的HTTPPOIST
		/// </summary>
		/// <param name="postUrl"></param>
		/// <param name="strPost"></param>
		/// <returns></returns>
		public static string WebPost(string postUrl, string strPost)
        {
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(postUrl);
                Encoding encoding = Encoding.UTF8;
                //encoding.GetBytes(postData);
                byte[] bs = Encoding.UTF8.GetBytes(strPost);
                string responseData = String.Empty;
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded";
				request.Proxy = null;
				if (CookiesContainer == null)
                {
                    CookiesContainer = new CookieContainer();
                }
                request.CookieContainer = CookiesContainer;  //启用cookie
                request.ContentLength = bs.Length;
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                    reqStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                strResult = reader.ReadToEnd();
                response.Close();
            }
            catch (Exception ex)
            {
				throw new Exception("网络请求错误:\n\r" + ex.Message);
			}
            return strResult;
        }
        /// <summary>
        /// 新的不带Cookie的HTTPPOIST
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="strPost"></param>
        /// <returns></returns>
        public static string WebPost2(string postUrl, string strPost)
        {
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(postUrl);
                Encoding encoding = Encoding.UTF8;
                //encoding.GetBytes(postData);
                byte[] bs = Encoding.UTF8.GetBytes(strPost);
                string responseData = String.Empty;
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
                request.ContentType = "application/json; charset=UTF-8";
				request.Proxy = null;
                if (CookiesContainer == null)
                {
                    CookiesContainer = new CookieContainer();
                }
                request.CookieContainer = CookiesContainer;  //启用cookie
                request.ContentLength = bs.Length;
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                    reqStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                strResult = reader.ReadToEnd();
                response.Close();
            }
            catch (Exception ex)
            {
				throw new Exception("网络请求错误:\n\r" + ex.Message);
			}
            return strResult;
        }
    }
}
