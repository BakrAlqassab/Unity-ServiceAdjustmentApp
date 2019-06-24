using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class takephotopanel : MonoBehaviour ,IPanel
{

    public Text CaseNumberText;
    public RawImage photoTaken;
    public InputField PhotoNotes;

    public GameObject overviewPanel;
    public GameObject borderPanel;

    private string imagepath;
    public void takephotoButton()
    {
        TakePicture(512);
    }
    void OnEnable()
    {

        // that legend : '-'
        CaseNumberText.text = "Case Number : 00000" + UIManager.instance.ActiveCase.caseId;

      //  CaseNumberText.text = "Case Number : 00000" + UIManager.instance.ActiveCase.caseId;
     

    }
  


    // Need to download from assest store NATIVE CAMERA BY SULYMAN YASIR
    // https://github.com/yasirkula/UnityNativeCamera A nd follow this link inistruction on the READ ME File 
    private void TakePicture(int maxSize)
    {
        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
        {
           
            Debug.Log("Image path: " + path);
            if (path != null)
            {
               
                // Create a Texture2D from the captured image
                Texture2D texture = NativeCamera.LoadImageAtPath(path, maxSize ,false);
                if (texture == null)
                {
                    CaseNumberText.text = "Couldn't load texture from " + path;
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                // here put the taken picture inside the raw image 
                photoTaken.texture = texture;
                photoTaken.gameObject.SetActive( true);

                imagepath = path;
               // CaseNumberText.text = "Image path: " + imagepath;
            }
        }, maxSize);

        Debug.Log("Permission result: " + permission);
    }

    public void ProcessInfo()
    {


        //Create a 2D Texture
        // and then to Apply the texture from image Path
        // encode to PNG
        // store bytes to photo taken of the active case 

        // false to make the texture is readable 0
        byte[] imgData = null;
        if (string.IsNullOrEmpty(imagepath) ==false)
            {
            Texture2D img = NativeCamera.LoadImageAtPath(imagepath, 512, false);
          imgData = img.EncodeToPNG();

        }

        UIManager.instance.ActiveCase.phototaken = imgData;
        //Texture2D convertedPhoto = photoTaken.texture as Texture2D;        
        //byte[] imgData = convertedPhoto.EncodeToPNG();



        if (string.IsNullOrEmpty(PhotoNotes.text) == false)
        {

            UIManager.instance.ActiveCase.photoNotes = PhotoNotes.text;

        }
        

        

        overviewPanel.SetActive(true);
        borderPanel.SetActive(true);



    }

}
