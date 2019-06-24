using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPanel : MonoBehaviour, IPanel
{
    public Text informationtext;

    // on Enable method
    public void OnEnable()
    {
        informationtext.text = UIManager.instance.ActiveCase.name;
    }

    public void ProcessInfo()
    {

    }
}
