using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Item))]
public class Colorizable : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] renderers;
    private Item item;

    private Color[] colors => item.Settings.Colors;

    public void Init()
    {
        item = GetComponent<Item>();
        foreach (var i in renderers)
        {
            foreach (var c in i.materials)
            {
                c.SetColor("_MainColor", colors[item.ColorID]);
            }
        }
    }

    public void Colorize(float time, int colorId)
    {
        item.ColorID = colorId;

        StartCoroutine(SetColor(time, colorId));
    }

    private IEnumerator SetColor(float useTime, int colorId)
    {
        foreach (var i in renderers)
        {
            foreach (var c in i.materials)
            {
                c.SetColor("_NewColor", colors[colorId]);
            }
        }

        float time = 0;

        while (time < useTime)
        {
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
            float val = time / useTime;

            foreach (var i in renderers)
            {
                foreach (var c in i.materials)
                {
                    c.SetFloat("_Percent", val);
                }
            }
        }
    }
}
