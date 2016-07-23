using System;
using System.Collections;
using System.Collections.Generic;

namespace Manticore
{
	public class Word
	{
		public string val;
		public int line;
		public int col;
		public Word (string val, int line=-1, int col=-1)
		{
			this.val = val;
			this.line = line;
			this.col = col;
		}
	}

	public class Lexer
	{
		List<Word> tokens;
		int place = 0;
		public string next
		{
			get
			{
				if (place + 1 <= tokens.Count)
					return tokens [place++].val;
				else
					return "\0";
			}
		}

		public int count
		{
			get
			{
				return tokens.Count;
			}
		}

		public Lexer (string text = "")
		{
			tokens = new List<Word> ();
			int line = 1;
			int col = 0;

			for (int i = 0; i < text.Length; i++)
			{
				if (char.IsLetterOrDigit (text [i]) || text [i] == '_' || text[i] == '.')
				{
					string val = "";
					while (char.IsLetterOrDigit (text [i]) || text [i] == '_' || text[i] == '.')
					{
						val += text [i++];
						col++;
					}
					i--;
					tokens.Add (new Word (val, line, col));
				}
				else if (text [i] == '"')
				{
					col++;
					i++;
					string val = "\"";
					while (true)
					{
						if (text [i] == '\\' && text [i + 1] == '"')
						{
							val += "\\";
							val += '"';
							i+=2;
							col += 2;
							continue;
						}
						else if (text [i] == '"')
						{
							val += '"';
							col++;
							break;
						}
						val += text [i];
						i++;
						col++;
					}

					tokens.Add (new Word (val, line, col));
				}
				else if (text [i] == '\'')
				{
					col++;
					i++;
					string val = "'";
					while (true)
					{
						if (text [i] == '\\' && text [i + 1] == '\'')
						{
							val += "\\";
							val += '\'';
							i+=2;
							col += 2;
							continue;
						}
						else if (text [i] == '\'')
						{
							val += '\'';
							col++;
							break;
						}

						val += text [i];
						i++;
						col++;
					}

					tokens.Add (new Word (val, line, col));
				}
				else if (char.IsWhiteSpace (text [i]))
				{
					if (text [i] == '\n')
					{
						col = 0;
						line++;
					}
					else
					{
						col++;
					}
				}
				else if (char.IsSymbol (text [i]) || char.IsPunctuation (text [i]))
				{
					tokens.Add (new Word (text[i].ToString (), line, ++col));
				}
				else
				{
					throw new Exception ("Unknown character, '" + text [i] + "'");
				}
			}
		}

		public void Add (string val)
		{
			tokens.Add (new Word (val));
		}

		public void Add (Word val)
		{
			tokens.Add (val);
		}

		public Word this[int i]
		{

			get {return tokens [i];}
			set {tokens [i] = value;}
		}

		public override string ToString ()
		{
			string o = "";
			for (int i = 0; i < tokens.Count; i++)
				o += tokens [i].val + ": line, " + tokens [i].line + ", col, " + tokens [i].col + '\n';
			return o;
		}
	}
}

