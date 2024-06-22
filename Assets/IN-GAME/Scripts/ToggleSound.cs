using UnityEngine;
using System.Collections;

namespace FindItFast
{
	/// <summary>
	/// This script toggles a sound source when clicked on. It also records the sound state (on/off) in a PlayerPrefs. In order to detect clicks you need to attach a collider to this object.
	/// </summary>
	public class ToggleSound:MonoBehaviour 
	{
		[Tooltip("The tag of the sound object")]
		public string soundObjectTag = "GameController";

		[Tooltip("The source of the sound")]
		public Transform soundObject;
	
		[Tooltip("The PlayerPrefs name of the sound")]
		public string playerPref = "SoundVolume";
	
		[Tooltip("The default volume of the sound. 0 to 1")]
		public float defaultVolume = 1;

		// The index of the current value of the sound
		internal float currentState;

		void Awake()
		{
			if ( !soundObject && soundObjectTag != string.Empty )    soundObject = GameObject.FindGameObjectWithTag(soundObjectTag).transform;

			// Get the current state of the sound from PlayerPrefs
			if( soundObject )
				currentState = PlayerPrefs.GetFloat(playerPref, soundObject.GetComponent<AudioSource>().volume);
			else   
				currentState = PlayerPrefs.GetFloat(playerPref, defaultVolume);
		
			// Set the sound in the sound source
			SetSound();
		}

		void OnMouseDown()
		{
			ToggleSoundFunction();
		}
	
		/// <summary>
		/// Sets the sound volume
		/// </summary>
		void SetSound()
		{
			if ( !soundObject && soundObjectTag != string.Empty )    soundObject = GameObject.FindGameObjectWithTag(soundObjectTag).transform;

			// Set the sound in the PlayerPrefs
			PlayerPrefs.SetFloat(playerPref, currentState);

			Color newColor = GetComponent<SpriteRenderer>().color;

			// Update the graphics of the button image to fit the sound state
			if( currentState == defaultVolume )
				newColor.a = 1;
			else
				newColor.a = 0.5f;

			GetComponent<SpriteRenderer>().color = newColor;

			// Set the value of the sound state to the source object
			if( soundObject ) 
				soundObject.GetComponent<AudioSource>().volume = currentState;
		}
	
		/// <summary>
		/// Toggle the sound. Cycle through all sound modes and set the volume and icon accordingly
		/// </summary>
		void ToggleSoundFunction()
		{
			if ( currentState == defaultVolume )    currentState = 0;
			else    currentState = defaultVolume;
		
			SetSound();
		}
	
		/// <summary>
		/// Starts the sound source.
		/// </summary>
		void StartSound()
		{
			if( soundObject )
				soundObject.GetComponent<AudioSource>().Play();
		}
	}
}











