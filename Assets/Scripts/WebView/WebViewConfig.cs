using Gpm.Common;
using Gpm.WebView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Application;

public class WebViewConfig : MonoBehaviour
{
	// FullScreen
	public void ShowUrlFullScreen()
	{
		GpmWebView.ShowUrl(
			"https://google.com/",
			new GpmWebViewRequest.Configuration()
			{
				style = GpmWebViewStyle.FULLSCREEN,
				orientation = GpmOrientation.PORTRAIT,
				isClearCookie = false,
				isClearCache = false,
				isNavigationBarVisible = false,
				navigationBarColor = "#4B96E6",
				title = "Need no title",
				isBackButtonVisible = true,
				isForwardButtonVisible = true,
				supportMultipleWindows = false,
#if UNITY_IOS
            contentMode = GpmWebViewContentMode.MOBILE
#endif
			},
			OnCallback,
			new List<string>()
			{
			"USER_CUSTOM_SCHEME"
			});
	}

	private void OnCallback(
		GpmWebViewCallback.CallbackType callbackType,
		string data,
		GpmWebViewError error)
	{
		Debug.Log("OnCallback: " + callbackType);
		switch (callbackType)
		{
			case GpmWebViewCallback.CallbackType.Open:
				if (error != null)
				{
					Debug.LogFormat("Fail to open WebView. Error:{0}", error);
				}
				break;

			case GpmWebViewCallback.CallbackType.Close:
				if (error != null)
				{
					Debug.LogFormat("Fail to close WebView. Error:{0}", error);
				}
				break;
			case GpmWebViewCallback.CallbackType.PageLoad:
				if (string.IsNullOrEmpty(data) == false)
				{
					Debug.LogFormat("Loaded Page:{0}", data);
				}
				break;
			case GpmWebViewCallback.CallbackType.MultiWindowOpen:
				Debug.Log("MultiWindowOpen");
				break;
			case GpmWebViewCallback.CallbackType.MultiWindowClose:
				Debug.Log("MultiWindowClose");
				break;
			case GpmWebViewCallback.CallbackType.Scheme:
				if (error == null)
				{
					if (data.Equals("USER_CUSTOM_SCHEME") == true || data.Contains("CUSTOM_SCHEME") == true)
					{
						Debug.Log(string.Format("scheme:{0}", data));
					}
				}
				else
				{
					Debug.Log(string.Format("Fail to custom scheme. Error:{0}", error));
				}
				break;
			case GpmWebViewCallback.CallbackType.GoBack:
				Debug.Log("GoBack");
				break;
			case GpmWebViewCallback.CallbackType.GoForward:
				Debug.Log("GoForward");
				break;
		}
	}
}
