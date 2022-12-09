using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InternetAccess : MonoBehaviour
{
    [SerializeField] private string[] uris;

    public IEnumerator TestConnection(Action<bool> callback)
    {
        foreach (string uri in uris)
        {
            UnityWebRequest request = UnityWebRequest.Get(uri);
            yield return request.SendWebRequest();
            Debug.Log("{GameLog} => [InternetAccess] - TestConnection \n + uri: " + uri + "\n Network Error: " + request.result);

            if (request.result == UnityWebRequest.Result.Success)
            {
                callback(true);
                yield break;
            }
        }
        callback(false);
    }
}
