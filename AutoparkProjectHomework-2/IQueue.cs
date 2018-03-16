using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkProjectHomework_2
{
    interface IQueue
    {
        void Insert(Car C);
        Car Remove();
        Car Peek();
        Boolean isEmpty();

    }
}
