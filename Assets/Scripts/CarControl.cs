using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControl : MonoBehaviour {
    public GameObject m_CarBody;
    public GameObject m_Front;
    public GameObject m_Wheel;
    public GameObject m_SpeedDigit;
    public GameObject m_SpeedNiddle;
    public GameObject m_GameManager;
    public AudioClip m_IdleSound;
    public AudioClip m_RunSound;
    public bool OnDebug;

    public static float speed = 0;
    int gear = 1;
    float steering = 0;
    float rawSteering = 0;
    int level = 0;
    bool crashed;
    bool started;
    // Use this for initialization
    void Start() {
        speed = 0;
        crashed = false;
        started = false;
        m_GameManager.SendMessage("GetLevel", level);
        GetComponent<AudioSource>().Stop();
    }

    // Update is called once per frame
    void Update() {
        if (!crashed && started) {
            speeding();
            steer();
        }
    }

    void speeding() {
        // 속도 제어
        if (OnDebug) {
            if (Input.GetKey(KeyCode.W))          // 액셀
                speed += Time.deltaTime * (10 - gear * 2f) * ((200 - Mathf.Abs(steering)) / 200);
            else if (Input.GetKey(KeyCode.S))   // 브레이크 페달
                speed -= Time.deltaTime * 40;
            else                                // 엔진 브레이크
                speed -= Time.deltaTime * 5f * (1 + Mathf.Abs(steering) / 80);
        }
        else {
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))          // 액셀
                speed += Time.deltaTime * (16 - gear * 3.5f) * ((200 - Mathf.Abs(steering)) / 200);
            else if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad))   // 브레이크 페달
                speed -= Time.deltaTime * 40;
            else                                // 엔진 브레이크
                speed -= Time.deltaTime * 5f * (1 + Mathf.Abs(steering) / 80);
        }

        Vector3 axis = m_Front.transform.position - m_CarBody.transform.position;   // 정면 방향 설정
        if (speed < 0)
            speed = 0;
        if (speed > 120)    // 최대 속도: 120
            speed = 120;
        transform.localPosition += axis.normalized / 3.6f * Time.deltaTime * speed; // 이동

        if (speed < 1) {          // 엔진 소리
            GetComponent<AudioSource>().clip = m_IdleSound;
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
        }
        else {
            GetComponent<AudioSource>().clip = m_RunSound;
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().pitch = 0.3f + (2f / 120 * speed);
        }

        // 기어
        if (speed < 30)
            gear = 1;
        else if (speed < 60)
            gear = 2;
        else if (speed < 90)
            gear = 3;
        else
            gear = 4;

        // 속도계
        m_SpeedDigit.GetComponent<TextMesh>().text = ((int)speed).ToString();
        if (speed < 30)
            m_SpeedDigit.GetComponent<TextMesh>().color = Color.green;
        else if (speed < 60)
            m_SpeedDigit.GetComponent<TextMesh>().color = Color.yellow;
        else if (speed < 90)
            m_SpeedDigit.GetComponent<TextMesh>().color = new Color(1, 0.5f, 0);
        else
            m_SpeedDigit.GetComponent<TextMesh>().color = Color.red;
        m_SpeedNiddle.transform.localEulerAngles = new Vector3(25, 0, 120 - speed * 2);
    }
    void steer() {
        // 회전
        if (OnDebug) {
            if (Input.GetKey(KeyCode.A) && steering > -100)     // 최대 조향각: 100도
                steering -= 300 * Time.deltaTime;
            else if (Input.GetKey(KeyCode.D) && steering < 100)
                steering += 300 * Time.deltaTime;
            else if (steering < 3 && steering > -3)
                steering = 0;
            else if (steering > 0)
                steering -= 300 * Time.deltaTime;
            else if (steering < 0)
                steering += 300 * Time.deltaTime;
        }
        else {
            steering = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad).x * 100;
        }
        
        // FOR HARD ONLY
        /*
        if (Random.value > 0.95f)
            steering += (Random.value - 0.5f) * 1500f;
        */
        // FOR HARD ONLY

        if (steering > 100)
            steering = 100;
        else if (steering < -100)
            steering = -100;

        if (speed < 10) // 느린 속도 보정
            transform.localEulerAngles += new Vector3(0, (steering / 1.5f * Time.deltaTime) * (0.1f * speed), 0);
        else
            transform.localEulerAngles += new Vector3(0, steering / 1.5f * Time.deltaTime, 0);
        m_Wheel.transform.localEulerAngles = new Vector3(0, steering, 0);
    }

    void Crashed() {
        crashed = true;
        GetComponent<AudioSource>().Stop();
    }

    void Started() {
        started = true;
        GetComponent<AudioSource>().Play();
    }
}
