﻿using UnityEngine;
using System.Collections.Generic;

using GDK.Pool;
using GDK.MathEngine;
using DG.Tweening;
using Zenject;

namespace GDK.Reels
{
	public class ReelLayout : MonoBehaviour
	{
		[Inject] Paytable paytable;
		[Inject] IRng rng;
		[Inject] ISymbolFactory symbolFactory;

		[SerializeField] private int visibleSymbols;
		[SerializeField] private float spinTime = 1f;
		[SerializeField] private float reelHeight = 10;

		private List<GameObject> symbolContainers = new List<GameObject> ();
		private List<GameObject> symbolObjects = new List<GameObject> ();

		private float symbolHeight;

		private Vector3 initialPosition;

		void Start ()
		{
			DOTween.Init (false, true, LogBehaviour.ErrorsOnly);

			initialPosition = gameObject.transform.position;

			// Create the symbol containers.
			for (int i = 0; i < visibleSymbols; ++i)
			{
				var symbolContainer = new GameObject();
				symbolContainer.name = "SymbolContainer";
				symbolContainer.transform.parent = gameObject.transform;
				symbolContainer.transform.localPosition = Vector3.zero;
				symbolContainers.Add (symbolContainer);
			}

			// Align the symbol containers.
			AnimationCurveExtensions.AlignVerticalCenter (
				symbolContainers, 
				initialPosition.y + reelHeight, 
				initialPosition.y - reelHeight);

			// TODO: This is a bit hacky...
			symbolHeight = symbolContainers[0].transform.position.y - symbolContainers[1].transform.position.y;

			// Attach the symbol as a child objects of the symbol containers.
			for (int i = 0; i < visibleSymbols; ++i)
			{
				var symbolObject = PoolManager.Obtain (symbolFactory.CreateSymbol ("AA"));
				symbolObject.transform.parent = symbolContainers[i].transform;
				symbolObject.transform.localPosition = Vector3.zero;
				symbolObjects.Add (symbolObject);
			}
		}

		private bool spinning;

		private void Update ()
		{
			if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
			{
				if (!spinning)
					SetTween ();

				spinning = !spinning;
			}
		}

		private void OnComplete ()
		{
			// Return the last symbol object to the pool.
			PoolManager.Return (symbolObjects[symbolObjects.Count - 1]);

			// Shuffle all symbols down.
			for (int i = symbolObjects.Count - 1; i > 0; --i)
			{
				symbolObjects [i] = symbolObjects[i - 1];
				symbolObjects [i].transform.parent = symbolContainers [i].transform;
				symbolObjects [i].transform.localPosition = Vector3.zero;
			}

			// Pick a random symbol for fun!
			List<ReelProperties> reelProps = paytable.ReelGroup.Reels;
			List<Symbol> symbols = reelProps [0].ReelStrip.Symbols;
			int random = rng.GetRandomNumber (symbols.Count);

			// Add the new symbol object.
			symbolObjects [0] = PoolManager.Obtain (symbolFactory.CreateSymbol (symbols[random].Name));
			symbolObjects [0].transform.parent = symbolContainers [0].transform;
			symbolObjects [0].transform.localPosition = Vector3.zero;

			// Reset the reel mover.
			gameObject.transform.position = initialPosition;

			if (spinning)
				SetTween ();
		}

		private void SetTween ()
		{
			gameObject.transform.DOMove (
				new Vector3 (initialPosition.x, initialPosition.y - symbolHeight, initialPosition.z), spinTime)
				.SetEase (Ease.Linear)
				.OnComplete (OnComplete);
		}
	}

	public static class AnimationCurveExtensions
	{
		public static void AlignVerticalCenter (List<GameObject> gameObjects, float valueStart, float valueEnd)
		{
			AnimationCurve layoutCurve = AnimationCurve.Linear (0, valueStart, 1, valueEnd);
			float firstPos = 1.0f / (gameObjects.Count + 1);

			for (int i = 1; i <= gameObjects.Count; ++i)
			{
				float y = layoutCurve.Evaluate (i * firstPos);
				UpdatePosition (gameObjects [i - 1].transform, 0, y, 0);
			}
		}

		private static void UpdatePosition(Transform t, float x, float y, float z)
		{
			t.position = new Vector3 (t.position.x + x, t.position.y + y, t.position.z + z);
		}
	}
}