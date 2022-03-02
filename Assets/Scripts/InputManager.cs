using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public Gun gun;         

    public bool debug = false;   //make true to display debug msgs

    void Start() {
        if(debug) Debug.Log("Input manager is starting!");
    }

    // Update is called once per frame
    void Update()
    {
        var mouse = Mouse.current;
        if(mouse == null) return;

        if(mouse.leftButton.isPressed) {                //mouse is held down
            if(debug) Debug.Log("Left Mouse Button was pressed this frame");
            
            if(gun != null) {
                gun.Fire();             //calling the gun method on gun variable
            } else {
                if(debug) Debug.Log("There is no gun to fire");
            }
        }

        var keyboard = Keyboard.current;
        if(keyboard == null) return;

        if(keyboard.rKey.wasPressedThisFrame) {
            if(gun != null) {
                gun.Reload();
            }
        }

    }
}
