using System;

using System.Collections;
using System.Collections.Generic;

using System.IO;

namespace Manticore
{
	public class main
	{
		public static void Main (string[] args)
		{
			if (args.Length == 0)
				throw new Exception ("Error: need input!");
			
			string files = "";

			Lexer lex = new Lexer ();

			for (int i = 0; i < args.Length; i++)
			{
				if (File.Exists (args [i]))
				{
					Lexer newlex = new Lexer (File.ReadAllText (args [i]));
					for (int j = 0; j < newlex.count; j++)
						lex.Add (newlex[j]);
				}
			}

			Console.WriteLine (lex);

			AST tree = Language.csharp.parse (lex);
		}
	}
}

