using Firebase;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class FirebaseController : CupsElements
{
	DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
	protected bool isFirebaseInitialized = false;

	void Start()
	{
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
			dependencyStatus = task.Result;
			if (dependencyStatus == DependencyStatus.Available)
			{
				InitializeFirebase();
			}
			else
			{
				Debug.LogError(
				  "Could not resolve all Firebase dependencies: " + dependencyStatus);
			}
		});
	}

	// Initialize remote config, and set the default values.
	void InitializeFirebase()
	{
		// [START set_defaults]
		Dictionary<string, object> defaults = new()
		{
			// These are the values that are used if we haven't fetched data from the
			// server
			// yet, or if we ask for values that the server doesn't have:
			{ "remote_url", "" }
		};

		FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
		  .ContinueWithOnMainThread(task =>
		  {
			  // [END set_defaults]
			  Debug.Log("RemoteConfig configured and ready!");
			  isFirebaseInitialized = true;
		  });
	}

	[ContextMenu("Test/FetchFirebase")]
	public void FetchFirebase()
	{
		FetchDataAsync();
	}

	public void ShowData()
	{
		Debug.Log("remote_url: " + FirebaseRemoteConfig.DefaultInstance
			   .GetValue("remote_url").StringValue);
	}

	// [START fetch_async]
	public Task FetchDataAsync()
	{
		Debug.Log("Fetching data...");
		Task fetchTask =
		FirebaseRemoteConfig.DefaultInstance.FetchAsync(
			TimeSpan.Zero);
		return fetchTask.ContinueWithOnMainThread(FetchComplete);
	}
	//[END fetch_async]

	void FetchComplete(Task fetchTask)
	{
		if (fetchTask.IsCanceled)
		{
			Debug.Log("Fetch canceled.");
		}
		else if (fetchTask.IsFaulted)
		{
			Debug.Log("Fetch encountered an error.");
		}
		else if (fetchTask.IsCompleted)
		{
			Debug.Log("Fetch completed successfully!");
		}

		var info = FirebaseRemoteConfig.DefaultInstance.Info;
		switch (info.LastFetchStatus)
		{
			case LastFetchStatus.Success:
				FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
				.ContinueWithOnMainThread(task => {
					Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
								   info.FetchTime));
				});

				break;
			case LastFetchStatus.Failure:
				switch (info.LastFetchFailureReason)
				{
					case FetchFailureReason.Error:
						Debug.Log("Fetch failed for unknown reason");
						break;
					case FetchFailureReason.Throttled:
						Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
						break;
				}
				break;
			case LastFetchStatus.Pending:
				Debug.Log("Latest Fetch call still pending.");
				break;
		}
	}

	[ContextMenu("Test/LoadURLData")]
	public void GetRemoteData()
	{
		Debug.Log("Get URL Data... ");
		FetchDataAsync();
		app.model.RemoteAppURL = FirebaseRemoteConfig.DefaultInstance.GetValue("remote_url").StringValue;
		Debug.Log("Show URL data: " + app.model.RemoteAppURL);
	}
}
