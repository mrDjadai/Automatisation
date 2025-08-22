using UnityEngine;
using TMPro;

public class CharacterNameShower : MonoBehaviour
{
    [SerializeField] private TMP_Text txt;

    private void Start()
    {
        txt.text = SaveManager.instance.CharacterName;
    }
}
