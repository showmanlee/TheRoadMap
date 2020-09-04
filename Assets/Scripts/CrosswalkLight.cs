using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosswalkLight : MonoBehaviour {


    public GameObject m_NearbyRedLight;
    public GameObject m_NearbyYellowLight;
    public GameObject m_RedLight;
    public GameObject m_GreenLight;
    public bool isInverted;

    bool isWorking = false;
    float timer = 0;
    float wating = 0;
    // Use this for initialization
    void Start() {
        m_RedLight.SetActive(!isInverted);
        m_GreenLight.SetActive(isInverted);
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (isInverted) {
            if (!m_NearbyRedLight.activeSelf) {
                m_RedLight.SetActive(false);
                if (isWorking)
                    m_GreenLight.SetActive(true);
                else
                    Blinking();
            }
            else {
                isWorking = true;
                m_RedLight.SetActive(true);
                m_GreenLight.SetActive(false);
            }
        }
        else {
            if (!m_NearbyRedLight.activeSelf) {
                m_RedLight.SetActive(true);
                m_GreenLight.SetActive(false);
                wating = 0;
            }
            else {
                wating += Time.deltaTime;
                m_RedLight.SetActive(false);
                if (wating > 5)
                    Blinking();
                else
                    m_GreenLight.SetActive(true);
            }
        }
    }

    void Blinking() {
        m_GreenLight.SetActive((timer % 1 > 0.5f) ? true : false);
    }

    public void isGreen(out bool result) {
        result = m_GreenLight.activeSelf;
    }
}
