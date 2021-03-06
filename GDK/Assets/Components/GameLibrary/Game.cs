﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDK.StateMachine;

namespace GDK.GameLibrary
{
	/// <summary>
	/// Base class for games.
	/// </summary>
	public class Game
	{
		private BaseState stateConfiguration;
		private BaseState stateIdle;
		private BaseState statePlay;
		private BaseState statePayWin;
		private BaseState stateGameOver;

		protected GameStateMachine stateMachine;

		/// <summary>
		/// Configure all the game states.
		/// </summary>
		/// <param name="stateMachine">The game state machine.</param>
		public virtual void ConfigureStates (GameStateMachine stateMachine)
		{
			this.stateMachine = stateMachine;

			stateConfiguration = new StateConfiguration ();
			stateIdle = new StateIdle ();
			statePlay = new StatePlay ();
			statePayWin = new StatePayWin ();
			stateGameOver = new StateGameOver ();

			stateConfiguration.Configure (stateMachine);
			stateIdle.Configure (stateMachine);
			statePlay.Configure (stateMachine);
			statePayWin.Configure (stateMachine);
			stateGameOver.Configure (stateMachine);
		}

		public virtual void StartConfiguration()
		{
			stateMachine.AddTrigger ("TriggerStateConfiguration");
		}

		public virtual void StartIdle()
		{
			stateMachine.AddTrigger ("TriggerStateIdle");
		}

		public virtual void StartPlay()
		{
			stateMachine.AddTrigger ("TriggerStatePlay");
		}

		public virtual void StartPayWin ()
		{
			stateMachine.AddTrigger ("TriggerStatePayWin");
		}

		public virtual void StartGameOver()
		{
			stateMachine.AddTrigger ("TriggerStateGameOver");
		}
	}
}
