using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekvip_Calculator
{
    internal class Token
    {
        public enum TokenType {
            Dummy,
            Number,
            Operator,
            LeftBracket,
            RightBracket
        }
        public enum Associativity{
            Left,
            Right
        }

        TokenType tokenType= TokenType.Dummy;

        float val = 0;

        char symbol = ' ';
        Associativity associativity = Associativity.Left;
        int presedence = 0;
        int parameterCount = 0;
        
        public Token() { }

        public void SetValues(char sym, Associativity assc, int pres, int paramC)
        {
            symbol = sym;
            associativity = assc;
            presedence = pres;
            parameterCount = paramC;
        }

        public TokenType GetTokenType {
            get {
                return tokenType;
            }  
        }
        public Associativity GetAssociativity
        {
            get
            {
                return associativity;
            }
        }
        public int GetPresedence
        {
            get {
                return presedence;
            }
        }
        public int GetParameterCount {
            get
            {
                return parameterCount;
            }
        }

        public char Symbol 
        {
            get 
            {  
                return symbol; 
            } 
        }

        public float GetVal 
        { 
            get
            {
                return val;
            }
            set 
            {
                val = value;
            }
        }


        public static Token StringToToken(string str)
        {
            Token t = new Token();
            if (float.TryParse(str, out t.val))
            {
                t.tokenType = TokenType.Number;
            }
            else if (str == "(")
            {
                t.tokenType = TokenType.LeftBracket;
            }
            else if (str == ")")
            {
                t.tokenType = TokenType.RightBracket;
            }
            else 
            {
                switch (str)
                {
                    case "+":
                        t.tokenType = TokenType.Operator;
                        t.symbol = '+';
                        t.associativity = Associativity.Left;
                        t.presedence = 10;
                        t.parameterCount = 2;
                        break;
                    case "-":
                        t.tokenType = TokenType.Operator;
                        t.symbol = '-';
                        t.associativity = Associativity.Left;
                        t.presedence = 10;
                        t.parameterCount = 2;
                        break;
                    case "*":
                        t.tokenType = TokenType.Operator;
                        t.symbol = '*';
                        t.associativity = Associativity.Left;
                        t.presedence = 20;
                        t.parameterCount = 2;
                        break;
                    case "/":
                        t.tokenType = TokenType.Operator;
                        t.symbol = '/';
                        t.associativity = Associativity.Left;
                        t.presedence = 20;
                        t.parameterCount = 2;
                        break;
                    case "%":
                        t.tokenType = TokenType.Operator;
                        t.symbol = '%';
                        t.associativity = Associativity.Left;
                        t.presedence = 20;
                        t.parameterCount = 2;
                        break;
                    default:
                        throw new Exception("Unknown symbol");
                }
            }
            return t;
        }

    }
}
