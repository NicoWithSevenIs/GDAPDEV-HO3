using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class HearthstoneAPIManager : MonoBehaviour
{

    [SerializeField] private Image _cardImage;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private TextMeshProUGUI _hp;
    [SerializeField] private TextMeshProUGUI _atk;

    private string _baseURL = "https://omgvamp-hearthstone-v1.p.rapidapi.com/";
    private string _baseTextureURL = "https://art.hearthstonejson.com/v1/orig";
    private string _apiHost = "omgvamp-hearthstone-v1.p.rapidapi.com";
    private string _apiKey = "cc66aa1ac0msh508d60a6eb1d805p146115jsnbc631a6c0479";

    #region Singleton

    public static HearthstoneAPIManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    #endregion

    public void CreateCard()
    {
        this.StartCoroutine(this.RequestCard());
    }

    private IEnumerator RequestCard()
    {

        string url = this._baseURL + "cards/sets/Basic";


        Debug.Log("[URL] : " + url);


        using (UnityWebRequest request = new UnityWebRequest(url, "GET"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("X-RapidAPI-Key", this._apiKey);
            request.SetRequestHeader("X-RapidAPI-Host", this._apiHost);

            yield return request.SendWebRequest();

            Debug.Log($"Response CPDE : {request.responseCode}");

            if (string.IsNullOrEmpty(request.error))
            {
                Debug.Log("JSON Response " + request.downloadHandler.text);

                MinionData response = JsonConvert.DeserializeObject<MinionData>(request.downloadHandler.text);


            }
            else
            {
                Debug.Log(request.error);
            }
        }

        yield return null;
    }

    private IEnumerator DownloadTexture(string url, int i)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError ||
           request.result != UnityWebRequest.Result.ProtocolError)
        {
            DownloadHandlerTexture response = (DownloadHandlerTexture)request.downloadHandler;
            Texture texture = response.texture;
            //this._cardObject.GetComponent<MeshRenderer>().material.mainTexture = texture;
            //this._cardObject.GetComponent<Renderer>().material.mainTexture = texture;
        }
        else
        {
            Debug.Log(request.error);
        }
        yield return null;
    }

}
