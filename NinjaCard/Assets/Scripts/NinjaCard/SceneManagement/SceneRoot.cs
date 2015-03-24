using UnityEngine;
using System;

namespace NinjaCard.SceneManagement
{
	public class SceneRoot : MonoBehaviour 
	{
		public Camera MainCamera;
		public Transform SceneSpaceTransform;
		public Canvas UiCanvas;

		private void Start()
		{
			if (MainCamera == null)
			{
				throw new Exception("SceneRoot must have a camera.");
			}

			if (SceneSpaceTransform == null)
			{
				throw new Exception("SceneRoot must have SceneSpaceTransform.");
			}

			if (UiCanvas == null)
			{
				throw new Exception("SceneRoot must have UiCanvas.");
			}

			if (transform.childCount != 3)
			{
				throw new Exception("SceneRoot must only have Camera, SceneSpaceTransform and UiCanvas at root level.");
			}

		}
	}
}