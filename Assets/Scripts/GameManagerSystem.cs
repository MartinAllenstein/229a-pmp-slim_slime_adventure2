using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerSystem : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadCreditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }
}
