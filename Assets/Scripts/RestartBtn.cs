using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartBtn : MonoBehaviour
{


    void Update()
    {
        if (Input.GetButton("Jump"))
            GameManager.ReGame();
    }
}
