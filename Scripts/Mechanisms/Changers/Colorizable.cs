using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Item))]
public class Colorizable : MonoBehaviour
{
    [SerializeField] private int baseID;
    [SerializeField] private Renderer[] renderers;
    private Item item;

    private Color[] colors => item.Settings.Colors;

    public void Init()
    {
        item = GetComponent<Item>();
    }

    public void Colorize(float time, int[] colorIds)
    {
        StartCoroutine(SetColor(time, colorIds));
        foreach (var r in renderers)
        {
            foreach (var i in r.renderers)
            {
                foreach (var c in i.materials)
                {
                    c.SetColor("_MainColor", colors[baseID]);
                }
            }
        }
    }

    private IEnumerator SetColor(float useTime, int[] colorIds)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            foreach (var r in renderers[i].renderers)
            {
                foreach (var c in r.materials)
                {
                    c.SetColor("_NewColor", colors[colorIds[i]]);
                }
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
                foreach (var item in i.renderers)
                {
                    foreach (var c in item.materials)
                    {
                        c.SetFloat("_Percent", val);
                    }
                }
            }
        }
    }

    [System.Serializable] 
    private struct Renderer
    {
        public MeshRenderer[] renderers;
    }
}
