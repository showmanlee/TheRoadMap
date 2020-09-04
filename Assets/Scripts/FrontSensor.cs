using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontSensor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Contains("Human")) {
            other.gameObject.SendMessage("Wow");
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Contains("Human")) {
            other.gameObject.SendMessage("UnWow");
        }
    }
}
