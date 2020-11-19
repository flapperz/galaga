using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScoreController : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    void Update()
    {
        scoreText.text = GameManager.Score.ToString();
    }
}
