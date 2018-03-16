using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkProjectHomework_2
{
    interface IStack
    {
        void Push(Car C);
        Car Pop();
        Car Peek();
        Boolean isEmpty();
    }
}
