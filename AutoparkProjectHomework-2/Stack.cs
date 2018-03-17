using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkProjectHomework_2
{
    public class Stack : ArrayBase, IStack
    {
        public int top { get; set; }
        public Stack(int size):base(size)
        {
            top = -1;
        }
        public void Push(Car C)
        {
            C.flatName = "basement";
            C.setUrl();
            if(this.Data.Length == top+1)
            {
                throw new Exception("This is an overflow error." + Environment.NewLine + "You can't add an item from a full array.");
            }
            count++;
            Data[++top] = C;
        }

        public Car Pop()
        {
            if(isEmpty())
            {
                throw new Exception("This is an underflow error." + Environment.NewLine + "You can't delete an item from an empty stack.");
            }
           count--;
           return Data[top--];
        }

        public Car Peek()
        {
            return Data[top];
        }

        public bool isEmpty()
        {
            return top == -1 ? true : false;
        }
    }
}
