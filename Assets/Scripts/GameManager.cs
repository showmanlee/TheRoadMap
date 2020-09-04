using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject m_Car;
    public GameObject m_overScreen;
    public GameObject m_prolog;

    int accident = 0;   // 사고 횟수
    int illigal = 0;    // 불법 운전 포인트
    bool reversed = false, missed = false, sidewalked = false, touched = false;
    // 각각 역주행 여부, 신호위반 여부, 인도 침범 여부, 접촉사고 여부 
    bool gameover = false;
    bool restarted = false; 
    float timer = 0;
    float restartTimer = 0;
    string text = "";
    int level = 0;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 30;
    }

    void Start() {
        gameover = false;
    }

    void Update() {
        if (gameover) {
            timer += Time.deltaTime;
            if (timer < 1f)
                m_overScreen.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, timer);
            if (timer > 3f) {
                m_overScreen.transform.GetChild(1).GetComponent<Text>().color = new Color(0, 0, 0, timer - 3f);
                m_overScreen.transform.GetChild(1).GetComponent<Text>().text = "당신은...\n\n";
            }
            if (timer > 5f) {             
                if (m_overScreen.transform.GetChild(2).GetComponent<Text>().text == "") {
                    if (reversed) m_overScreen.transform.GetChild(2).GetComponent<Text>().text += "역주행을 했으며 ";
                    if (missed) m_overScreen.transform.GetChild(2).GetComponent<Text>().text += "신호위반을 저질렀으며 ";
                    if (sidewalked) m_overScreen.transform.GetChild(2).GetComponent<Text>().text += "인도를 침범했고 ";
                    if (touched) m_overScreen.transform.GetChild(2).GetComponent<Text>().text += "접촉사고를 " + accident + "회 일으켰고 ";
                    m_overScreen.transform.GetChild(2).GetComponent<Text>().text += "\n\n" + text;
                }
                m_overScreen.transform.GetChild(2).GetComponent<Text>().color = new Color(0, 0, 0, timer - 5f);
            }
            if (timer > 8f) {
                if (m_overScreen.transform.GetChild(3).GetComponent<Text>().text == "") {
                    if (illigal > 200)
                        m_overScreen.transform.GetChild(3).GetComponent<Text>().text += "당신의 운전은 매우 난폭했습니다.";
                    else if (illigal > 100)
                        m_overScreen.transform.GetChild(3).GetComponent<Text>().text += "당신의 운전은 위험했습니다.";
                    else if (illigal > 20)
                        m_overScreen.transform.GetChild(3).GetComponent<Text>().text += "당신의 운전은 보기에 이상해보였습니다.";
                    else
                        m_overScreen.transform.GetChild(3).GetComponent<Text>().text += "당신의 운전은 언뜻 보기엔 정상적으로 보이긴 했지만, 그건 중요하지 않습니다.";
                }
                m_overScreen.transform.GetChild(3).GetComponent<Text>().color = new Color(0, 0, 0, timer - 8f);
            }
            if (timer > 11f) {
                m_overScreen.transform.GetChild(4).GetComponent<Text>().color = new Color(0, 0, 0, (timer / 2f) - 5.5f);
                m_overScreen.transform.GetChild(4).GetComponent<Text>().text = "\n\n 모두 음주운전 때문입니다. \n\n음주운전은 절대 안 됩니다.";
            }
            if (timer > 16f) {
                for (int i = 1; i <= 5; i++)
                    if (m_overScreen.transform.GetChild(i).GetComponent<Text>().text != "")
                        m_overScreen.transform.GetChild(i).GetComponent<Text>().text = "";
                if (m_overScreen.transform.GetChild(5).GetComponent<Text>().text == "") {
                    m_overScreen.transform.GetChild(5).GetComponent<Text>().color = new Color(0, 0, 0);
                    m_overScreen.transform.GetChild(5).GetComponent<Text>().text = "체험이 모두 종료되었습니다.\n\n다시 하시려면 트리거를 누르세요.\n(프롤로그를 건너뛰고 시작하려면 터치패드를 누르세요.)";
                }
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.A))
                    restart(false);
                else if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad) || Input.GetKeyDown(KeyCode.S))
                    restart(true);
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.R))
            restarted = !restarted;

        if (restarted) {
            restartTimer += Time.deltaTime;
            m_overScreen.transform.GetChild(5).GetComponent<Text>().color = new Color(1, 1, 1);
            m_overScreen.transform.GetChild(5).GetComponent<Text>().text = "재시작까지...\n" + (int)(3 - restartTimer + 1);
            if (restartTimer > 3)
                restart(false);
        }
        else {
            restartTimer = 0;
            if (m_overScreen.transform.GetChild(0).GetComponent<Image>().color == new Color(1, 1, 1, 0))
                m_overScreen.transform.GetChild(5).GetComponent<Text>().text = "";
        }
    }

    // 이하 4대 교통법규 위반 항목
    void DidReverse() {
        if (!reversed)
            reversed = true;
        illigal++;
        print(illigal);
    }

    void DidSignMissed() {
        if (!missed)
            missed = true;
        illigal += 25;
        print(illigal);
    }

    void DidSidewalked() {
        if (!sidewalked)
            sidewalked = true;
        illigal += 2;
        print(illigal);
    }

    void DidTouch() {
        if (!touched)
            touched = true;
        accident++;
        illigal += 35;
        print(accident);
    }

    void GameOver(string type) {    // 게임 즉시 종료
        gameover = true;
        m_Car.SendMessage("Crashed");
        GetComponent<AudioSource>().Play();
        switch (type) {
            case "Human":           // 사람 충돌
                text = "사람을 치고 말았습니다.\n";
                break;
            case "Construct":       // 공사장 충돌
                text = "공사장에 들이받았습니다.\n";
                break;
            case "Accident":        // 나머지 충돌
                text = "질주하다 그대로 들이받았습니다.\n";
                break;
            case "Fall":            // 낙사
                text = "돌아오지 못할 길을 건넜습니다.\n";
                break;
            case "Finish":          // 종료
                text = "결국 단속에 걸리고 말았습니다.\n";
                break;
        }
    }

    void GetLevel(out int n) {
        n = level;
    }

    // 게임 재시작 관련
    void restart(bool isSkipped) {
        gameover = restarted = false;
        SceneManager.LoadScene("RoadMaps");
        Destroy(gameObject);
    }
}
