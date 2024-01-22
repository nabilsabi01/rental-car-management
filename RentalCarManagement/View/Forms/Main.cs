using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using RentalCarManagement.View.UserControls;

namespace RentalCarManagement.View.Forms
{
    public partial class Main : Form
    {
        //Fields
        private IconButton currentBtn;
        private readonly Panel leftBorderBtn;

       

        private struct RGBColors
        {
            public static Color color3 = Color.FromArgb(94, 148, 255);
        }

        public Main()
        {
            InitializeComponent();

            

            leftBorderBtn = new Panel
            {
                Size = new Size(7, 60)
            };
            panel1.Controls.Add(leftBorderBtn);
            timer1.Start();
        }

        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(43, 45, 66);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(58, 63, 79);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        public void AddUserControl(UserControl userControl)
        {
            pnlMenu.Controls.Clear();
            userControl.Dock = DockStyle.Fill;
            pnlMenu.Controls.Clear();
            pnlMenu.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            Dashboard home = new Dashboard();
            AddUserControl(home);
        }

        private void BtnCustomer_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            ListCustomers customerform = new ListCustomers();
            AddUserControl(customerform);
        }

        private void BtnCar_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            ListCars carform = new ListCars();
            AddUserControl(carform);
        }

        private void BtnReservation_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            ListReservations resform = new ListReservations();
            AddUserControl(resform);
        }

        private void BtnPayment_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            ListPayments frm = new ListPayments();
            AddUserControl(frm);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString();
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
