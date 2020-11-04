using UnityEngine;
using System.Collections;

public class WWWTest : MonoBehaviour
{
    public string url;
    public Material fallBackMat;
    IEnumerator Start()
    {
        using (WWW www = new WWW(url))
        {
            yield return www;
            Renderer renderer = GetComponent<Renderer>();

            
            renderer.material.mainTexture = www.texture;
        }
    }
}
// URL V
// https://i.pinimg.com/474x/26/15/e6/2615e69bc4e9bf743c27cca6b873b32b.jpg