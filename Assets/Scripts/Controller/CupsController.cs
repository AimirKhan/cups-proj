using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupsController : CupsElements
{
	[SerializeField] private InternetAccess _internetAccess;
	[SerializeField] private FirebaseController _firebaseController;
	public void CheckLocalURL()
	{
		// 1. Is have local URL?
		if (app.model.RemoteAppURL != "")
		{
			// 1.1 local URL is presented
			StartCoroutine(_internetAccess.TestConnection(result =>
			{
				// 2. Is have Internet Access?
				if (result)
				{
					// 2.1 if have internet access
					//  4. Open LocalURL in WebView
					// app.model.RemoteAppURL;
				}
				else
				{
					// 2.2 if haven't internet access
					//TODO Show no internet access window
				}
			}));
		}
		else
		{
			// 1.2 local URL isn't presented
			// 3. Connect to Firebase Remote Config
			//TODO Условие для проверки ссылки и эмулятора
			// 3 Get Remote URL
			_firebaseController.GetRemoteData();
			if (app.model.RemoteAppURL == "" ||
				app.model.FirebaseModel.IsEmulator() ||
				!app.model.FirebaseModel.IsSimInserted())
			{
				// 3.1 Firebase link wrong or Device emulator or SIM isn't inserted
				// 5 Open cap (game or service)

			}
			else
			{
				// 3.2 Firebase link rigth or Device isn't emulator or SIM inserted
				// 3.3 Save Firebase Remote URL Link to local device
				//  4. Open LocalURL in WebView
				// app.model.RemoteAppURL;

			}

		}
	}
}
