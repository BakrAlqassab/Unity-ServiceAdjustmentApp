using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoctionPanel : MonoBehaviour, IPanel
{
    public Text CaseNumberText;
    public RawImage mapImg;
    public InputField mapNotes;

    // for Map first step Hold the api key

    public string apikey;

    public float xCord;
    public float yCord;
    public int zoom;
    public int imgSize;
    public string url = "https://maps.googleapis.com/maps/api/staticmap?";

    IEnumerator Start()
    {
        // Fetch GEO Data
        //  if (Input.location.isEnabledByUser == true)
        if (!Input.location.isEnabledByUser)
        {
            Input.compass.enabled = true;

        }

            //Input.compass.enabled = true;
        
        // Input.compass.enabled = true;

        // Start Service 
        Input.location.Start();
        int maxwait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxwait > 0)
        {
            yield return new WaitForSeconds(1.0f);
            maxwait--;
            Debug.Log("noooooooppppppppe");
        }

        if (maxwait < 1)
        {
            Debug.Log("Timed Out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {

            Debug.Log("Unable to determine deveice location .... ");
        }
        else
        {
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            xCord = Input.location.lastData.latitude;

            yCord = Input.location.lastData.longitude;
            CaseNumberText.text = "Case Number : 00000  " + xCord + "'" + yCord;


        }

        Input.location.Stop();


    
        
        StartCoroutine(getlocationroutine());


    }

    public void  OnEnable()
    {
      
        // that legend : '-'
         CaseNumberText.text = "Case Number : 00000" + UIManager.instance.ActiveCase.caseId;

       // CaseNumberText.text = "Case Number : 00000  " + xCord + "'" + yCord;


    }
    // IEnumerator
    IEnumerator getlocationroutine ()
    {

        // construect appropriate url
        url = url + "center=" + xCord + "," + yCord + "&zoom=" + zoom + "&size=" + imgSize + "x" + imgSize + "&key=" + apikey;
        
        using (WWW map = new WWW(url))
        {
            yield return map;

            if (map.error != null)
            {
                Debug.LogError("map Error :" + map.error);

            }
            mapImg.texture = map.texture;

        }
    }
    public void ProcessInfo()
    {
       
        if (string.IsNullOrEmpty(mapNotes.text) ==false)
        {

            UIManager.instance.ActiveCase.locationNotes = mapNotes.text;
        }
    }
}
