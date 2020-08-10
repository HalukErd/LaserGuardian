using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthDisplay : MonoBehaviour {

    Player gameSession;
    
    // Use this for initialization
    void Start()
    {
        gameSession = FindObjectOfType<Player>();
        GetComponent<Text>().text = gameSession.GetHealth().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = gameSession.GetHealth().ToString();
    }
}
