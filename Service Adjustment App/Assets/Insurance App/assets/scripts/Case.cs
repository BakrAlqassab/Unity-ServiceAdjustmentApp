using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// that allow us to se this class in the inespector

    // in order want tell unity we can turn it to a file then we nned to use System.Serializeable
[System.Serializable]
public class Case 
{
    public string caseId;
    public string name;
    public string date;
  public string location;// don't need it here because we don't save that image or can use it for longtitude and lintitude 
    public string locationNotes;
    // INVITE UI BY Add " using UnityEngine.UI " in the up  :) :) :) '-' '-'  ; 

        // we will make the picture save as byte to save it in file 
    public byte[] phototaken;
    public string photoNotes;

    




}