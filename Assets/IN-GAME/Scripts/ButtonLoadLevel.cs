﻿
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

namespace FindItFast
{
	/// <summary>
	/// This function loads a level when clicked on. In order to detect clicks you need to attach a collider to this object.
	/// </summary>
	public class ButtonLoadLevel:MonoBehaviour 
	{
		private Transform thisTransform;
		
		//The name of the level to be loaded
		public string levelName;
		
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
			if ( clickEffect == true )    ClickEffect();
			
			//Play a sound from the source
			if ( soundSource )    if ( soundSource.GetComponent<AudioSource>() )    soundSource.GetComponent<AudioSource>().PlayOneShot(soundClick);
			
			//Wait a while
			yield return new WaitForSeconds(delay);

			//Load the level
			if ( levelName != string.Empty )    
			{
				#if UNITY_5_3 || UNITY_5_3_OR_NEWER
				SceneManager.LoadScene(levelName);
				#else
				Application.LoadLevel(levelName);
				#endif
			}
		}

		//Create an effect, making the object large and then gradually smaller
		IEnumerator ClickEffect()
		{
			//Register the original size of the object
			var initScale = thisTransform.localScale;
			
			//Resize it to be larger
			thisTransform.localScale = initScale * 1.1f;
			
			//Gradually reduce its size back to the original size
			while ( thisTransform.localScale.x > initScale.x * 1.01f )
			{
				yield return new WaitForFixedUpdate();

				thisTransform.localScale = new Vector3( thisTransform.localScale.x - 1 * Time.deltaTime, thisTransform.localScale.x - 1 * Time.deltaTime, thisTransform.localScale.z);
			}
			
			//Reset the size to the original
			thisTransform.localScale = thisTransform.localScale = initScale;
		}	

	}
}




