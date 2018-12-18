#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：藏锋微信机器人.Model
* 项目描述 ：
* 类 名 称 ：wxRequest
* 类 描 述 ：
* 所在的域 ：cangfeng-PC
* 命名空间 ：藏锋微信机器人.Model
* 机器名称 ：CANGFENG-PC 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：cangfeng
* 创建时间 ：2018/12/18 20:28:06
* 更新时间 ：2018/12/18 20:28:06
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

namespace 藏锋微信机器人
{
	public class csMSG
	{
		public int Type { get; set; }
		public string Content { get; set; }
		public string FromUserName { get; set; }
		public string ToUserName { get; set; }
		public string LocalID { get; set; }
		public string ClientMsgId { get; set; }
	}

	public class csBaseRequest
	{
		public string Uin;
		public string Sid;
		public string Skey;
		public string DeviceID;
	}

	public class message
	{
		public csMSG Msg { get; set; }
		public csBaseRequest BaseRequest { get; set; }
	}

	//发送的媒体的结构
	public class media
	{

	}
}
