using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkProjectHomework_2
{
    public abstract class ArrayBase
    {
        public int size { get; set; }
        public Car[] Data { get; set; }
        public int count { get; set; }
        public ArrayBase(int size)
        {
            this.size = size;
            Data = new Car[size];
        }
    }
}
