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
            pnlWelcome.Visible = true;
            pnlWelcome.BringToFront();           
        }
        Autopark _Autopark;
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
            ilistBasement.Images.Clear();
            ilistFirstFlat.Images.Clear();
            ilistSecondFlat.Images.Clear();
            _Autopark = new Autopark(new Stack(15), new Queue(15), new LinkedList());
            Random r = new Random();
            Car C;
            Node n;
            for (int i = 0; i < _Autopark.Basement.size; i++)
            {
                C = new Car() { id = i, carType = r.Next(1,14) };
                n = new Node() { Data = C };
                _Autopark.SecondFlat.InsertLast(n);
                ilistSecondFlat.Images.Add((n.Data.id).ToString(), Image.FromFile(n.Data.imageUrl));
            }
            for (int i = 0; i < _Autopark.Basement.size; i++)
            {
                C = new Car() { id = i + 15, carType = 1 };
                _Autopark.FirstFlat.Insert(C);
                ilistFirstFlat.Images.Add((_Autopark.FirstFlat.Data[i].id).ToString(), Image.FromFile(_Autopark.FirstFlat.Data[i].imageUrl));
            }
            for (int i = 0; i < _Autopark.Basement.size; i++)
            {
                C = new Car() { id = i + 30, carType = 3 };
                _Autopark.Basement.Push(C);
                ilistBasement.Images.Add((_Autopark.Basement.Data[i].id).ToString(), Image.FromFile(_Autopark.Basement.Data[i].imageUrl));
            }
        }

        private void btnCikar_Click(object sender, EventArgs e)
        {
            // show the informations
            pnlInformations.Visible = true;

            // First Flat Operations inorder: show the picture of leaving car, show the information label, remove the image from list
            int imageIndex = ilistFirstFlat.Images.IndexOfKey(_Autopark.FirstFlat.Data[_Autopark.FirstFlat.front].id.ToString());
            if( imageIndex != -1)
            {
                picLeavingCar.BackgroundImage = ilistFirstFlat.Images[imageIndex];
                lblLeavingCarInfo.Text = "45 CCO " + _Autopark.FirstFlat.Peek().id + " left the car park.";
            }
            ilistFirstFlat.Images.RemoveByKey(_Autopark.FirstFlat.Peek().id.ToString());

            // Let the front car left the car park.
            _Autopark.deleteCar();
            if (!_Autopark.FirstFlat.isEmpty())
            {
                // ** ImageList-ListView Operations  **
                int deletedCarId = _Autopark.FirstFlat.Data[_Autopark.FirstFlat.rear].id;
                if (deletedCarId < 15 ||deletedCarId == 0)
                {
                    imageIndex = ilistSecondFlat.Images.IndexOfKey(deletedCarId.ToString());
                    if(imageIndex != -1)
                    {
                        ilistFirstFlat.Images.Add(deletedCarId.ToString(),ilistSecondFlat.Images[imageIndex]);
                        picQueuedCar.BackgroundImage = ilistSecondFlat.Images[imageIndex];
                        ilistSecondFlat.Images.RemoveByKey(deletedCarId.ToString());
                    }
                    if(ilistSecondFlat.Images.Empty)
                    {
                        lvwSecondFlat.Visible = false;
                    }

                }
                if (deletedCarId > 29)
                {
                    imageIndex = ilistBasement.Images.IndexOfKey(deletedCarId.ToString());
                    if(imageIndex != -1)
                    {
                        ilistFirstFlat.Images.Add(deletedCarId.ToString(), ilistBasement.Images[imageIndex]);
                        picQueuedCar.BackgroundImage = ilistBasement.Images[imageIndex];
                        ilistBasement.Images.RemoveByKey(deletedCarId.ToString());
                    }
                    if(ilistBasement.Images.Empty)
                    {
                        lvwBasement.Visible = false;
                    }
                }
                lblInlinedCarInfo.Visible = true;
                lblInlinedCarInfo.Text = "45 CCO " +_Autopark.FirstFlat.Data[_Autopark.FirstFlat.rear].id + " added to 1. Flat.";
            }
            else
            {
                pnlInformations.Visible = false;
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
            pnlTest.Visible = false;
            pnlInformations.Visible = false;
            pnlManual.Visible = true;
            lblHeader.Visible = true;
            lblHeader.Text = "Manual Autopark";
            pnlManual.BringToFront();
            if (btnEmptyNotification.Visible == true || _Autopark.Basement.count < _Autopark.Basement.size || _Autopark.SecondFlat.count < _Autopark.Basement.size)
            {
                _Autopark = new Autopark(new Stack(15), new Queue(15), new LinkedList());
                Form1_Load(this, null);
            }
            Listele();
        }

        private void dpdSelection_onItemSelected(object sender, EventArgs e)
        {
            _Autopark.SecondFlat.interval = Convert.ToInt32(dpdSelection.selectedValue);
        }
        Autopark TestedPark;
        private void btnTest_Click(object sender, EventArgs e)
        {
            lblHeader.Text = "Automated Autopark";
            pnlWelcome.Visible = false;
            pnlManual.Visible = false;
            btnEmptyNotification.Visible = false;
            pnlTest.Visible = true;
            pnlTest.BringToFront();
            pbLoading.Value = 1;
            TestedPark = new Autopark(new Stack(15), new Queue(15), new LinkedList());
            pbLoading.Value = 15;
            FakeData(TestedPark);
            pbLoading.Value = 35;
            lblCompName.Text = HardwareInfo.GetComputerName();
            pbLoading.Value = 45;
            lblCpuName.Text = HardwareInfo.GetCPUManufacturer();
            pbLoading.Value = 55;
            lblRam.Text = HardwareInfo.GetPhysicalMemory();
            pbLoading.Value = 65;
            lblOS.Text = HardwareInfo.GetOSInformation();
            pbLoading.Value = 75;
            lblOneTime.Text = ((int)TestEt()).ToString();
            pbLoading.Value = 85;
            lblFiveSecs.Text = "5.000";
            pbLoading.Value = 95;
            lblQuantity.Text = ((int)(5000 / TestEt())).ToString()+"x";
            pbLoading.Value = 99;
            lblOneQuantity.Text = "1x";
            pbLoading.Value = 100;
            pnlLoading.Visible = false;         
        }
        public double TestEt()
        {
            DateTime start = DateTime.Now;
            while ((TestedPark.FirstFlat.isEmpty() && TestedPark.SecondFlat.isEmpty() && TestedPark.Basement.isEmpty()) != true)
            {
                TestedPark.deleteCar();
            }
            DateTime stop = DateTime.Now;
            FakeData(TestedPark);
            return stop.Subtract(start).TotalMilliseconds;
        }
        public void FakeData(Autopark _Park)
        {
            Random r = new Random();
            Car C;
            Node n;
            for (int i = 0; i < _Park.Basement.size; i++)
            {
                C = new Car() { id = i, carType = 2 };
                n = new Node() { Data = C };
                _Park.SecondFlat.InsertLast(n);
            }
            for (int i = 0; i < _Park.Basement.size; i++)
            {
                C = new Car() { id = i + 15, carType = 1 };
                _Park.FirstFlat.Insert(C);
            }
            for (int i = 0; i < _Park.Basement.size; i++)
            {
                C = new Car() { id = i + 30, carType = 3 };
                _Park.Basement.Push(C);
            }
        }
    }
}
