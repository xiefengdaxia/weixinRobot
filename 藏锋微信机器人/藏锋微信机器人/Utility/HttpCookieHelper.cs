#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：藏锋微信机器人.Utility
* 项目描述 ：
* 类 名 称 ：HttpCookieHelper
* 类 描 述 ：
* 所在的域 ：cangfeng-PC
* 命名空间 ：藏锋微信机器人.Utility
* 机器名称 ：CANGFENG-PC 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：cangfeng
* 创建时间 ：2018/12/23 10:35:36
* 更新时间 ：2018/12/23 10:35:36
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ cangfeng 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace 藏锋微信机器人.Utility
{
	/// <summary>
	/// Cookie操作帮助类
	/// </summary>
	public static class HttpCookieHelper
	{
		/// <summary>
		/// 根据字符生成Cookie列表
		/// </summary>
		/// <param name="cookie">Cookie字符串</param>
		/// <returns></returns>
		public static List<CookieItem> GetCookieList(string cookie)
		{
			List<CookieItem> cookielist = new List<CookieItem>();
			foreach (string item in cookie.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries))
			{
				if (Regex.IsMatch(item, @"([\s\S]*?)=([\s\S]*?)$"))
				{
					Match m = Regex.Match(item, @"([\s\S]*?)=([\s\S]*?)$");
					cookielist.Add(new CookieItem() { Key = m.Groups[1].Value, Value = m.Groups[2].Value });
				}
			}
			return cookielist;
		}

		/// <summary>
		/// 根据Key值得到Cookie值,Key不区分大小写
		/// </summary>
		/// <param name="Key">key</param>
		/// <param name="cookie">字符串Cookie</param>
		/// <returns></returns>
		public static string GetCookieValue(string Key, string cookie)
		{
			foreach (CookieItem item in GetCookieList(cookie))
			{
				if (item.Key == Key)
					return item.Value;
			}
			return "";
		}
		/// <summary>
		/// 格式化Cookie为标准格式
		/// </summary>
		/// <param name="key">Key值</param>
		/// <param name="value">Value值</param>
		/// <returns></returns>
		public static string CookieFormat(string key, string value)
		{
			return string.Format("{0}={1};", key, value);
		}
	}

	/// <summary>
	/// Cookie对象
	/// </summary>
	public class CookieItem
	{
		/// <summary>
		/// 键
		/// </summary>
		public string Key { get; set; }
		/// <summary>
		/// 值
		/// </summary>
		public string Value { get; set; }
	}
}
