using UnityEngine;

public class FirebaseModel : CupsModel
{
	[SerializeField] private string[] emuNameDevices;

	[ContextMenu("Test/ShowSystemInfo")]
	public bool IsEmulator()
	{
		if (Debug.isDebugBuild) { return false; }

		var phoneModel = SystemInfo.deviceModel;
		Debug.Log("Phone model: " + phoneModel);
		var buildType = SystemInfo.deviceType;
		Debug.Log("buildType: " + buildType);

		var result = false;
		foreach (var device in emuNameDevices)
		{
			result = device.Contains(phoneModel);
			if (result) break;
		}

		// if (result) return true;
		Debug.Log("Is Emulator: " + result);
		return result;
		// TODO manufacturer, brand, platform
	}

	[ContextMenu("Test/Get Sim Info")]
	public bool IsSimInserted()
	{
		AndroidJavaObject TM = new AndroidJavaObject("android.telephony.TelephonyManager");
		string reg = TM.Call<string>("getSimCountryIso");
		return reg != "";
	}
}
