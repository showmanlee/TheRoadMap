using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerr : MonoBehaviour {
    public RawImage m_RawImage;
    public VideoPlayer m_VideoPlayer;
    public AudioSource m_AudioSource;
    public GameObject m_Player;
    public GameObject m_Button;
    public GameObject m_Tutorial;
    float timer;

    // Use this for initialization
    void Start () {
        StartCoroutine(PlayVideo());
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad) || Input.GetKey(KeyCode.Space)) {
            print("skipping");
            timer += Time.deltaTime;
            int i = (int)(timer / 2 * 100);
            m_Button.GetComponentInChildren<Text>().text = i.ToString();
            if (timer > 2)
                Destroy(transform.parent.parent.gameObject);
        }
        else {
            m_Button.GetComponentInChildren<Text>().text = "터치패드를 길게 눌러 SKIP";
            timer = 0;
        }
        if (!m_VideoPlayer.isPlaying && m_VideoPlayer.isPrepared)
            Destroy(transform.parent.parent.gameObject);
	}

    IEnumerator PlayVideo() {
        m_VideoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!m_VideoPlayer.isPrepared) {
            yield return waitForSeconds;
            break;
        }
        m_RawImage.texture = m_VideoPlayer.texture;
        m_VideoPlayer.Play();
        m_AudioSource.Play();
    }
    private void OnDestroy() {
        m_Tutorial.SetActive(true);
        m_Player.SendMessage("Started");
    }

    void Die() {
        Destroy(transform.parent.parent.gameObject);
    }
}
