using UnityEngine;
using System.Collections;

public  class Global
{

	public static string LoadUIName; 
	public static string LoadSceneName = "InteractivePage";
	//public static bool isBattle = false;

	public static bool Contain3DScene = false;

	public static string PlayerName = "";
	public static Transform roleTransform;
	public static Vector3 postion = new Vector3(27.4f, 0.063f, 13f);
	public static bool hideEtc = false;

	public static Vector3 mapAndRoleOffset = new Vector3(35.88683f, 50.187f, 17.90932f) - new Vector3(30.28683f, 0.25f, 12.00932f);	

	
	public static string GetMinuteTime(float time)
	{
		int mm,ss;
		string stime = "0:00";
		if (time<=0) return stime;
		mm = (int)time/60;
		ss = (int)time%60;
		if(mm>60)
			stime = "59:59";
		else if (mm <10 && ss >=10)
		{
			stime = "0" + mm + ":" + ss;
		}else if (mm<10&&ss<10)
		{
			stime = "0"+mm+":0"+ss;
		}else if (mm>=10&&ss<10)
		{
			stime = mm+":0"+ss;
		}
		else
		{
			stime= mm+":"+ss;
		}
		return stime;
	}
}
