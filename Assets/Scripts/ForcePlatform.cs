using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePlatform : MonoBehaviour
{
    public float force = 5;
    
    public bool destroyAfterUse = false;
    Vector3 dir;

    void Start() 
    {
        dir = this.transform.up;
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            if(other.gameObject.GetComponent<Rigidbody>() != null){
                other.gameObject.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);
                if(destroyAfterUse){
                    Destroy(this.gameObject, 0.25f);
                }
            }
        }
    }
}
