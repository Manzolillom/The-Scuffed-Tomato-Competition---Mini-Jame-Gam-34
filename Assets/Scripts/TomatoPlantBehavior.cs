using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoPlantBehavior : MonoBehaviour
{
    [SerializeField]float tomatoDefaultGrowingSpeed = 1;

    private void Start()
    {
        ResetTomatoesSize();
    }

    void FixedUpdate()
    {
        if (CheckIfGrounded()) //the plant will only grow if on the ground
                GrowTomatoes();
    }

    bool CheckIfGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.5f);
    }

    void GrowTomatoes() //Makes the tomatoes go big big
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            float growOfHowMuch = Random.Range(0.0f, tomatoDefaultGrowingSpeed);
            Transform tomato = transform.GetChild(i);
            tomato.localScale += new Vector3(growOfHowMuch, growOfHowMuch, growOfHowMuch);
        }
    }

    void ResetTomatoesSize()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform tomato = transform.GetChild(i);
            tomato.localScale = Vector3.zero;
        }
    }
}
