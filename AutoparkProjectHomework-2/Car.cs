using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkProjectHomework_2
{
    public class Car
    {
        public int carType { get; set; }
        public string flatName { get; set; }
        public string imageUrl { get; set; }
        public int id { get; set; }

        public void setUrl()
        {
            this.imageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\assets\img\cars\car-"+carType+".png");
        }
    }
}
