using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType { Light, Ped }

public class TriggerRoad : MonoBehaviour {

    public GameObject m_CrossLight;
    public GameObject m_Ped;
    public TriggerType Type;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "TheCar") 
            switch (Type) {
                case TriggerType.Light:
                    if (Random.value > 0.5f)
                        m_CrossLight.SendMessage("EnterTrigger");
                    break;
                case TriggerType.Ped:
                    if (Random.value > 0.5f)
                        m_Ped.SendMessage("GoGoGo");
                    break;
            }
    }
}
