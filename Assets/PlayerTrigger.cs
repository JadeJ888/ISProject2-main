using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public Gun gun;

    void Start() {
        if(gun == null) {
            Debug.LogError("Forgot to assign gun script here.");
            
        }
    }

   void OnTriggerEnter(Collider other) {
       if(other.gameObject.CompareTag("AmmoPickup")) {
           // call the gun.GetAmmo() function
           gun.GetAmmo();
           Destroy(other.gameObject);
       }
   }
}
