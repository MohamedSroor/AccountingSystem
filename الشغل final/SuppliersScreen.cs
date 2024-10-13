using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

namespace الشغل_final
{
    public partial class SuppliersScreen : Form
    {
        private List<Supplier> suppliers = new List<Supplier>(); // قائمة الموردين
        private Supplier currentSupplier; // المورد الحالي
        private int supplierCodeCounter = 1; // عداد أكواد الموردين

        public SuppliersScreen()
        {
            InitializeComponent();
            InitializeDataGridView();
            txtSupplierCode.Text = supplierCodeCounter.ToString(); // تهيئة كود المورد
        }

        private void InitializeDataGridView()
        {
            guna2DataGridView1.AutoGenerateColumns = false;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string name = txtSupplierName.Text; // اسم المورد
            string phone = txtSupplierPhone.Text; // هاتف المورد
            string address = txtSupplierAddress.Text; // عنوان المورد

            // التحقق من إدخالات المستخدم
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(address))
            {
                guna2MessageDialog1.Show("يرجى ملء جميع الحقول.");
                return; // الخروج إذا كان أي حقل فارغ
            }

            // التحقق من صحة رقم الهاتف
            if (!Regex.IsMatch(phone, @"^\d+$"))
            {
                guna2MessageDialog1.Show("رقم الهاتف يجب أن يحتوي على أرقام فقط.");
                return;
            }

            // إضافة مورد جديد إذا لم يتم تحرير مورد حالي
            if (currentSupplier == null)
            {
                suppliers.Add(new Supplier
                {
                    SuppCode = supplierCodeCounter.ToString(),
                    SuppName = name,
                    SuppPhone = phone,
                    SuppAddress = address,
                    SuppAccount = 0
                });
                supplierCodeCounter++;
            }
            else
            {
                // تحديث المورد الحالي
                currentSupplier.SuppName = name;
                currentSupplier.SuppPhone = phone;
                currentSupplier.SuppAddress = address;
                currentSupplier = null; // إعادة تعيين المورد الحالي بعد التحرير
            }

            // تحديث DataGridView بالقائمة الحالية للموردين
            UpdateSupplierDataGrid();
            ClearSupplierInputs();
        }

        private void UpdateSupplierDataGrid()
        {
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = suppliers;
            guna2DataGridView1.Refresh();
        }

        private void ClearSupplierInputs()
        {
            txtSupplierName.Clear();
            txtSupplierPhone.Clear();
            txtSupplierAddress.Clear();
            txtSupplierCode.Text = supplierCodeCounter.ToString(); // إعادة تعيين كود المورد
        }

        private void EditSupplier(int rowIndex)
        {
            var supplier = guna2DataGridView1.Rows[rowIndex].DataBoundItem as Supplier;
            if (supplier != null)
            {
                currentSupplier = supplier;
                txtSupplierCode.Text = supplier.SuppCode; // عرض كود المورد ولكن لا يتم تغييره
                txtSupplierName.Text = supplier.SuppName;
                txtSupplierPhone.Text = supplier.SuppPhone;
                txtSupplierAddress.Text = supplier.SuppAddress;
            }
        }

        private void DeleteSupplier(int rowIndex)
        {
            var supplierToRemove = guna2DataGridView1.Rows[rowIndex].DataBoundItem as Supplier;
            if (supplierToRemove != null)
            {
                suppliers.Remove(supplierToRemove); // حذف المورد من القائمة
                UpdateSupplierDataGrid(); // تحديث DataGridView
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
                    EditSupplier(e.RowIndex);
                }

                // إذا كان الضغط على عمود المسح
                if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "DeleteIcon")
                {
                    // إظهار رسالة تأكيد باستخدام Guna2MessageDialog
                    var result = guna2MessageDialog2.Show("هل أنت متأكد من أنك تريد المسح؟", "تأكيد المسح");
                    if (result == DialogResult.Yes)
                    {
                        // استدعاء دالة المسح
                        DeleteSupplier(e.RowIndex);
                    }
                }
            }
        }
    }

    public class Supplier
    {
        public string SuppCode { get; set; }
        public string SuppName { get; set; }
        public string SuppPhone { get; set; }
        public string SuppAddress { get; set; }
        public decimal SuppAccount { get; set; }
    }
}
