using System.Collections;

namespace GameLib
{
	public abstract class State<T>
	{
		protected StateMachine<T> stateMachine;

		public abstract void Enter();

		public abstract void Execute();

		public abstract void Exit();

		public void SetStateMachine(StateMachine<T> stateMachine)
		{
			this.stateMachine = stateMachine;
		}

		public StateMachine<T> GetStateMachine()
		{
			return stateMachine;
		}

		public virtual bool OnNotice(StateNotice stateNotice)
		{
			// Implement in extended class
			return false;
		}

		~State()
		{
		}
	}
}