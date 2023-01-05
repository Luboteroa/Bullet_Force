using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Player;

namespace JsonUtilities
{
    public class HttpClient : MonoBehaviour
    {
        public TextMeshProUGUI textComponent;
        public string apiUrl = "https://dummyjson.com/users/search?q=John";

        void Start()
        {
            StartCoroutine(GetRequest(apiUrl));
        }

        IEnumerator GetRequest(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                if (webRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(pages[page] + ": Error: " + webRequest.error);
                }
                else
                {
                    // Parse the JSON response
                    ResponseObject response = JsonUtility.FromJson<ResponseObject>(webRequest.downloadHandler.text);

                    // Access the data in the response object
                    string data = "";
                    foreach(User user in response.users)
                    {
                        data += user.firstName + "\n\n" + 
                                user.lastName + "\n\n" + 
                                user.university + "\n\n" + 
                                user.company.department;
                    }

                    // Set the text of the TMP text component
                    textComponent.text = data;
                }
            }
        }
    }
}