using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public List<Transform> cameraPositions = new List<Transform>();
    int currentCamera = 404;

    private void Start()
    {
        //ChangeCameraPosition("ThatCamera") //The first camera it should look for
    }

    public void ChangeCameraPosition(string cameraPosition)
    {
        for(int i = 0; i < cameraPositions.Count; i++) 
        {
            if (cameraPositions[i].name == cameraPosition) 
            {
                currentCamera = i;
            }
        }
    }

    private void Update()
    {
        if (currentCamera == 404) return;
        transform.position = cameraPositions[currentCamera].position;
    }
}
