using UnityEngine;
using System.Collections;

public class ShipTransfer : MonoBehaviour
{
    public float travelSpeed = 5f;

    public IEnumerator MoveTo(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                travelSpeed * Time.deltaTime
            );

            yield return null;
        }
    }
}