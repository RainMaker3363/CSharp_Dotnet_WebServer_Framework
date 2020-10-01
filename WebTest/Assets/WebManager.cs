using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class GameResult
{
    public string UserName;
    public int Score;
}

public class WebManager : MonoBehaviour
{
    string _baseUrl = "https://localhost:44377/api";

    private Coroutine _coPostWebRequest = null;
    private Coroutine _coGetAllWebRequest = null;

    // Start is called before the first frame update
    void Start()
    {
        GameResult res = new GameResult()
        {
            UserName = "LeinMaker",
            Score = 777
        };
        
        SendPostRequest("ranking", res, (uwr) =>
        {
            Debug.Log($"TODO : UI 갱신하기");
        });

        SendGetAllRequest("ranking", (uwr) =>
        {
            Debug.Log($"TODO : UI 갱신하기");
        });
    }

    public void SendPostRequest(string url, object obj, Action<UnityWebRequest> callback)
    {
        if (_coPostWebRequest != null)
        {
            StopCoroutine(_coPostWebRequest);
            _coPostWebRequest = null;
        }

        _coPostWebRequest = StartCoroutine(CoSendWebRequest("ranking", "POST", obj, callback));
    }

    public void SendGetAllRequest(string url, Action<UnityWebRequest> callback)
    {
        if (_coGetAllWebRequest != null)
        {
            StopCoroutine(_coGetAllWebRequest);
            _coGetAllWebRequest = null;
        }

        _coGetAllWebRequest = StartCoroutine(CoSendWebRequest("ranking", "GET", null, callback));
    }


    IEnumerator CoSendWebRequest(string url, string method, object obj, Action<UnityWebRequest> callback)
    {
        yield return null;

        string sendUrl = $"{_baseUrl}/{url}/";
        byte[] jsonBytes = null;

        if(obj != null)
        {
            string jsonStr = JsonUtility.ToJson(obj);
            jsonBytes = Encoding.UTF8.GetBytes(jsonStr);
        }

        var uwr = new UnityWebRequest(sendUrl, method);
        uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if(uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.LogError(uwr.error);
        }
        else
        {
            Debug.Log("Recv " + uwr.downloadHandler.text);
            callback.Invoke(uwr);
        }
    }
}
