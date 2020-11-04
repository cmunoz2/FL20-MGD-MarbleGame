using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GetTexture : MonoBehaviour
{
    public string url;
    // put ?raw=true at the end of a url to ensure it pulls up the image, instead of the URL itself

    public Material fallbackMat;

    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
                this.GetComponent<Renderer>().material = fallbackMat;
            }
            else
            {
                // Get downloaded asset bundle
                var texture = DownloadHandlerTexture.GetContent(uwr);
                this.GetComponent<Renderer>().material.mainTexture = texture;
            }
        }
    }
}