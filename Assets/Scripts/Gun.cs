using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawn;
    public Rigidbody bulletPrefab;
   
    [Range(10,100)]
    public float bulletForce = 50;
    public float fireRate = 0.15f;

    public bool debug = false; //set true to display debug msgs
    [Header("Audio")]
    public AudioClip fire, getAmmo;          //make one for reload, shoot, pick up ammo, out of bullets

    //private variables
    [Header("Ammo Management")]
    public int totalAmmo = 60;
    public int clipSize = 10;
    public int clip = 0;

    private AudioSource aud;                

    bool canShoot = true;

    

    void Start() {
        aud = this.gameObject.GetComponent<AudioSource>();
    }


    public void Reload() {                                      //I think I wanna add a delay later and an animation in the polish stage here
        if(clip == clipSize) {                                  //if I can figure that out
            if(debug) Debug.Log("Clip is already full.");
            return;
        }

        int partialClip = 0;
        if(clip > 0) partialClip = clip;    

        if(totalAmmo + clip >= clipSize) {       
            totalAmmo -= (clipSize - clip);       
            clip = clipSize;             
        } else {
            // throw the rest of the ammo into the clip
            clip = totalAmmo + clip;
            totalAmmo = 0;
        }  
    }

        //idk how to do rate of fire but I think it would be something like
        //if mousehelddown and allowFire is true then shoot
        //fire then set allowFire to false and after .1seconds set allowFire back to true
        //i just dont know how to code that in


    public void Fire() {            
        
        if(canShoot) {
                if(clip > 0) {
                if(debug) Debug.Log("Pow!");
                
                clip -= 1;
                // create a copy of the bullet prefab
                Rigidbody bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                // move the bullet in front of the gun
                bullet.transform.Translate(0,0,1);
                // add forward force to the gun
                bullet.AddRelativeForce(Vector3.forward * bulletForce, ForceMode.Impulse);
                StartCoroutine(Cooldown());
                aud.PlayOneShot(fire);                //PlayOneShot() will overlap sounds and .play with play once
            } else {
                if(debug) Debug.Log("Out of Ammo!");
            }
        }
    }

    public void GetAmmo() {
        totalAmmo += 90;
        aud.PlayOneShot(getAmmo);
    }

    IEnumerator Cooldown() {
        canShoot = false; 
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

}
