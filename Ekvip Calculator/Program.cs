// See https://aka.ms/new-console-template for more information
using Ekvip_Calculator;
using System.Text.RegularExpressions;

    //Input
    string equation = "";

    Console.WriteLine("Please write your equation");

    equation=Console.ReadLine();
    
    
    if (equation != null)
    {
        //Check for if its the string "Red Button", if it is call the function to generate a random Ying Hex   
        if (equation == "Red Button")
        {
            YijngHex yijngHex = new YijngHex();
            Console.WriteLine(yijngHex.RandHex());
        }
        else 
        {
            try
            {
                Calculator calculator = new Calculator(equation);
                float res = calculator.Result();

                //Check if the equation had a division by zero
                if (float.IsInfinity(res) || float.IsNaN(res))
                {
                    Console.WriteLine("Can't divide by zero");
                }
                //Write out the result if it is smaller or equal to zero
                else if (res <= 4)
                {
                    Console.WriteLine(res);
                }
                //Write out "A Suffusion of Yellow" if it's greater than 4
                else
                {
                    Console.WriteLine("A Suffusion of Yellow");
                }
            }
            //If there was an invalid input of the equation the program will throw an exception
            catch (Exception e)
            {
                Console.WriteLine("Please enter a valid equation: "+e.Message);
            }
        
        }
    
    }


