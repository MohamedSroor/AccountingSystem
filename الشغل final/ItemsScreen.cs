using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace الشغل_final
{
    public partial class ItemsScreenUserControl : UserControl
    {
        private List<Item> items = new List<Item>(); // قائمة العناصر
        private Item currentItem; // العنصر الحالي
        private int itemCodeCounter = 1; // عداد أكواد العناصر

        public ItemsScreenUserControl()
        {
            InitializeComponent();
            InitializeDataGridView();
            txtItemCode.Text = itemCodeCounter.ToString(); // تهيئة كود العنصر
        }

        private void InitializeDataGridView()
        {
            guna2DataGridView1.AutoGenerateColumns = false;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("63, 62, 67");
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string name = txtItemName.Text; // اسم العنصر

            // التحقق من إدخالات المستخدم
            if (string.IsNullOrWhiteSpace(name))
            {
                guna2MessageDialog1.Show("يرجى ملء جميع الحقول.");
                return; // الخروج إذا كان أي حقل فارغ
            }

            // إضافة عنصر جديد إذا لم يتم تحرير عنصر حالي
            if (currentItem == null)
            {
                items.Add(new Item
                {
                    ItemCode = itemCodeCounter.ToString(),
                    ItemName = name
                });
                itemCodeCounter++;
            }
            else
            {
                // تحديث العنصر الحالي
                currentItem.ItemName = name;
                currentItem = null; // إعادة تعيين العنصر الحالي بعد التحرير
            }

            // تحديث DataGridView بالقائمة الحالية للعناصر
            UpdateItemDataGrid();
            ClearItemInputs();
        }

        private void UpdateItemDataGrid()
        {
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = items;
            guna2DataGridView1.Refresh();
        }

        private void ClearItemInputs()
        {
            txtItemName.Clear();
            txtItemCode.Text = itemCodeCounter.ToString(); // إعادة تعيين كود العنصر
        }

        private void EditItem(int rowIndex)
        {
            var item = guna2DataGridView1.Rows[rowIndex].DataBoundItem as Item;
            if (item != null)
            {
                currentItem = item;
                txtItemCode.Text = item.ItemCode;
                txtItemName.Text = item.ItemName;
            }
        }

        private void DeleteItem(int rowIndex)
        {
            var itemToRemove = guna2DataGridView1.Rows[rowIndex].DataBoundItem as Item;
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove); // حذف العنصر من القائمة
                UpdateItemDataGrid(); // تحديث DataGridView
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
                    EditItem(e.RowIndex);
                }

                // إذا كان الضغط على عمود المسح
                if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "DeleteIcon")
                {
                    // إظهار رسالة تأكيد باستخدام Guna2MessageDialog
                    var result = guna2MessageDialog2.Show("هل أنت متأكد من أنك تريد المسح؟", "تأكيد المسح");
                    if (result == DialogResult.Yes)
                    {
                        // استدعاء دالة المسح
                        DeleteItem(e.RowIndex);
                    }
                }
            }
        }
    }

    public class Item
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
    }
}
