using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crashing : MonoBehaviour {

    public GameObject m_GameManager;

    bool crash;
	// Use this for initialization
	void Start () {
        crash = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag.Contains("Trigger"))
            print("TRIGGER YEAH!");
        if (!collision.gameObject.tag.Contains("Road")) {  // 도로와 충돌된 게 아닐 때...
            float defaultSpeed = CarControl.speed;                     // 사고 판정 확인을 위한 직전 속도 체크
            CarControl.speed = 0;
            if (collision.gameObject.tag.Contains("Human"))              // 대인사고
                m_GameManager.SendMessage("GameOver", "Human");
            else if (collision.gameObject.tag.Contains("Construct"))     // 공사장 사고
                m_GameManager.SendMessage("GameOver", "Construct");
            else if (collision.gameObject.name == "GameOver") // 낙사
                m_GameManager.SendMessage("GameOver", "Fall");
            else {
                if (defaultSpeed > 40)
                    m_GameManager.SendMessage("GameOver", "Accident");   // 대물사고
                else
                    m_GameManager.SendMessage("DidTouch");            // 넘어가드림
            }
        }
        else {                                              // 도로 중 교통법규 확인
            if (collision.gameObject.tag == "Road_Sidewalk")        // 인도 밟았을 때
                m_GameManager.SendMessage("DidSidewalked");
            else if (collision.gameObject.tag == "Road_Reverse")    // 중앙선 침범 시
                m_GameManager.SendMessage("DidReverse");
            else if (collision.gameObject.tag == "Road_Finish")        // 종료 지점일 경우
                m_GameManager.SendMessage("GameOver", "Finish");
        }
    }
}
