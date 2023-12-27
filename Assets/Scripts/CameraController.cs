using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    [SerializeField]private Transform target;
    public float smoothTime = 0.3F;
    private Vector3 newPos;

    private void LateUpdate()
    {
        if (MainMenu.isStartGame)
        {          
            newPos = new Vector3(target.transform.position.x+2.55f,target.transform.position.y+3.73f,target.transform.position.z-10);
            transform.position = newPos;
        }
    }
    void Update()
    {
        
    }
}
