using System;

using System.Collections;
using System.Collections.Generic;

namespace Manticore
{
	public class ParserRuleException : Exception
	{
		public ParserRuleException (string message)
		{
			Console.WriteLine (message);
			Environment.Exit (1);
		}
	}

	public class Rule
	{
		public string name;
		string[] rules;

		public Rule (string name, Lexer tmp, Language parent)
		{
			this.name = name;
			//Console.WriteLine (tmp);
			string token;
			List<string> rules = new List<string> ();
 			while ((token = tmp.next) != "\0")
			{

				if (token [0] == '\'')
					rules.Add (token);
				else if (token [0] == '"')
					rules.Add (token);
				else if (token == "<")
				{
					string t = "<" + tmp.next;
					string a = tmp.next;
					if (a != ">")
					{
						throw new ParserRuleException (parent.name + " parser: Error expected character, '>', got \"" + a + "\", in rule \"" + name + "\"");
					}
					rules.Add (t + a);
				}
			}

			this.rules = rules.ToArray ();
		}
	}

	public class Language
	{
		public static CSharp csharp = new CSharp ();

		List<Rule> rules = new List<Rule> ();

		public string name = "Unknown";

		public Language ()
		{
			
		}

		public void addRule (string name, string val)
		{
			Lexer lex = new Lexer (val);
			Lexer l = new Lexer ();
			for (int i = 0; i < lex.count; i++)
			{
				if (lex [i].val == "|")
				{
					rules.Add (new Rule (name, l, this));
					l = new Lexer ();
				}
				else
				{
					l.Add (lex [i]);
				}
			}
			if (l.count > 0)
				rules.Add (new Rule (name, l, this));
		}

		public AST parse (Lexer lex)
		{
			AST ast = new AST ();



			return ast;
		}

		public virtual Lexer preproccess (Lexer lex)
		{
			
		}
	}
}

