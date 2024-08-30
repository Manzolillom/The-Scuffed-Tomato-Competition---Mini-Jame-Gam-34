using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float dayTimeLenght;
    float dayTimeLeft;

    void Start()
    {
        dayTimeLeft = dayTimeLenght;
    }

    void Update()
    {
        dayTimeLeft -= Time.deltaTime;
        if(dayTimeLeft < 0)
        {
            SceneManager.LoadScene("Judge");
        }
    }
}
