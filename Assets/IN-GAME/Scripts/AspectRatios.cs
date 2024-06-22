using UnityEngine;
using System;

namespace FindItFast
{
	/// <summary>
	/// This scripts dynamically set the aspect ratio to fit several screen aspects
	/// </summary>
	public class AspectRatios:MonoBehaviour 
	{
		internal Camera cameraObject;
		
		public Transform backgroundObject;
		
		public Transform topBarObject;
		
		public CustomAspects[] customAspect;

		[Serializable]
		public class CustomAspects
		{
			public Vector2 aspect = new Vector2(16,9);
			
			public float cameraSize = 5;
			
			public Vector2 backgroundScale = new Vector2(3.5f,2);
			
			public Vector2 topBarPosition = new Vector2(0,4);
		}

		void Start() 
		{
			cameraObject = this.GetComponent<Camera>();
			
			foreach ( CustomAspects index in customAspect )
			{
				if ( Mathf.Round(cameraObject.aspect * 100f) / 100f == Mathf.Round((index.aspect.x/index.aspect.y) * 100f) / 100f )
				{
					cameraObject.orthographicSize = index.cameraSize;
					
					if ( backgroundObject )
					{
						backgroundObject.localScale = new Vector3( index.backgroundScale.x, index.backgroundScale.y, backgroundObject.localScale.z);
					}
					
					if ( topBarObject )
					{	
						topBarObject.position = new Vector3( index.topBarPosition.x, index.topBarPosition.y, topBarObject.position.z);
					}
				}
			}
		}
	}
}