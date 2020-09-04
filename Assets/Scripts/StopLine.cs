using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopLine : MonoBehaviour {

    public GameObject m_SignRed;
    public GameObject m_GameManager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        if (m_SignRed.activeSelf && collision.gameObject.name == "TheCar") {
            print("Illigal! Illigal!");
            m_GameManager.SendMessage("DidSignMissed");
        }
    }
}
