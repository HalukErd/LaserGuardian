
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    GameSession gameSession;
    Text scoreText;
    // Use this for initialization
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        scoreText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        scoreText.text = gameSession.GetScore().ToString();
    }
}
