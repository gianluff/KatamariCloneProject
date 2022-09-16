using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject ball;
   
    void Update()
    {
        //assegno alla camera le stesse coordinate della sfera per seguirla costantemente
        transform.position = new Vector3(ball.transform.position.x, 17.5f, ball.transform.position.z);
    }
}
