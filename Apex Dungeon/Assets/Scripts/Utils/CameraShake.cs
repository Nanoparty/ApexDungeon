using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{



    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition =
                new Vector3(x, y, transform.localPosition.z);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.localPosition = orignalPosition;
    }


}