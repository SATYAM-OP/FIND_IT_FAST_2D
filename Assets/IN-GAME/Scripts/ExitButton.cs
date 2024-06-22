using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    private Transform thisTransform;

    //How many seconds to wait before loading the level, after a click
    public float delay = 0.5f;

    //The sound of the click and the source of the sound
    public AudioClip soundClick;
    public Transform soundSource;

    //Should there be a click effect
    public bool clickEffect = true;

    void Start()
    {
        thisTransform = transform;
    }

    /// <summary>
    /// Raises the mouse down event.
    /// </summary>
    IEnumerator OnMouseDown()
    {
        //Create an effect
        if (clickEffect == true) ClickEffect();

        //Play a sound from the source
        if (soundSource) if (soundSource.GetComponent<AudioSource>()) soundSource.GetComponent<AudioSource>().PlayOneShot(soundClick);

        //Wait a while
        yield return new WaitForSeconds(delay);

        //Exit level
        Application.Quit();
    }

    //Create an effect, making the object large and then gradually smaller
    IEnumerator ClickEffect()
    {
        //Register the original size of the object
        var initScale = thisTransform.localScale;

        //Resize it to be larger
        thisTransform.localScale = initScale * 1.1f;

        //Gradually reduce its size back to the original size
        while (thisTransform.localScale.x > initScale.x * 1.01f)
        {
            yield return new WaitForFixedUpdate();

            thisTransform.localScale = new Vector3(thisTransform.localScale.x - 1 * Time.deltaTime, thisTransform.localScale.x - 1 * Time.deltaTime, thisTransform.localScale.z);
        }

        //Reset the size to the original
        thisTransform.localScale = thisTransform.localScale = initScale;
    }

}
