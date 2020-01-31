using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchManager : MonoBehaviour
{
    // Switch Functions
    void manageSwitch01(){
      RotateCatwalk();
      ToggleSaws();
    }

    void manageSwitch02(){
      ToggleSaws();
      ToggleFlames();
      ToggleSpikes();
    }

    void manageSwitch03(){
      ToggleFlames();
      TogglePlatforms();
    }

    // Separate Action Functions
    public void RotateCatwalk() {
      Debug.Log ("RotateCatwalk()");
    }

    public void ToggleFlames() {
      Debug.Log ("ToggleFlames()");
    }

    public void TogglePlatforms() {
      Debug.Log ("TogglePlatforms()");
    }

    public void ToggleSaws() {
      Debug.Log ("ToggleSaws()");
    }

    public void ToggleSpikes() {
      Debug.Log ("ToggleSpikes()");
    }


    public void Update(){
      if(Input.GetKey(KeyCode.Alpha1)){
        manageSwitch01();
      }
      if(Input.GetKey(KeyCode.Alpha2)){
        manageSwitch02();
      }
      if(Input.GetKey(KeyCode.Alpha3)){
        manageSwitch03();
      }
    }
}
