using System;
using System.Linq;
using System.Windows.Forms;

namespace الشغل_final
{
    public partial class HomeScreenUserControl : UserControl
    {
        public HomeScreenUserControl()
        {
            InitializeComponent();
        }

        private void SuppliersBtn_Click(object sender, EventArgs e)
        {
            SuppliersScreenUserControl suppliersScreen = new SuppliersScreenUserControl();
            LoadUserControl(suppliersScreen);
        }

        private void StoreBtn_Click(object sender, EventArgs e)
        {
            StoreScreenUserControl storeScreen = new StoreScreenUserControl();
            LoadUserControl(storeScreen);
        }

        private void CustomerBtn_Click(object sender, EventArgs e)
        {
            CustomerScreenUserControl customerScreen = new CustomerScreenUserControl();
            LoadUserControl(customerScreen);
        }

        private void LoadUserControl(UserControl userControl)
        {
            Form parentForm = this.FindForm();
            Panel mainPanel = (Panel)parentForm.Controls.Find("guna2Panel07", true).FirstOrDefault();

            // Clear existing controls and load the new UserControl
            mainPanel.Controls.Clear();
            userControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(userControl);

        }


    }
}
