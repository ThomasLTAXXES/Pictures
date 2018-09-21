using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Http;

namespace Pictures
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            List<string> objectIds = new List<string>();
            string json = File.ReadAllText(@"c:/temp/UsersFromAD.txt");
            dynamic objects = JsonConvert.DeserializeObject<dynamic>(json);
            foreach (dynamic personObject in objects.value)
            {
                objectIds.Add(personObject.objectId.ToString());
                string pictureUrl = string.Format("https://graph.microsoft.com/v1.0/users/{0}/photo/$value", personObject.objectId.ToString());
                
                // uncomment  for testing
                //string pictureUrl = "https://graph.microsoft.com/v1.0/users/9b95b4f8-9947-4647-824e-6f8e6ce22ab3/photo/$value";
                using (WebClient client = new WebClient())
                {
                    //to do this;
                    // go to url; https://developer.microsoft.com/en-us/graph/graph-explorer
                    // log in, do a request (e.g. https://graph.microsoft.com/v1.0/users/9b95b4f8-9947-4647-824e-6f8e6ce22ab3/photo/$value
                    // track the request and copy the bearer token
                    try
                    {
                        client.Headers.Add("Accept", "application/json, text/plain, */*");
                        client.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                        client.Headers.Add("Accept-Language", "nl-NL,nl;q=0.9,en-US;q=0.8,en;q=0.7");
                        client.Headers.Add("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJub25jZSI6IkFRQUJBQUFBQUFEWHpaM2lmci1HUmJEVDQ1ek5TRUZFanl4bkpzVGQzWENXSEhmM1ZWNDlLZWM2SURLV0pHV3VhNmIzRWRmQkp6c1A0ekVjVGp5VGVKVDhscmo2UzVNajRDNC1iVUpKWS1CYWkwOXRRdTNGTkNBQSIsImFsZyI6IlJTMjU2IiwieDV0IjoiaTZsR2szRlp6eFJjVWIyQzNuRVE3c3lISmxZIiwia2lkIjoiaTZsR2szRlp6eFJjVWIyQzNuRVE3c3lISmxZIn0.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9lZGQxYzNiNi1iZTg3LTQzYmItOTJkNC03YTkxMWM1Y2VlMTcvIiwiaWF0IjoxNTM3NDU1MDg1LCJuYmYiOjE1Mzc0NTUwODUsImV4cCI6MTUzNzQ1ODk4NSwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFWUUFxLzhJQUFBQTNFRWsrNDZLVTgrUWt2cXZoSWhVMGFGSmNETkVSZEgyVmt5ZWE2ZENmbGxTVFJSbUhvQkVwRFB0OTRqWUxvNzltSVVMb2RIci9HLytrWTFSWlR1VVNWYnNyWktHR0hOQ1lpQ0x2c3BVNmxZPSIsImFtciI6WyJ3aWEiLCJtZmEiXSwiYXBwX2Rpc3BsYXluYW1lIjoiR3JhcGggZXhwbG9yZXIiLCJhcHBpZCI6ImRlOGJjOGI1LWQ5ZjktNDhiMS1hOGFkLWI3NDhkYTcyNTA2NCIsImFwcGlkYWNyIjoiMCIsImZhbWlseV9uYW1lIjoiTGVmZXZlci1UZXVnaGVscyIsImdpdmVuX25hbWUiOiJUaG9tYXMiLCJpcGFkZHIiOiI4NC4xOTkuMjQ4LjQ3IiwibmFtZSI6IlRob21hcyBMZWZldmVyLVRldWdoZWxzIiwib2lkIjoiMDM0MDFlYzMtZDQ5Ny00NjlhLWI4MDMtMDkyMDE3NTIwODE4Iiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTI5MTU3OTMwNzItMzg4Mzg2ODYxNy0yNTE5OTgzMTkzLTI3NzUiLCJwbGF0ZiI6IjMiLCJwdWlkIjoiMTAwM0JGRkRBRDE1RTQ3OSIsInNjcCI6IkNhbGVuZGFycy5SZWFkV3JpdGUgQ29udGFjdHMuUmVhZFdyaXRlIEZpbGVzLlJlYWRXcml0ZS5BbGwgTWFpbC5SZWFkV3JpdGUgTm90ZXMuUmVhZFdyaXRlLkFsbCBvcGVuaWQgUGVvcGxlLlJlYWQgcHJvZmlsZSBTaXRlcy5SZWFkV3JpdGUuQWxsIFRhc2tzLlJlYWRXcml0ZSBVc2VyLlJlYWRCYXNpYy5BbGwgVXNlci5SZWFkV3JpdGUgZW1haWwiLCJzdWIiOiJmZDc2S3Fvd08zNXFxMGwtbkxXN255STNCM29DR3dkYXE1YmwwRnkxWTA0IiwidGlkIjoiZWRkMWMzYjYtYmU4Ny00M2JiLTkyZDQtN2E5MTFjNWNlZTE3IiwidW5pcXVlX25hbWUiOiJUaG9tYXMuTGVmZXZlci1UZXVnaGVsc0BheHhlcy5jb20iLCJ1cG4iOiJUaG9tYXMuTGVmZXZlci1UZXVnaGVsc0BheHhlcy5jb20iLCJ1dGkiOiJ2NzZ1VmZkMzBFNnFFOVRpUGVnakFBIiwidmVyIjoiMS4wIiwieG1zX3N0Ijp7InN1YiI6Im80bW1TX3Zsa2duc2J0MWQzcXh0NVFERFlkaXczVGtjWnF5UmhiX3llLTgifSwieG1zX3RjZHQiOiIxNDczOTI1MjM3In0.CJ1jvKi96d_0bhbHTJSdTOg-q4zM9hSpl7GkI0KiXsCIHLdbFUfQTJcYZ6lorIKvJraiZW2pMid_gIv62qhUDr-8OHw15b9vSWOrs-cE5cQfQz1-x_9DO02sKGLlnoilCouW3OH4a4Om5mY_dg4vRfNGnSsdFUO4SG5FddvNm_OkMbR8THwW3w1IzGeTTDWRouV03W-gbJOe-vqeyL8tq456luowNsziJ4k8MgjG0Dqs6id3mAr6C_Ll6PKSSSNDFJdIuQN1O9YEMXMEL1lfmXKydp_yDBxmlhIPUDRsf6FGGheVO33fuOc7_755O6aByzkeBCugVJLENdRn0fVJnA");
                        //CHANGE THE BEARER TOKEN WHEN RE-RUNNING THIS
                        client.DownloadFile(new Uri(pictureUrl), string.Format(@"C:\temp\pictures\{0}.jpg", personObject.objectId.ToString()));
                    }
                    catch(Exception ex)
                    {
                        // Currently we are ignoring it because not everyone has a picture
                    }
                    
                }
            }
        }
    }
}
