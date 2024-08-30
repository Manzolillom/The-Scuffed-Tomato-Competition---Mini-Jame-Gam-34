using UnityEngine;

public class LookAtTheCamera : MonoBehaviour
{
    [SerializeField] Transform whatToLookAt;
    void Update()
    {
        transform.LookAt(whatToLookAt);
    }
}
