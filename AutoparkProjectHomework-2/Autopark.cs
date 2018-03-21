using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkProjectHomework_2
{
    public class Autopark
    {
        public Stack Basement { get; set; }
        public Queue FirstFlat { get; set; }
        public LinkedList SecondFlat { get; set; }
        public Autopark(Stack Basement, Queue FirstFlat, LinkedList SecondFlat)
        {
         
            this.Basement = Basement;
            this.FirstFlat = FirstFlat;
            this.SecondFlat = SecondFlat;
        }

        public bool NextFlat()
        {
            Random r = new Random();
            if(r.Next(0,2) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void deleteCar()
        {
            if(!FirstFlat.isEmpty())
            {
                bool flat = NextFlat();
                if (flat == true && !Basement.isEmpty())
                {
                        FirstFlat.Remove();
                        FirstFlat.Insert(Basement.Pop());
                }
                else if(flat == false && !SecondFlat.isEmpty())
                {
                        FirstFlat.Remove();
                        FirstFlat.Insert(SecondFlat.Remove().Data);                    
                }
                else
                {
                    if (Basement.isEmpty() && SecondFlat.isEmpty())
                    this.FirstFlat.Remove();
                }
            }
        }
    }
}
