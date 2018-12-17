#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：藏锋微信机器人.Core
* 项目描述 ：
* 类 名 称 ：bot
* 类 描 述 ：
* 所在的域 ：cangfeng-PC
* 命名空间 ：藏锋微信机器人.Core
* 机器名称 ：CANGFENG-PC 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：cangfeng
* 创建时间 ：2018/12/16 19:34:09
* 更新时间 ：2018/12/16 19:34:09
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
using Newtonsoft.Json;

namespace 藏锋微信机器人
{
	public class bot
	{
		private robot robot;

		/// <summary>
		/// 图灵apikey
		/// </summary>
		private string tuling_key= "6290d4258fb94796bbfaf0b92d3c7d51";

		/// <summary>
		/// 开关
		/// </summary>
		private bool robot_switch=true;

		/// <summary>
		/// 图灵接口地址
		/// </summary>
		private string url = "http://www.tuling123.com/openapi/api";

		public bot()
		{
			var path = System.Windows.Forms.Application.StartupPath + "\\xf.ini";
			robot_switch = CSHelper.ReadINI("setup", "robot_switch",  path) == "1";
			tuling_key = CSHelper.ReadINI("setup", "tuling_key",  path);
		}

		/// <summary>
		/// 获取自动回复的内容
		/// </summary>
		/// <param name="uid"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		public string tuling_auto_reply(string uid,string msg)
		{
			var result = "";
			
			if (!string.IsNullOrWhiteSpace(tuling_key)&& robot_switch)
			{
				var obj = new { key = tuling_key, info = msg, userid = uid };
				var postStr = JsonConvert.SerializeObject(obj);
				var tulingRtnStr=Http.WebPost2(url, postStr);
				var retunDict=JsonConvert.DeserializeObject<Dictionary<string,object>>(tulingRtnStr);
				if (retunDict["code"].ToString() == "100000")
				{
					result = retunDict["text"].ToString().Replace("<br>", "  ");
				}
				else
				{
					result =  tulingRtnStr;
				}
                
			}

			return result;
		}

	}

}
