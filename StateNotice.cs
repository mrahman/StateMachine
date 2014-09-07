using System;
namespace GameLib
{
	public class StateNotice
	{
		public string Title
		{
			get; protected set;
		}
		public object Messages
		{
			get; protected set;
		}

		public StateNotice(string title, object messages = null)
		{
			this.Title = title;
			this.Messages = messages;
		}
	}
}

