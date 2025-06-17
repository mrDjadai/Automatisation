using UnityEngine;
using System.Collections;

public class LightActivator : MonoBehaviour
{
    [SerializeField] private LightLine[] lines;
    [SerializeField] private AudioClip activateSound;
    [SerializeField] private AudioClip deactivateSound;
    [SerializeField] private float lineDelay;

    private Coroutine cur;

    public void SetActivated(bool m)
    {
        if (cur != null)
        {
            StopCoroutine(cur);
        }
        cur = StartCoroutine(SetMode(m));
    }

    private IEnumerator SetMode(bool m)
    {
        if (lines.Length > 0)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                yield return new WaitForSeconds(lineDelay);
                bool useSound = lines[i].lamps[0].enabled != m;
                foreach (var item in lines[i].lamps)
                {
                    item.enabled = m;
                }
                if (useSound)
                {
                    lines[i].source.PlayOneShot(m ? activateSound : deactivateSound);
                }
            }
        }
    }   
    
    [System.Serializable]
    private struct LightLine
    {
        public AudioSource source;
        public Light[] lamps;
    }    
}
