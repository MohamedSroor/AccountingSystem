using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Guna.UI2.WinForms;

namespace الشغل_final
{
    public partial class ElHashimiGroup : Form
    {
      
        public ElHashimiGroup()
        {
            InitializeComponent();
            OpenHomePage();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void OpenHomePage()
        {
            // فتح الشاشة الرئيسية في الـ Panel
            HomeScreenUserControl homePage = new HomeScreenUserControl();
            homePage.Dock = DockStyle.Fill;
            LoadUserControl(homePage);

            // تفعيل زر الـ HomeBtn
            HomeBtn.Checked = true;
        }

        private void HomeBtn_Click(object sender, EventArgs e)
        {
            OpenHomePage();
        }

        private void LoadUserControl(UserControl userControl)
        {
            guna2Panel07.Controls.Clear(); // مسح أي عناصر موجودة في الـ Panel
            userControl.Dock = DockStyle.Fill; // ملء الـ Panel بالكامل
            guna2Panel07.Controls.Add(userControl); // إضافة الـ UserControl للـ Panel
        }

        private void CustomerBtn_Click(object sender, EventArgs e)
        {
            // عرض شاشة الموردين
            CustomerScreenUserControl CustomerScreen = new CustomerScreenUserControl();
           
            CustomerScreen.Dock = DockStyle.Fill;
            LoadUserControl(CustomerScreen);
        }
       

        private void SuppliersBtn_Click(object sender, EventArgs e)
        {
            // عرض شاشة الموردين
            SuppliersScreenUserControl SuppliersScreen = new SuppliersScreenUserControl();
            SuppliersScreen.Dock = DockStyle.Fill;
            LoadUserControl(SuppliersScreen);
        }

        private void ItemsBtn_Click(object sender, EventArgs e)
        {
            // عرض شاشة الموردين
            ItemsScreenUserControl ItemsScreen = new ItemsScreenUserControl();
            // إعداد النافذة الجديدة لتكون متناسبة مع اللوحة
            ItemsScreen.Dock = DockStyle.Fill;
            LoadUserControl(ItemsScreen);
        }

        private void StoreBtn_Click(object sender, EventArgs e)
        {
            // عرض شاشة الموردين
            StoreScreenUserControl StoreScreen = new StoreScreenUserControl();
            StoreScreen.Dock = DockStyle.Fill;
            LoadUserControl(StoreScreen);
        }

    }
    
}
