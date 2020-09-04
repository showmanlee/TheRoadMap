using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossLight : MonoBehaviour {

    public GameObject m_RedLight;
    public GameObject m_YellowLight;
    public GameObject m_GreenLight;

    float timer = 0;
    bool isYellow = false;
    bool isRed = false;
	// Use this for initialization
	void Start () {
        m_RedLight.SetActive(false);
        m_YellowLight.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (isYellow) {
            timer -= Time.deltaTime;
            if (timer < 0) {
                m_RedLight.SetActive(true);
                m_YellowLight.SetActive(false);
                isYellow = false;
                isRed = true;
                timer = 10;
            }
        }
		if (isRed) {
            timer -= Time.deltaTime;
            if (timer < 0) {
                m_GreenLight.SetActive(true);
                m_RedLight.SetActive(false);
                isRed = false;
            }
        }
	}

    public void EnterTrigger() {
        isYellow = true;
        timer = 2;
        m_YellowLight.SetActive(true);
        m_GreenLight.SetActive(false);
    }

    public void nowYellow(out bool result) {
        result = m_YellowLight.activeSelf;
    }
    public void nowRed(out bool result) {
        result = m_GreenLight.activeSelf;
    }
}
