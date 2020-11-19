using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text liveText;

    void Update()
    {
        scoreText.text = GameManager.Score.ToString();
        liveText.text = PlayerController.Instance.Live.ToString();
    }
}
