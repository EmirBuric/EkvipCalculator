using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Ekvip_Calculator
{
    internal class Calculator
    {
        string equation;
        public Calculator(string str) {
            equation = str;
        }
        //Method to parse the correctly inputed equation from a string using
        //Shunting algorithm and Polish reverse notation
        public float Result()
        {
            
            Token[] tokens = Tokenize(equation);
            tokens=ShuntingYard(tokens);
            float result = Calculate(tokens);
            /*var result = new DataTable().Compute(equation, null);
            float res=Convert.ToSingle(result);*/

            return result;
        }

        private float Calculate(Token[] tokens)
        {
            int index = 0;
            Stack<Token> stack = new Stack<Token>();
            while (index < tokens.Length) 
            {
                Token t= tokens[index];
                if (t.GetTokenType == Token.TokenType.Number)
                {
                    stack.Push(t);
                }
                else if (stack.Count < t.GetParameterCount)
                {
                    throw new Exception("Insuficient values in the equation");
                }
                else 
                {
                    List<Token> list = new List<Token>();
                    for (int i = 0; i < t.GetParameterCount; i++)
                    {
                        list.Add(stack.Pop()); 
                    }
                    stack.Push(EvaluateOperator(t, list));
                }
                index++;
            }
            if (stack.Count == 1)
            {
                return stack.Pop().GetVal;
            }
            else 
            {
                throw new Exception("Too many values were inputed");
            }
        }

        private Token EvaluateOperator(Token t, List<Token> list)
        {
            switch(t.Symbol)
            {
                case '+':
                    list[0].GetVal += list[1].GetVal;
                    break;
                case '-':
                    list[0].GetVal = list[1].GetVal - list[0].GetVal;
                    break;
                case '/':
                    list[0].GetVal = list[1].GetVal / list[0].GetVal;
                    break;
                case '*':
                    list[0].GetVal *= list[1].GetVal;
                    break;
                case '%':
                    list[0].GetVal = list[1].GetVal % list[0].GetVal;
                    break;
                case '_':
                    list[0].GetVal = -list[0].GetVal;
                    break;
                default:
                    throw new Exception("Unknown operator");
            }
            return list[0];
        }

        private Token[] ShuntingYard(Token[] tokens)
        {
            int index = 0;
            Queue<Token> outputQueue = new Queue<Token>();
            Stack<Token> operatorStack = new Stack<Token>();
            while (index < tokens.Length)
            {
                Token t = tokens[index];
                if (t.GetTokenType == Token.TokenType.Number)
                {
                    outputQueue.Enqueue(t);
                }
                else if (t.GetTokenType == Token.TokenType.Operator)
                {
                    while (operatorStack.Count != 0)
                    {
                        Token o2 = operatorStack.Peek();
                        if(o2.GetTokenType!=Token.TokenType.Operator)
                        {
                            break; 
                        }
                        if ((t.GetAssociativity == Token.Associativity.Left && t.GetPresedence == o2.GetPresedence) ||
                            (t.GetPresedence < o2.GetPresedence))
                        {
                            outputQueue.Enqueue(operatorStack.Pop());
                        }
                        else
                        {
                            break;
                        }
                    }
                    operatorStack.Push(t);
                }
                else if (t.GetTokenType == Token.TokenType.LeftBracket)
                {
                    operatorStack.Push(t); 
                }
                else if(t.GetTokenType == Token.TokenType.RightBracket)
                {
                    while (operatorStack.Peek().GetTokenType!= Token.TokenType.LeftBracket)
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                        if(operatorStack.Count == 0) 
                        {
                            throw new Exception("Mismached brackets");
                        }
                    }
                    operatorStack.Pop();
                }
                index++;
            }
            while(operatorStack.Count != 0) 
            {
                if (operatorStack.Peek().GetTokenType == Token.TokenType.LeftBracket ||
                    operatorStack.Peek().GetTokenType == Token.TokenType.RightBracket) 
                {
                    throw new Exception("Mismached brackets");
                }
                outputQueue.Enqueue(operatorStack.Pop());
            }
            return outputQueue.ToArray();
        }

        private Token[] Tokenize(string equation)
        {
            string[] split = SplitTokens(equation);
            List<Token> tokens = new List<Token>();
            foreach (string s in split) {
                Token t= Token.StringToToken(s);
                if(s=="-")
                {
                    if(tokens.Count==0 || tokens[tokens.Count-1].GetTokenType == Token.TokenType.LeftBracket ||
                        tokens[tokens.Count - 1].GetTokenType == Token.TokenType.Operator)
                    {
                        t.SetValues('_', Token.Associativity.Right, 30, 1);
                    }
                }
                tokens.Add(t);
            }
            return tokens.ToArray();
        }

        private string[] SplitTokens(string equation)
        {
            equation = equation.Replace("+", " + ");
            equation = equation.Replace("-", " - ");
            equation = equation.Replace("*", " * ");
            equation = equation.Replace("/", " / ");
            equation = equation.Replace("%", " % ");
            equation = equation.Replace("(", " ( ");
            equation = equation.Replace(")", " ) ");
            equation.Trim();
            while (equation.Contains("  ")) {
                equation = equation.Replace("  ", " ");
            }
            return equation.Split(" ");
        }
    }
}
