using System.Collections;
using UnityEngine;

public class SteamPipePoint : MonoBehaviour, ILookDetectable
{
    public bool IsBroken => !(steam.localScale == Vector3.zero);
    public Transform ConnectedPoint => connectedPoint;
    private Transform connectedPoint;
    [SerializeField] private SteamPipe pipe;
    [SerializeField] private float timeToRepair;
    [SerializeField] private string repairSpeedKey;
    [SerializeField] private string autoKey;
    [SerializeField] private float repairSpeedBonus;
    [SerializeField] private float infinityBonus;
    [SerializeField] private string infinityKey;
    [SerializeField] private EaseAudioSourse steamSource;
    [SerializeField] private EaseAudioSourse weldingSource;
    [SerializeField] private Transform point;

    [SerializeField] private float maxScale;

    [SerializeField] private float weldingTime = 0.2f;
    [SerializeField] private float repairOffset;

    private bool autoMode;
    [SerializeField] private Transform steam;
    private Coroutine cor;
    [SerializeField] private string scaleBonusKey;
    [SerializeField] private float scaleBonusValue;

    public void ActivateOnPlace(Transform spawn)
    {
        steam.forward = spawn.forward;
        steam.localScale = maxScale * Vector3.one;

        point.position = spawn.position;
        point.gameObject.SetActive(true);
        steamSource.Play();
        connectedPoint = spawn;

        steamSource.transform.position = spawn.position;
        weldingSource.transform.position = spawn.position;
    }

    private void Start()
    {
        if (SaveManager.instance.HasUpgrade(repairSpeedKey))
        {
            timeToRepair /= repairSpeedBonus;
        }
        if (SaveManager.instance.HasUpgrade(infinityKey))
        {
            timeToRepair /= infinityBonus;
        }
        if (SaveManager.instance.HasUpgrade(scaleBonusKey))
        {
            point.GetComponent<SphereCollider>().radius *= scaleBonusValue;
        }

        autoMode = SaveManager.instance.HasUpgrade(autoKey);

        steamSource.transform.parent = null;
        weldingSource.transform.parent = null;
    }

    private bool isRepairing;

    private void Update()
    {
        if (isRepairing)
        {
            Repair(Time.deltaTime / timeToRepair, autoMode);
        }
    }

    public void OnRepair()
    {
        steamSource.Stop();
        weldingSource.Stop();
        point.gameObject.SetActive(false);
        if (cor != null)
        {
            StopCoroutine(cor);
        }
        cor = null;
    }

    public void Repair(float f, bool autoMode)
    {
        float scale = Mathf.Max(0, steam.localScale.x - f);

        steam.localScale = scale * Vector3.one;

        if (scale <= repairOffset)
        {
            connectedPoint = null;

            pipe.TryRepair();
            steam.localScale = Vector3.zero;
        }

        if (cor != null)
        {
            StopCoroutine(cor);
        }
        weldingSource.Play();
        if (gameObject.activeSelf)
        {
            cor = StartCoroutine(Stop());
        }

        if (!autoMode)
        {
            return;
        }

        if (PlayerInventory.instance.InHandItem is Welding)
        {
            Welding w = PlayerInventory.instance.InHandItem as Welding;

            if (!w.IsActive)
            {
                w.Use();
            }
        }
    }

    private IEnumerator Stop()
    {
        yield return new WaitForSeconds(weldingTime);
        weldingSource.Stop();
        cor = null;
    }

    public void OnLook()
    {
        isRepairing = true;
    }

    public void OnUnLook()
    {
        isRepairing = false;
    }

    public virtual void OnStartLook()
    {
        if (!autoMode)
        {
            return;
        }

        if (PlayerInventory.instance.InHandItem is Welding)
        {
            Welding w = PlayerInventory.instance.InHandItem as Welding;

            if (!w.IsActive)
            {
                w.Use();
            }
        }
    }

    public virtual void OnEndLook()
    {
        if (!autoMode)
        {
            return;
        }

        if (PlayerInventory.instance.InHandItem is Welding)
        {
            Welding w = PlayerInventory.instance.InHandItem as Welding;

            if (w.IsActive)
            {
                w.Use();
            }
        }
    }

    public virtual void Interact()
    {
    }

    public virtual void EndInteract()
    {
    }
}
