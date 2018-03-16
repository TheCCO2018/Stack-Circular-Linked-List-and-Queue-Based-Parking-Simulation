using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkProjectHomework_2
{
    public class LinkedList : ICircle, IJosephus
    {
        public Node front { get; set; }
        public Node rear { get; set; }
        public int count { get; set; }
        public int interval { get; set; }
        public LinkedList()
        {
            this.count = 0;
            this.interval = 1;
        }

        public int ResultJosephus(int carCount, int interval)
        {
           if(carCount == 1)
           {
               return 0;
           }
           int[] Jewisharray = new int[carCount];
            int Livejewish = carCount;
            int counter = 0;
            int Jewis=-1;
            for (int i = 0; i < carCount; i++)
            {
                Jewisharray[i] = 1;
            }
            int j = 0;
            while(Livejewish != 1)
            {
                if(j==carCount)
                j = 0;
                    
                if(Jewisharray[j] == 1)
                {
                    if(counter == interval)
                    {
                        Jewisharray[j] = 0;
                        counter = 0;
                        Livejewish--;          

                    }
                    else
                    counter++;
                }
                j++;

            }
           
            for (int i = 0; i < carCount; i++)
            {
                if(Jewisharray[i]==1)                  
                Jewis = i;  
            }
            return Jewis;
        }

        public void InsertLast(Node N)
        {
            N.Data.flatName = "secondFlat";
            N.Data.setUrl();
            if(front == null)
            {
                front = N;
                rear = N;
                rear.Next = front;// TODO - Dikkat
            }
            else
            {
               
                rear.Next = N;
                N.Next = front;
                rear = N;
            }
            count++;
        }

        public Node Remove()
        {
            if(isEmpty())
            {
                throw new Exception("This Linked List is Empty." + Environment.NewLine + "You can't delete any data from this list.");
            }
            int removeDataindex = ResultJosephus(count, interval);
            Node last = front;
            Node temp = null;
            if(removeDataindex == 0)
            {
                front = front.Next;
                temp = front;
                rear.Next = front;
                if (count == 1)
                {
                    front = null;
                }
                count--;
                return temp;
            }
            else
            {
                int i = 0;
                while (last.Next != front)
                {
                    if (i == removeDataindex-1)
                    {
                        temp = last.Next;
                        last.Next = temp.Next;
                        if (removeDataindex == count - 1)
                        {
                            rear = last;
                        }
                        break;
                    }
                    i++;
                    last = last.Next;
                }
                count--;
                return temp;
            }     
        }

        public bool isEmpty()
        {
            return (count == 0) ? true : false;
        }
        public string ValueOf()
        {
            string temp = "";
            Node last = front;
            if(count>0)
            {
                while(last.Next != front )
                {
                    temp += last.Data.id+"-";
                    last = last.Next;
                }
                temp += rear.Data.id;
            }
            else
            {
                temp += "Bu kat boştur.";
            }
            return temp;
        }
    }
}
