using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class MessageParser
    {
        public String Parse(String msg)
        {
            string[] msgArray = msg.Split(',');
            int msgType = System.Int32.Parse(msgArray[0]);


            switch (msgType)
            {
                case 1:
                    //getRestaurants
                    return "2,A,B,C,D,E";
                default:
                    break;
            }

            return "";

        }
    }
}
