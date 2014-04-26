using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

namespace game
{
	public class GameContextView : ContextView {
		void Awake()
		{
			context = new GameContext(this, true);
			context.Start();
		}
	}
}