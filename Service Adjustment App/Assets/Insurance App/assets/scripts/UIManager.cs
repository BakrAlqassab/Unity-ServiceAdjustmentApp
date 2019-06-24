using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

using System.IO;
public class UIManager : MonoBehaviour
{


    // WE CREATE THIS FOR ACCESS TO THE UIMANAGER , THAT WAHT CALLED SINGLTON Design Pattery which help us to access from every where

    // we will change from mono behavior to mono singelton , for that to start we need private static variable
    private static UIManager _Instance;

    // we will reate get to invite the private value and check it .
    public static UIManager instance
    {
        get
        {
            if (_Instance == null)
            {
                Debug.LogError("The UI Manager is NULL");
            }
            return _Instance;
        }

    }



    // if want create list of cases add the add brackets after the cases script name , and we will create a array of cases and filter them throught find the matching ID AND WEILL USE THE CASE Data :) 
    public Case ActiveCase;
    public ClientInfoPanel clientInfoPanel;
    public GameObject borderPanel;


    private void Awake()
    {
        _Instance = this;

    }

    public void createnewcase()
    {
        // this 
        //Case newCase = new Case();
        // or 
        ActiveCase = new Case();

        // generate Case ID 
        // between 0 and 999 
        //assign it in active Case ID 
        int randomcaseid = Random.Range(0, 1000);
        ActiveCase.caseId = "" + randomcaseid;


        //  ActiveCase.photoNotes = " test attststs";


        //invite the scripts from here hep to update the CaseID when create new Case  
        clientInfoPanel.gameObject.SetActive(true);
        borderPanel.SetActive(true);
    }

    public void SubmitButton()
    {
        // create a new case to save

        //populate the case data

        // open a data stream to turn that object ( case ) into a file 
        // begin a aws process 

        Case awscase = new Case();
        awscase.caseId = ActiveCase.caseId;
        awscase.name = ActiveCase.name;
        awscase.date = ActiveCase.date;
        //awscase.location = ActiveCase.location;
        awscase.locationNotes = ActiveCase.locationNotes;
        awscase.phototaken = ActiveCase.phototaken;
        awscase.photoNotes = ActiveCase.photoNotes;

        BinaryFormatter bf = new BinaryFormatter();
        string filepath = Application.persistentDataPath + "/case#" + awscase.caseId + ".dat";
        FileStream file = File.Create(filepath);
        bf.Serialize(file, awscase);
        file.Close();

        Debug.Log(" application data path " + Application.persistentDataPath);

        //Send to AWS
        // o make it inside all the path then use only the filepath else add another string contain only id 
        AWSManager.instance.UploadToS3(filepath, awscase.caseId);
        //overviewpanel.instance.CaseNumberTitle.text = filepath;
    }


}
