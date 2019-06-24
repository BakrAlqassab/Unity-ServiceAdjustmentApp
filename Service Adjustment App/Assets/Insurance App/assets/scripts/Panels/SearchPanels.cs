using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// IPanel is interface name come from  IPanel.cs script and ca implementinterfaces as you want just add , 
public class SearchPanels : MonoBehaviour , IPanel
{
    public InputField CaseNumber;
    public SelectPanel selectpanel;
           
    public void ProcessInfo()
    {

        // Download list of all object inside the s3 storage

        // here will use something name Lambda LAMBDA

        AWSManager.instance.getlist(CaseNumber.text, () => {
            selectpanel.gameObject.SetActive(true);
        });
          
        // Compare those to caseNumber Input by user
       // if we fid a match
       //download that object 
    }
}
