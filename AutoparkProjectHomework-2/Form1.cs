using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoparkProjectHomework_2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
           
        }
        Autopark _Autopark;
        LinkedList LL = new LinkedList();
        Stack st = new Stack(15);
        Queue que = new Queue(15);
        public void Listele()
        {
            this.lvwSecondFlat.Items.Clear();
            this.lvwFirstFlat.Items.Clear();
            this.lvwBasement.Items.Clear();
            for (int j = 0; j < this.ilistSecondFlat.Images.Count; j++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = j;
                this.lvwSecondFlat.Items.Add(item);
            }
            for (int i = 0; i < this.ilistFirstFlat.Images.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;
                this.lvwFirstFlat.Items.Add(item);
            }
            for (int i = 0; i < this.ilistBasement.Images.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;
                this.lvwBasement.Items.Add(item);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pnlManual.Visible = false;
            pnlTest.Visible = false;
            _Autopark = new Autopark(st, que, LL);
            Random r = new Random();
            Car C;
            Node n;
            for (int i = 0; i < st.size; i++)
            {
                C = new Car() { id = i, carType = 2 };
                n = new Node() { Data = C };
                _Autopark.SecondFlat.InsertLast(n);
                ilistSecondFlat.Images.Add((n.Data.id).ToString(), Image.FromFile(n.Data.imageUrl));
            }
            for (int i = 0; i < st.size; i++)
            {
                C = new Car() { id = i + 15, carType = 1 };
                _Autopark.FirstFlat.Insert(C);
                ilistFirstFlat.Images.Add((_Autopark.FirstFlat.Data[i].id).ToString(), Image.FromFile(_Autopark.FirstFlat.Data[i].imageUrl));
            }
            for (int i = 0; i < st.size; i++)
            {
                C = new Car() { id = i + 30, carType = 3 };
                _Autopark.Basement.Push(C);
                ilistBasement.Images.Add((_Autopark.Basement.Data[i].id).ToString(), Image.FromFile(_Autopark.Basement.Data[i].imageUrl));
            }
        }
        public void FakeData()
        {
            _Autopark = new Autopark(st, que, LL);
            Random r = new Random();
            Car C;
            Node n;
            for (int i = 0; i < st.size; i++)
            {
                C = new Car() { id = i, carType = 2 };
                n = new Node() { Data = C };
                _Autopark.SecondFlat.InsertLast(n);
            }
            for (int i = 0; i < st.size; i++)
            {
                C = new Car() { id = i + 15, carType = 1 };
                _Autopark.FirstFlat.Insert(C);
            }
            for (int i = 0; i < st.size; i++)
            {
                C = new Car() { id = i + 30, carType = 3 };
                _Autopark.Basement.Push(C);
            }
        }

        private void btnCikar_Click(object sender, EventArgs e)
        {
            ilistFirstFlat.Images.RemoveByKey(_Autopark.FirstFlat.Data[_Autopark.FirstFlat.front].id.ToString());
            _Autopark.deleteCar();
            if (!_Autopark.FirstFlat.isEmpty())
            {
                int deletedCarId = _Autopark.FirstFlat.Data[_Autopark.FirstFlat.rear].id;
                if (deletedCarId < 15)
                {
                    int imageIndex = ilistSecondFlat.Images.IndexOfKey(deletedCarId.ToString());
                    if(imageIndex != -1)
                    {
                        ilistFirstFlat.Images.Add(deletedCarId.ToString(),ilistSecondFlat.Images[imageIndex]);
                        ilistSecondFlat.Images.RemoveByKey(deletedCarId.ToString());
                    }
                    else
                    {
                        lvwSecondFlat.Visible = false;
                    }

                }
                if (deletedCarId > 29)
                {
                    int imageIndex = ilistBasement.Images.IndexOfKey(deletedCarId.ToString());
                    if(imageIndex != -1)
                    {
                        ilistFirstFlat.Images.Add(deletedCarId.ToString(), ilistBasement.Images[imageIndex]);
                        ilistBasement.Images.RemoveByKey(deletedCarId.ToString());
                    }
                    else
                    {
                        lvwBasement.Visible = false;
                    }
                }
            }
            else
            {
                btnEmptyNotification.Visible = true;
                btnCikar.Visible = false;
                lvwFirstFlat.Visible = false;
            }
            Listele();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBasla_Click(object sender, EventArgs e)
        {
            pnlWelcome.Visible = false;
            pnlManual.Visible = true;
            pnlManual.BringToFront();
            Listele();
        }

        private void dpdSelection_onItemSelected(object sender, EventArgs e)
        {
            _Autopark.SecondFlat.interval = Convert.ToInt32(dpdSelection.selectedValue);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            pnlWelcome.Visible = false;
            pnlManual.Visible = false;
            pnlTest.Visible = true;
            btnEmptyNotification.Visible = false;
            pnlTest.BringToFront();
            lblCompName.Text = HardwareInfo.GetComputerName();
            lblCpuName.Text = HardwareInfo.GetCPUManufacturer();
            lblRam.Text = HardwareInfo.GetPhysicalMemory();
            lblOS.Text = HardwareInfo.GetOSInformation();
            lblOneTime.Text = ((int)TestEt()).ToString();
            lblFiveMins.Text = "300.000";
            lblQuantity.Text = ((int)(300000 / TestEt())).ToString()+"x";
          
        }
        public double TestEt()
        {
            DateTime start = DateTime.Now;
            while ((_Autopark.FirstFlat.isEmpty() && _Autopark.SecondFlat.isEmpty() && _Autopark.Basement.isEmpty()) != true)
            {
                _Autopark.deleteCar();
            }
            DateTime stop = DateTime.Now;
            FakeData();
            return stop.Subtract(start).TotalMilliseconds;
        }
    }
}
