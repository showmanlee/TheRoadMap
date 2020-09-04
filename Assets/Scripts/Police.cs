using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour {

    public GameObject m_Red;
    public GameObject m_Blue;
    float f = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        f += Time.deltaTime;
        m_Red.GetComponent<Light>().intensity = (f % 2 > 1) ? f % 1 : 1 - (f % 1);
        m_Blue.GetComponent<Light>().intensity = (f % 2 > 1) ? 1 - (f % 1) : f % 1;
    }
}
