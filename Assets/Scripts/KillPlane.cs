using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {                           //change this to reload the scene when player falls into killplane
        if(other.gameObject.CompareTag("Player")) {
            Debug.Log(other.transform.position);
            other.transform.position = new Vector3(0f,1.5f,0f);
            Debug.Log(other.transform.position);
        }
    }



}
