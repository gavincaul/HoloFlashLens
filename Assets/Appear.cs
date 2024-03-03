using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour
{
    public GameObject avatar;
    private float appearingSpeed = 1f;

    void Start()
    {
        // Initially, set the scale to zero to make it invisible
        avatar.transform.localScale = Vector3.zero;
    }

    // Method to make the avatar appear
    public void appear()
    {
        StartCoroutine(AnimateAvatar(Vector3.one));
    }

    // Method to make the avatar disappear
    public void disappear()
    {
        StartCoroutine(AnimateAvatar(Vector3.zero));
    }

    // Coroutine to animate the avatar's scale
    private IEnumerator AnimateAvatar(Vector3 targetScale)
    {
        Vector3 currentScale = avatar.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < appearingSpeed)
        {
            // Interpolate between current scale and target scale
            avatar.transform.localScale = Vector3.Lerp(currentScale, targetScale, (elapsedTime / appearingSpeed));

            // Update elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure avatar reaches exact target scale
        avatar.transform.localScale = targetScale;
    }
}
