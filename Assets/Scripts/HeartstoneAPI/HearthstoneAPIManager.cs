using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using static System.Net.WebRequestMethods;

public class HearthstoneAPIManager : MonoBehaviour
{

    [SerializeField] private Image _cardImage;
    [SerializeField] private TextMeshProUGUI _name;
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

    private void Start()
    {
        this.CreateCard();
    }

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

                List<MinionData> response = JsonConvert.DeserializeObject<List<MinionData>>(request.downloadHandler.text);

                //MinionData test = response[0];
                //Debug.Log("name: " + test.Name);
                Debug.Log("size: " + response.Count);
                Clean(response);
                Debug.Log("size after cleaning: " + response.Count);
                int rng = randomIndex(response);

                string cardID = response[rng].CardId;


                this._name.text = response[rng].Name;
                this._hp.text = response[rng].Health.ToString();
                this._atk.text = response[rng].Attack.ToString();

                this.StartCoroutine(this.DownloadTexture(cardID));

             
        
            }
            else
            {
                Debug.Log(request.error);
            }
        }

        yield return null;
    }


    private IEnumerator DownloadTexture(string cardID)
    {
      
        string url = _baseTextureURL + cardID + ".png";

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError ||
           request.result != UnityWebRequest.Result.ProtocolError)
        {
            DownloadHandlerTexture response = (DownloadHandlerTexture)request.downloadHandler;
            Texture texture = response.texture;

            _cardImage.sprite = Sprite.Create(response.texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            
        }
        else
        {
            Debug.Log(request.error);
        }

    }


    private void Clean(List<MinionData> response)
    {
       // int index;
        for (int i = 0; i < response.Count; i++)
        {
            if (response[i].Type != "Minion")
            {
                response.RemoveAt(i);
                i = 0;
            }
        }

        if (response[0].Type != "Minion") 
            response.RemoveAt(0);
    }
    //7 minions
    private int randomIndex(List<MinionData> response)
    {
        return Random.Range(0, response.Count);
    }


}
