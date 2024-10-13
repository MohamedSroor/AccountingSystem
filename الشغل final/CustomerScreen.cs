using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

namespace الشغل_final
{
    public partial class CustomerScreen : Form
    {
        private List<Customer> customers = new List<Customer>();
        private Customer currentCustomer;
        private int customerCodeCounter = 1; // Counter for customer codes

        public CustomerScreen()
        {
            InitializeComponent();
            InitializeDataGridView(); // Initialize DataGridView columns
            txtCustomerCode.Text = customerCodeCounter.ToString(); // Initialize the customer code
        }

        private void InitializeDataGridView()
        {
            guna2DataGridView1.AutoGenerateColumns = false;

        }

        private void AddGridColumn(string propertyName, string headerText)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = propertyName;
            column.HeaderText = headerText;
            guna2DataGridView1.Columns.Add(column);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string name = txtCustomerName.Text; // Customer name
            string phone = txtCustomerPhone.Text; // Customer phone
            string address = txtCustomerAddress.Text; // Customer address

            // Validate inputs
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(address))
            {
                guna2MessageDialog1.Show("يرجى ملء جميع الحقول.");
                return;
            }

            // Validate phone number format
            if (!Regex.IsMatch(phone, @"^\d+$"))
            {
                guna2MessageDialog1.Show("رقم الهاتف يجب أن يحتوي على أرقام فقط.");
                return;
            }

            // If no current customer is being edited, add a new one
            if (currentCustomer == null)
            {
                customers.Add(new Customer
                {
                    CustCode = customerCodeCounter.ToString(),
                    CustName = name,
                    CustPhone = phone,
                    CustAddress = address,
                    CustAccount = 0
                });
                customerCodeCounter++;
            }
            else
            {
                // Update existing customer
                currentCustomer.CustName = name;
                currentCustomer.CustPhone = phone;
                currentCustomer.CustAddress = address;
                currentCustomer = null; // Reset currentCustomer after editing
            }

            // Update DataGridView with the current list of customers
            UpdateCustomerDataGrid();
            ClearCustomerInputs();
        }

        private void UpdateCustomerDataGrid()
        {
            guna2DataGridView1.DataSource = null; // إفراغ مصدر البيانات الحالي
            guna2DataGridView1.DataSource = customers; // تعيين قائمة العملاء كمصدر البيانات
        }

        private void ClearCustomerInputs()
        {
            txtCustomerName.Clear();
            txtCustomerPhone.Clear();
            txtCustomerAddress.Clear();
            txtCustomerCode.Text = customerCodeCounter.ToString(); // Reset the customer code
        }

        private void DeleteCustomer(int rowIndex)
        {
            var customerToRemove = guna2DataGridView1.Rows[rowIndex].DataBoundItem as Customer;
            if (customerToRemove != null)
            {
                customers.Remove(customerToRemove); // حذف العميل من القائمة
                UpdateCustomerDataGrid(); // تحديث DataGridView
            }
        }

        private void EditCustomer(int rowIndex)
        {
            var customer = guna2DataGridView1.Rows[rowIndex].DataBoundItem as Customer;
            if (customer != null)
            {
                currentCustomer = customer; 
                txtCustomerCode.Text = customer.CustCode; 
                txtCustomerName.Text = customer.CustName;
                txtCustomerPhone.Text = customer.CustPhone;
                txtCustomerAddress.Text = customer.CustAddress;
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // التأكد أن المستخدم لم يضغط على عنوان العمود
            if (e.RowIndex >= 0)
            {
                // إذا كان الضغط على عمود التعديل
                if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "EditIcon")
                {
                    // استدعاء دالة التعديل
                    EditCustomer(e.RowIndex);
                }

                // إذا كان الضغط على عمود المسح
                if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "DeleteIcon")
                {
                    // إظهار رسالة تأكيد باستخدام Guna2MessageDialog
                    var result = guna2MessageDialog2.Show("هل أنت متأكد من أنك تريد المسح؟");
                    if (result == DialogResult.Yes)
                    {
                        // استدعاء دالة المسح
                        DeleteCustomer(e.RowIndex);
                    }
                }
            }
        }


        public class Customer
        {
            public string CustCode { get; set; }
            public string CustName { get; set; }
            public string CustPhone { get; set; }
            public string CustAddress { get; set; }
            public decimal CustAccount { get; set; }
        }

        
    }
}
