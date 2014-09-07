using System.Collections;

namespace GameLib
{
	public class StateMachine<T>
	{
		//a agent that owns this instance
		private T owner;

		//Global state the agent is in
		private State<T> globalState;
		//current state the agent is in
		private State<T> currentState;
		//a record of the last state the agent was in
		private State<T> previousState;

		private NoticeQueue noticeQueue;

		//accessors
		public State<T> CurrentState 
		{
			get {
				return currentState;
			}
		}

		public State<T> PreviousState
		{
			get {
				return previousState;
			}
		}

		public T Owner
		{
			get {
				return owner;
			}
		}

		public StateMachine(T owner, State<T> initialState, State<T> globalState = null)
		{
			this.owner = owner;
			this.noticeQueue = new NoticeQueue();
			this.globalState = globalState;
			if (globalState != null) {
				this.globalState.Enter();
			}
			this.ChangeState(initialState);
		}

		//call this to update the FSM
		public void Update()
		{
			//execute global state
			if (globalState != null) globalState.Execute();
			//same for the current state
			if (currentState != null) currentState.Execute();
		}
	
		//change to a new state
		public void ChangeState(State<T> new_state)
		{
			//keep a record of the previous state
			previousState = currentState;
			//call the exit method of the existing state
			if (currentState != null) {
				currentState.Exit();
			}
			//change state to the new state
			currentState = new_state;
			//Set state machine
			currentState.SetStateMachine(this);
			//call the entry method of the new state
			currentState.Enter();
		}
		
		//change state back to the previous state
		public void RevertToPreviousState()
		{
	   		ChangeState(previousState);
		}

		//returns true if the current state’s type is equal to the type of the
		//class passed as a parameter.
		public bool IsInState<S>() where S : State<T>
		{
			if (currentState == null) {
				return false;
			}

			return currentState.GetType() == typeof(S);
		}

		public void TriggerNotice(StateNotice stateNotice)
		{
			noticeQueue.Trigger(stateNotice);
		}

		// HandleNotices
		public void HandleNotices()
		{
			StateNotice stateNotice;
			while (noticeQueue.Dequeue(out stateNotice)) {
				if (currentState != null && currentState.OnNotice(stateNotice)) {
					continue;
				}

				if (globalState != null) {
					globalState.OnNotice(stateNotice);
				}
			}
		}
		
		// Destroy StateMachine
		public void Destroy()
		{
			// exit current state
			if (currentState != null) {
				currentState.Exit();
			}

			// exit global state
			if (globalState != null) {
				globalState.Exit();
			}
		
			// clean up referencies
			globalState = null;
			previousState = null;
			currentState = null;
			owner = default(T);
		}
	}
}
