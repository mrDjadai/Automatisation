using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private float animationDuration;
    [SerializeField] private Image bgImage;

    private Color baseColor;

    private void Awake()
    {
        baseColor = bgImage.color;

        if (PlayerPrefs.GetInt("UseTransition") == 1 || PlayerPrefs.GetInt("CurrentLevel") == 0)
        {
            PlayerPrefs.SetInt("UseTransition", 0);
            bgImage.DOColor(Color.clear, animationDuration * 2);
        }
        else
        {
            bgImage.color = Color.clear;
        } 
    }

    public void Restart()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(int i)
    {
       // if (i == 0)
        {
            PlayerPrefs.SetInt("UseTransition", 1);
        }
        bgImage.DOColor(baseColor, animationDuration).OnComplete(() => { SceneManager.LoadScene(i); });
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
