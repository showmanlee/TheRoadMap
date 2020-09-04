using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public Sprite[] m_Image = new Sprite[2];

    float timer = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        transform.GetChild(0).GetComponent<Image>().sprite = m_Image[(int)(timer * 2 % 2)];
        print((int)(timer * 2 % 2));

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.W))
            Destroy(gameObject);
	}
}
