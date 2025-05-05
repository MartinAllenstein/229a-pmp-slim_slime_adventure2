using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerSystem : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Level1Scene");
    }

    public void LoadCreditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }
}
