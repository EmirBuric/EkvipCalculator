using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekvip_Calculator
{
    internal class YijngHex
    {
        public YijngHex() { }
        char[] yijngArray= {'䷀','䷁','䷂','䷃','䷄','䷅','䷆',	'䷇','䷈','䷉','䷊','䷋','䷌','䷍','䷎','䷏',
                            '䷐'	,'䷑','䷒','䷓','䷔','䷕','䷖','䷗'	,'䷘','䷙','䷚','䷛','䷜','䷝','䷞','䷟',
                            '䷠','䷡','䷢','䷣','䷤','䷥','䷦','䷧','䷨','䷩','䷪','䷫','䷬','䷭','䷮','䷯',
                            '䷠','䷡','䷢','䷣','䷤','䷥','䷦','䷧','䷨','䷩','䷪','䷫','䷬','䷭','䷮','䷯'};
       
        //Method to generate a random Ying Hex using an  char array
        public char RandHex()
        {
            Random random = new Random();
            //int hex = random.Next(0x4DC0, 0x4DFF+1);//Hexadecimal
            //int hex = random.Next(19904, 19968); //Decimal

            //return (char) hex;
            //return Convert.ToChar(hex);
            return yijngArray[random.Next(0,64)];
        }
    }
}
