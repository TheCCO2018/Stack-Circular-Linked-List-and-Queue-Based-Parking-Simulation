using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkProjectHomework_2
{
    public class Queue : ArrayBase, IQueue
    {
        public int front { get; set; }
        public int rear { get; set; }
        public Queue(int size): base(size)
        {
            this.front = -1;
            this.rear = -1;
            this.count = 0;       
        }

        public void Insert(Car C)
        {
            C.flatName = "firstFlat";
            C.setUrl();
            if ((count == size))
            {
                throw new Exception("This Queue is full.");
            }
            else if(front == -1)
            {
                front = 0;
                rear = 0;
                this.Data[rear] = C;

            }
            else if(rear == size-1)
            {
                rear = 0;
                this.Data[rear] = C;
            }
            else
            {
                rear++;
                this.Data[rear] = C;
            }
            count++;
        }

        public Car Remove()
        {
            Car temp = null;
            if(count == 0 )
            {
                throw new Exception("This Queue is empty.");
            }
            else
            {
               temp = Data[front];
               Data[front] = null;
               if (front == rear)
               {
                   front = 0;
                   rear = 0;
               }
                if(front == size-1)
               {
                   front = 0;            
               }
               else
               {
                   front++;
               }
            }
            count--;
            return temp;
            
        }

        public Car Peek()
        {
            return Data[front];
        }

        public bool isEmpty()
        {
            return (count == 0) ? true : false;
        }
        public override string ValueOf()
        {
            string temp = "";
            if (isEmpty())
            {
                temp = "Bu kat boştur.";
            }
            else
            {
                    for (int i = 0; i < size; i++)
                    {
                        if(this.Data[i] != null)
                        temp += Data[i].id + "-";
                    }
            }
            return temp;    
        }
    }
}
