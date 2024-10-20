using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace الشغل_final
{
    public partial class StoreScreenUserControl : UserControl
    {
        private List<Product> products = new List<Product>(); // قائمة المنتجات
        private Product currentProduct; // المنتج الحالي
        private int productCodeCounter = 1; // عداد أكواد المنتجات

        public StoreScreenUserControl()
        {
            InitializeComponent();
            InitializeDataGridView();
            txtProductCode.Text = productCodeCounter.ToString(); // تهيئة كود المنتج
        }

        private void InitializeDataGridView()
        {
            guna2DataGridView1.AutoGenerateColumns = false;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("63, 62, 67");
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string name = txtProductName.Text; // اسم المنتج
            string quantityText = txtProductQuantity.Text; // كمية المنتج
            string quantityUnit = guna2TextBox1.Text; // المنتج بالوحدات  
            string unit = checkBoxProductQntUnits.Checked ? "برانيك" : ""; // استخدام الوحدة إذا كانت العلامة محددة

            // التحقق من إدخالات المستخدم
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(quantityText) && !checkBoxProductQntUnits.Checked)
            {
                guna2MessageDialog1.Show("يرجى ملء الحقول الاختيارية على الأقل.");
                return; // الخروج إذا كانت جميع الحقول فارغة
            }

            int quantity = 0; // مقدار الكمية
            if (!string.IsNullOrWhiteSpace(quantityText) && !int.TryParse(quantityText, out quantity))
            {
                guna2MessageDialog1.Show("يرجى إدخال كمية صحيحة.");
                return; // الخروج إذا كانت الكمية غير صحيحة
            }

            // إنشاء نص وحدة الكمية
            string productQntUnits = string.IsNullOrWhiteSpace(quantityUnit) ? "" : quantityUnit.ToString() + " " + unit;

            // إضافة منتج جديد إذا لم يتم تحرير منتج حالي
            if (currentProduct == null)
            {
                products.Add(new Product
                {
                    ProductCode = productCodeCounter.ToString(),
                    ProductName = name,
                    ProductQuantity = quantity,
                    ProductQntUnits = productQntUnits // إضافة وحدة الكمية
                });
                productCodeCounter++;
            }
            else
            {
                // تحديث المنتج الحالي
                currentProduct.ProductName = name;
                currentProduct.ProductQuantity = quantity;
                currentProduct.ProductQntUnits = productQntUnits; // تحديث وحدة الكمية
                currentProduct = null; // إعادة تعيين المنتج الحالي بعد التحرير
            }

            // تحديث DataGridView بالقائمة الحالية للمنتجات
            UpdateProductDataGrid();
            ClearProductInputs();
        }

        private void UpdateProductDataGrid()
        {
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = products;
            guna2DataGridView1.Refresh();
        }

        private void ClearProductInputs()
        {
            txtProductName.Clear();
            txtProductQuantity.Clear(); // إعادة تعيين كمية المنتج
            checkBoxProductQntUnits.Checked = false; // إعادة تعيين العلامة
            txtProductCode.Text = productCodeCounter.ToString(); // إعادة تعيين كود المنتج
        }

        private void EditProduct(int rowIndex)
        {
            var product = guna2DataGridView1.Rows[rowIndex].DataBoundItem as Product;
            if (product != null)
            {
                currentProduct = product;
                txtProductCode.Text = product.ProductCode;
                txtProductName.Text = product.ProductName;
                txtProductQuantity.Text = product.ProductQuantity.ToString(); // تعيين كمية المنتج
                checkBoxProductQntUnits.Checked = !string.IsNullOrWhiteSpace(product.ProductQntUnits); // تحديد حالة العلامة
            }
        }

        private void DeleteProduct(int rowIndex)
        {
            var productToRemove = guna2DataGridView1.Rows[rowIndex].DataBoundItem as Product;
            if (productToRemove != null)
            {
                products.Remove(productToRemove); // حذف المنتج من القائمة
                UpdateProductDataGrid(); // تحديث DataGridView
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
                    EditProduct(e.RowIndex);
                }

                // إذا كان الضغط على عمود المسح
                if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "DeleteIcon")
                {
                    // إظهار رسالة تأكيد باستخدام Guna2MessageDialog
                    var result = guna2MessageDialog2.Show("هل أنت متأكد من أنك تريد المسح؟", "تأكيد المسح");
                    if (result == DialogResult.Yes)
                    {
                        // استدعاء دالة المسح
                        DeleteProduct(e.RowIndex);
                    }
                }
            }
        }
    }

    public class Product
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; } // إضافة خاصية الكمية
        public string ProductQntUnits { get; set; } // إضافة خاصية وحدة الكمية
    }
}
