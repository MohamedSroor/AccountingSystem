using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Guna.UI2.WinForms;

namespace الشغل_final
{
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void MaxBtn_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized; 
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

         private void CustomerBtn_Click(object sender, EventArgs e)
        {
            // عرض شاشة الموردين
            CustomerScreen newForm = new CustomerScreen();
            // إعداد النافذة الجديدة لتكون متناسبة مع اللوحة
            newForm.TopLevel = false;  
            newForm.FormBorderStyle = FormBorderStyle.None; 
            newForm.Dock = DockStyle.Fill; // ملء اللوحة بالكامل

            // مسح محتويات اللوحة وإضافة النافذة الجديدة
            guna2Panel7.Controls.Clear(); 
            guna2Panel7.Controls.Add(newForm); 
            newForm.Show();
        }
       

        private void SuppliersBtn_Click(object sender, EventArgs e)
        {
            // عرض شاشة الموردين
            SuppliersScreen newForm = new SuppliersScreen();
            // إعداد النافذة الجديدة لتكون متناسبة مع اللوحة
            newForm.TopLevel = false;  
            newForm.FormBorderStyle = FormBorderStyle.None; 
            newForm.Dock = DockStyle.Fill; 

            // مسح محتويات اللوحة وإضافة النافذة الجديدة
            guna2Panel7.Controls.Clear(); 
            guna2Panel7.Controls.Add(newForm); 
            newForm.Show();
        }

        private void ItemsBtn_Click(object sender, EventArgs e)
        {
            // عرض شاشة الموردين
            ItemsScreen newForm = new ItemsScreen();
            // إعداد النافذة الجديدة لتكون متناسبة مع اللوحة
            newForm.TopLevel = false;
            newForm.FormBorderStyle = FormBorderStyle.None;
            newForm.Dock = DockStyle.Fill;

            // مسح محتويات اللوحة وإضافة النافذة الجديدة
            guna2Panel7.Controls.Clear();
            guna2Panel7.Controls.Add(newForm);
            newForm.Show();
        }

        private void StoreBtn_Click(object sender, EventArgs e)
        {
            // عرض شاشة الموردين
            StoreScreen newForm = new StoreScreen();
            // إعداد النافذة الجديدة لتكون متناسبة مع اللوحة
            newForm.TopLevel = false;
            newForm.FormBorderStyle = FormBorderStyle.None;
            newForm.Dock = DockStyle.Fill;

            // مسح محتويات اللوحة وإضافة النافذة الجديدة
            guna2Panel7.Controls.Clear();
            guna2Panel7.Controls.Add(newForm);
            newForm.Show();
        }

    }

}
