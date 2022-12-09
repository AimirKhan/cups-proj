using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupsModel : CupsElements
{
	public FirebaseModel FirebaseModel;
	private const string url = "remote_url";
	

	// private bool IsURLValid = false;

	public string RemoteAppURL
	{
		get
		{
			return PlayerPrefsHelper.GetString(url, "");
		}
		set
		{
			PlayerPrefsHelper.SetString(url, value);
		}
	}
}
