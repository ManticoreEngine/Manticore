using System;

namespace Manticore
{
	public class CSharp : Language
	{
		public CSharp ()
		{
			name = "C#";
			//addRule ("expression", "'('')'");

			addRule ("test", "'(' ')' | '(' <test> ')'");
		}


	}
}

