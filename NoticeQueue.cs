using System;
using System.Collections.Generic;

namespace GameLib
{
	public class NoticeQueue
	{
		private Queue<StateNotice> notices = new Queue<StateNotice>();
		
		public void Trigger(StateNotice stateNotice)
		{
			notices.Enqueue(stateNotice);
		}
		
		public bool Dequeue(out StateNotice stateNotice)
		{
			if(notices.Count == 0){
				stateNotice = null;
				return false;
			}
			stateNotice = notices.Dequeue();
			return true;
		}
	}
}

