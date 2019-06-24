using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientInfoPanel : MonoBehaviour ,IPanel
{
    public Text CaseNumberText;
    public InputField firstname,lastname;

    // can be just GameObject but for be more consistince 
    public LoctionPanel locationpanel;

    public void OnEnable()
    { // that legend
        CaseNumberText.text = "Case Number : 00000" + UIManager.instance.ActiveCase.caseId;
    }
    public void ProcessInfo()
    {
        // Check if firstname or lastname are not empty
        if(string.IsNullOrEmpty(firstname.text) ||string.IsNullOrEmpty(lastname.text))
        {
            Debug.Log("Either firstname or lastname is Empty and we can't continue ");

        } else
        {
            UIManager.instance.ActiveCase.name = firstname.text + "  " + lastname.text;
            locationpanel.gameObject.SetActive(true);
        }
    }
}
