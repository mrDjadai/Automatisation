using UnityEngine;

public class GameEnder : MonoBehaviour
{
    [SerializeField] private int levelID;

    public void Win()
    {
        if (PlayerPrefs.GetInt("Level") == levelID)
        {
            PlayerPrefs.SetInt("Level", levelID + 1);
        }
    }

    public void Lose()
    {

    }
}
