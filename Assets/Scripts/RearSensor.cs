using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RearSensor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Contains("Vehicle")) {
            other.gameObject.SendMessage("Stop");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Contains("Vehicle")) {
            other.gameObject.SendMessage("Move");
        }
    }
}
