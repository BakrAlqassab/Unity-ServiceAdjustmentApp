using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class overviewpanel : MonoBehaviour ,IPanel 
{
    public Text CaseNumberTitle;
    public Text nameTitle;
    public Text dateTitle;
    public Text locationtitle;
    public Text locationnotes;
    public RawImage takenPhoto;
    public Text Photonotes;

    private static overviewpanel _instance;
    public static overviewpanel instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("overview Manager is null");
            }
            else
            {
                Debug.Log("alles gut overview  :)");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

    }
    public void ProcessInfo()
    {
    }

    public void OnEnable()
    {
        CaseNumberTitle.text = "Case Number : 00000" + UIManager.instance.ActiveCase.caseId;
      //  UIManager.instance.SubmitButton();

        nameTitle.text = UIManager.instance.ActiveCase.name;
        // from google look up how to get Datetime   / get today's date C#  and add using System 
        dateTitle.text = DateTime.Today.ToString();

         locationtitle.text = " Location  " + UIManager.instance.ActiveCase.location;
        locationnotes.text = " Location Notes " + UIManager.instance.ActiveCase.locationNotes;
        Photonotes.text = " PHOTO Notes : \n" +  UIManager.instance.ActiveCase.photoNotes;

        // we need to ReBuild the photo and dsiplay it 

        // convert bytes to png

        // convert  texture2D to texture 


        // tutorial 66 !!                           PICTURE CONVERT 
        Texture2D reconStructedImage = new Texture2D(1, 1);
        reconStructedImage.LoadImage(UIManager.instance.ActiveCase.phototaken);



        //Texture img = (Texture)reconStructedImage;
        takenPhoto.texture = (Texture)reconStructedImage;

     

        



    }

    // in submit Button Analytics , isearched in google about how can i activate events unity  by add analytics events Tracker " Component"  and add it to onClick
}
