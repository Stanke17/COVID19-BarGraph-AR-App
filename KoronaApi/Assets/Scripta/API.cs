using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.UI;

public class API : MonoBehaviour
{

    public GameObject ItalyBAR;
    public GameObject ChinaBAR;
    public GameObject USABAR;
    public GameObject SpainBAR;
    public GameObject SerbiaBAR;
    
    private double x;
   

    private readonly string koronaUrl = "https://covid-193.p.rapidapi.com/statistics";
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(GetKorona());

        
        
        
        

    }
    
    
    IEnumerator GetKorona()
    {
        Debug.Log("Usao je u funkciju");
        UnityWebRequest koronaRequest = UnityWebRequest.Get(koronaUrl);
        koronaRequest.SetRequestHeader("x-rapidapi-host", "covid-193.p.rapidapi.com");
        koronaRequest.SetRequestHeader("x-rapidapi-key", "f63e44dbd1msh66349b0f21ebf0ap10d048jsnca1d31c58058");
       
       

        Debug.Log("Usao je ispod requsta");

        yield return koronaRequest.SendWebRequest();

        Debug.Log("Dosao je ispod sendWebRequest");

        if(koronaRequest.isNetworkError || koronaRequest.isHttpError)
        {
            Debug.Log("Greska ovde");
            Debug.Log(koronaRequest.error);
            yield break;
        }

        Debug.Log("Json");


        var jsonObject = JSON.Parse(koronaRequest.downloadHandler.text);

        // Debug.Log(jsonObject.Value);
        JSONNode koInfo = JSON.Parse(koronaRequest.downloadHandler.text);


        //var zemlja = koInfo["country"].Value;
        Debug.Log(koInfo[4][59][0].ToString());
        Debug.Log(koInfo[4][59][1][4].ToString());

        //Country 
        Country china = new Country();
        china.country = koInfo[4][0][0].ToString();
        china.total = int.Parse(koInfo[4][0][1][4].ToString());


        Country italy = new Country();
        italy.country = koInfo[4][1][0].ToString();
        italy.total = int.Parse(koInfo[4][1][1][4].ToString());

        Country spain = new Country();
        spain.country = koInfo[4][2][0].ToString();
        spain.total = int.Parse(koInfo[4][2][1][4].ToString());

        Country usa = new Country();
        usa.country = koInfo[4][3][0].ToString();
        usa.total = int.Parse(koInfo[4][3][1][4].ToString());

        Country serbia = new Country();
        serbia.country = koInfo[4][59][0].ToString();
        serbia.total = int.Parse(koInfo[4][59][1][4].ToString());

        Debug.Log("ime: " + italy.country + " Ukupan broj Slucajeva: " + italy.total);

        
       
        //Italy.Transform
        ItalyBAR.transform.localScale = new Vector3(0.05869833f, obrada(italy.total), 0.05869833f);
        ItalyBAR.transform.localPosition = new Vector3(0.246f, obrada(italy.total)/2, 0.177f);

        //China.Transform
        ChinaBAR.transform.localScale = new Vector3(0.05869834f, obrada(china.total), 0.05869834f);
        ChinaBAR.transform.localPosition = new Vector3(-0.282f, obrada(china.total) / 2, 0.177f);

        //Spain.Transform
        SpainBAR.transform.localScale = new Vector3(0.05869833f, obrada(spain.total), 0.05869833f);
        SpainBAR.transform.localPosition = new Vector3(0.08400001f, obrada(spain.total) / 2, 0.177f);

        //USA.Transform
        USABAR.transform.localScale = new Vector3(0.05869833f, obrada(usa.total), 0.05869833f);
        USABAR.transform.localPosition = new Vector3(-0.096f, obrada(usa.total) / 2, 0.177f);

        //Serbia.Transform
        SerbiaBAR.transform.localScale = new Vector3(0.05869833f, obrada(serbia.total), 0.05869833f);
        SerbiaBAR.transform.localPosition = new Vector3(0.417f, obrada(serbia.total) / 2, 0.177f);

        //Add total cases
        TextMeshPro italijaTxt = GameObject.FindGameObjectWithTag("ItalijaTxt").GetComponent<TextMeshPro>();
        TextMeshPro chinaTxt = GameObject.FindGameObjectWithTag("ChinaTxt").GetComponent<TextMeshPro>();
        TextMeshPro spainTxt = GameObject.FindGameObjectWithTag("SpainTxt").GetComponent<TextMeshPro>();
        TextMeshPro USATxt = GameObject.FindGameObjectWithTag("USATxt").GetComponent<TextMeshPro>();
        TextMeshPro SerbiaTxt = GameObject.FindGameObjectWithTag("SerbiaTxt").GetComponent<TextMeshPro>();

        italijaTxt.text = italy.total.ToString();
        
        chinaTxt.text = china.total.ToString();
        
        spainTxt.text = spain.total.ToString();
        
        USATxt.text = usa.total.ToString();
        
        SerbiaTxt.text = serbia.total.ToString();
        

        Debug.Log(serbia.total.ToString());

        
    }

    public float obrada(float total)
    {
        double x = ((total * 0.000001f) * 5);
        Debug.Log(x);
        float y = float.Parse(x.ToString());

        return y;
    }


    public class Country
    {
        public string country { get; set; }
        public int total { get; set; }
    }

    [System.Serializable]
    public class Cases
    {
        public string @new { get; set; }
        public int active { get; set; }
        public int critical { get; set; }
        public int recovered { get; set; }
        public int total { get; set; }
    }

    [System.Serializable]
    public class Deaths
    {
        public string @new { get; set; }
        public int total { get; set; }
    }

    [System.Serializable]
    public class RootObject
    {
        public string country { get; set; }
        public Cases cases { get; set; }
        public Deaths deaths { get; set; }
        public string day { get; set; }
        public Time time { get; set; }
    }

    
}
