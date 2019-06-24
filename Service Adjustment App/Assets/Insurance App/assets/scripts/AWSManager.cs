using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using System.IO;
using System;
using Amazon.S3.Util;
using System.Collections.Generic;
using Amazon.CognitoIdentity;
using Amazon;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
public class AWSManager : MonoBehaviour
{

    private static AWSManager _instance;
    public static AWSManager instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("AWS Manager is null");
               // overviewpanel.instance.CaseNumberTitle.text = "AWS Manager is null";
            }
            else
            {
                Debug.Log("alles gut :)");
               // overviewpanel.instance.CaseNumberTitle.text = "alles gut :)";
            }
            return _instance;
        }
    }
    public string S3Region = RegionEndpoint.USEast2.SystemName;
    private RegionEndpoint _S3Region
    {
        get { return RegionEndpoint.GetBySystemName(S3Region); }
    }
    private AmazonS3Client _S3Client;
    public AmazonS3Client S3Client
    {
        get
        {
            if (_S3Client == null)
            {
                _S3Client = new AmazonS3Client(new CognitoAWSCredentials(
        "us-east-2:d2f8c75d-d231-496d-a4ec-1a50920351fd", // Identity Pool ID

        RegionEndpoint.USEast2 // Region
                ), _S3Region);
            }
            return _S3Client;

        }
    }
    private void Awake()
    {
        _instance = this;
        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;
        // For obtain credentials
        // https://docs.aws.amazon.com/mobile/sdkforunity/developerguide/setup-unity.html
        // To obtain my identify Pool ID
        // https://us-east-2.console.aws.amazon.com/cognito/pool/edit/?region=us-east-2&id=us-east-2:d2f8c75d-d231-496d-a4ec-1a50920351fd

        //CognitoAWSCredentials credentials =


        // AmazonS3Client S3Client = new AmazonS3Client(credentials, _S3Region);

        // ResultText is a label used for displaying status information
        /*
        S3Client.ListBucketsAsync(new ListBucketsRequest(), (responseObject) =>
        {

            if (responseObject.Exception == null)
            {
                responseObject.Response.Buckets.ForEach((s3b) =>
                {
                    Debug.Log("Bucket Name : " + s3b.BucketName);
                });
            }
            else
            {
                Debug.Log("AWS ERROR : " + responseObject.Exception);
            }
        });
        */


    }


    public void UploadToS3(string path , string filename)
    {
       // overviewpanel.instance.CaseNumberTitle.text = "EHKA";
        FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        PostObjectRequest request = new PostObjectRequest()
        {
            Bucket = "mycasefilesb",
            //  Key =  Path.GetFileName(fileName),
            Key ="case#"+ filename+".dat",
            InputStream = stream,
            CannedACL = S3CannedACL.Private,
            Region = _S3Region
        };
         overviewpanel.instance.CaseNumberTitle.text = "EHKA";
        S3Client.PostObjectAsync(request, (responseObj) =>
        {
            overviewpanel.instance.CaseNumberTitle.text = "EHKA No ";
            if (responseObj.Exception == null)
             {
            //responseObj.Response.s3b.ForEach((obj) =>
            //        {
            //    Debug.Log(obj.Key);
            //});
               Debug.Log("Successfully posted to Bucket");

              //  overviewpanel.instance.CaseNumberTitle.text = "Successfully posted to Bucket";
                   SceneManager.LoadScene(0);
                //  overviewpanel.instance.CaseNumberTitle.text = "Successfully posted to Bucket";

            }
            else
              {
               Debug.Log("Exception occured during uploading :" + responseObj.Exception);
              // overviewpanel.instance.CaseNumberTitle.text = "badly  posted to Bucket";

                // Debug.Log("\n receieved error "+ responseObj.Response.HttpStatusCode.ToString());
             }
       });

    }

    // https://docs.aws.amazon.com/mobile/sdkforunity/developerguide/s3.html
    public void getlist(string casenumber ,  Action onComplete = null)
    {

        string target = "case#" + casenumber+".dat";
        var request = new ListObjectsRequest()
        {
            BucketName = "mycasefilesb"
        };

        S3Client.ListObjectsAsync(request, (responseObject) =>
         {
         if (responseObject.Exception == null)
         {

                 // the power of link is basically eliminated the long function to short 
                 bool casefound = responseObject.Response.S3Objects.Any(obj => obj.Key == target);

                 if(casefound == true)
                 {
                     Debug.Log("case File exist ");
                     S3Client.GetObjectAsync("mycasefilesb", target, (responseObj) =>
                     {
                         //read the data and apply it to case (Object) fie to use


                         //check if the response is null or not
                         if (responseObj.Response.ResponseStream != null)
                         {
                             // create a byte array to store data from file
                             byte[] data = null;

                             // use stream reader to read response data
                             using (StreamReader reader = new StreamReader(responseObj.Response.ResponseStream))
                             {
                                 //access a memory stream
                                 using (MemoryStream memory = new MemoryStream())
                                 {   // populate data byte array with memstream data
                                     var buffer = new byte[512];
                                     var bytesread = default(int);

                                     while ((bytesread = reader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
                                     {
                                         memory.Write(buffer, 0, bytesread);
                                     }
                                     data = memory.ToArray();
                                 }
                             }



                             // onverts the bytes to a case (Object)

                             using (MemoryStream memory = new MemoryStream(data))
                             {

                                 // now we need use the binary formatter to reconstracted , essentially  mean do theoposit os contracted data in UIManager 

                                 BinaryFormatter bf = new BinaryFormatter();
                                 //we pass the memory to Deserilaize because it filled with bytes 
                                 Case downloadedcase = (Case)bf.Deserialize(memory);
                                 Debug.Log("Downloaded case Name : " + downloadedcase.name);

                                 UIManager.instance.ActiveCase = downloadedcase;
                                 if(onComplete != null  )
                                 {// when the case id exist then will send true else will not active the onComplete
                                     Debug.Log("oncompöeteeee");
                                     onComplete();

                                 }
                                 else
                                 {
                                     Debug.Log("Valite ttavasti");
                                 }
                             }


                         }

                      


                     });
                 }
                 else
                 {
                     Debug.Log("Case file not exist ");
                    
                 }

                 // because there is no way to break the loop after found the file , then will change the foe each to linq  
                 //responseObject.Response.S3Objects.ForEach((obj) =>
                 //    {
                 //    // this will get called for every object
                 //    Debug.Log(obj.Key);

                 //    // if target == current obj.key
                 //    // we found our case file!
                 //    if(obj.Key == target)
                 //    {
                 //        Debug.Log("case File exist ");

                 //        // to stop looping after found the file type Break

                 //    } else
                 //    {
                 //        Debug.Log("valitettavassti Nooop! ");


                 //    }
                 //});
             }
             else
             {
                 Debug.Log("Error getting List of items from S3 :" + responseObject.Exception);
             }
         });
    }
}
