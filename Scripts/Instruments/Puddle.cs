using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class Puddle : MonoBehaviour
{
    [SerializeField] private PhysicsMaterial itemMaterial;
    [SerializeField] private DecalProjector decalProjector;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Vector3 targetColliderScale;
    [SerializeField] private float targetWidth;
    [SerializeField] private float targetHeight;
    [SerializeField] private float animationTime;
    [SerializeField] private Vector3 rotateAxis;
    [SerializeField] private float scaleBonus;
    [SerializeField] private string scaleBonusKey;

    private IEnumerator Start()
    {
        float t = 0;
        transform.localEulerAngles = rotateAxis * Random.Range(0, 360f);

        if (SaveManager.instance.HasUpgrade(scaleBonusKey))
        {
            targetWidth *= scaleBonus;
            targetHeight *= scaleBonus;
            targetColliderScale *= scaleBonus;
        }

        do
        {
            t += Time.deltaTime;
            float p = t / animationTime;
            float w = Mathf.Lerp(0, targetWidth, p);
            float h = Mathf.Lerp(0, targetHeight, p);
            float z = decalProjector.size.z;

            decalProjector.size = new Vector3(w, h, z);
            boxCollider.size = Vector3.Lerp(Vector3.zero, targetColliderScale, p);

            yield return new WaitForEndOfFrame();
        } while (t < animationTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.OnPuddleEnter();
        }

        if (other.attachedRigidbody == null)
        {
            return;
        }

        if (other.attachedRigidbody.TryGetComponent<Instrument>(out Instrument instrument))
        {
            instrument.SetPhysicMaterial(itemMaterial);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.OnPuddleExit();
        }

        if (other.attachedRigidbody == null)
        {
            return;
        }

        if (other.attachedRigidbody.TryGetComponent<Instrument>(out Instrument instrument))
        {
            instrument.SetPhysicMaterial(null);
        }
    }
}
