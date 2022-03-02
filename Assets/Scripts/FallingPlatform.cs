using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    
    public AnimationCurve curve;
    public float delayFallTime = 0.5f;
    public float resetInterval = 5;
    Color defaultColor = new Color(1f, 1f, 1f, 1f);
    Color fallingColor = new Color(1f, 0f, 0f, 1f);
    Rigidbody rb;
    Vector3 startPosition;
    Quaternion startRotation;
    bool platformIsActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        startPosition = this.transform.position;
        startRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log(other.name + " has jumped on us.");
        if(other.gameObject.CompareTag("Player")) { 
            StartCoroutine(WaitToFall());
        }
    }
    
    //Coroutine to make the cube wait before it falls
    IEnumerator WaitToFall() {
        if(!platformIsActive) {
            platformIsActive = true;
            this.GetComponent<Renderer>().material.color = fallingColor;
            yield return new WaitForSeconds(delayFallTime);
            rb.isKinematic = false;
            StartCoroutine(ResetPlatform());
        }
    }
    //Coroutine to make the cube reset position
    IEnumerator ResetPlatform() {
        yield return new WaitForSeconds(3);
        rb.isKinematic = true;                  //stop falling

        Vector3 pointB = startPosition;
        Vector3 pointA = this.transform.position;

        Quaternion rotA = this.transform.rotation;
        Quaternion rotB = startRotation;

        float timer = 0;

        while(timer < 1) {
            this.transform.position = Vector3.Lerp(pointA, pointB, curve.Evaluate(timer));  // position
            this.transform.rotation = Quaternion.Lerp(rotA, rotB, curve.Evaluate(timer));   // rotation
            timer += Time.deltaTime / resetInterval;
            yield return null;
        }
        //just in case its not exactly where it started after resetting
        this.transform.position = startPosition;
        this.transform.rotation = startRotation;
        this.GetComponent<Renderer>().material.color = defaultColor;
        platformIsActive = false;
    }
}
