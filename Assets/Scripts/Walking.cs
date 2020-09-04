using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PedMode { CycleX, CycleZ, Gone }
public class Walking : MonoBehaviour {

    public PedMode mode;
    public float speed = 3;
    public float ceil = 8.0f, floor = -40f;
    public float lifeTime = 20;
    public GameObject m_Hip;

    bool stoped = false;
    float timer;
    float initTurn;
    // Use this for initialization
    void Start() {
        initTurn = transform.eulerAngles.y;
        m_Hip.GetComponent<Animator>().speed = speed / 2;
        if (mode == PedMode.Gone) {
            m_Hip.GetComponent<Animator>().speed = 0;
            timer = 30;
        }
    }

    // Update is called once per frame
    void Update() {
        if (!stoped) {
            switch (mode) {
                case PedMode.CycleX:
                case PedMode.CycleZ:            // X축 왕복 / Z축 왕복
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    if (mode == PedMode.CycleX && (transform.position.x > ceil + 0.1f || transform.position.x < floor + 0.1f))
                        transform.Rotate(Vector3.up * 180.0f);
                    if (mode == PedMode.CycleZ && (transform.position.z > ceil + 0.1f || transform.position.z < floor + 0.1f))
                        transform.Rotate(Vector3.up * 180.0f);
                    break;
                case PedMode.Gone:              // 직진 후 사라짐
                    if (timer > 0)
                        timer -= Time.deltaTime;
                    else {
                        if (m_Hip.GetComponent<Animator>().speed < 0.1f)
                            m_Hip.GetComponent<Animator>().speed = speed / 2;
                        lifeTime -= Time.deltaTime;
                        transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        if (lifeTime < 0)
                            Destroy(gameObject);
                    }
                    break;
            }
        }
        else {

        }
    }

    void Wow() {
        stoped = true;
        m_Hip.GetComponent<Animator>().speed = 0;
    }
    void UnWow() {
        stoped = false;
        m_Hip.GetComponent<Animator>().speed = speed / 2;
    }
}
