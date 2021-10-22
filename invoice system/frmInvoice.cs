using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace invoice_system
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("www.Invoice.com");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn d in dgvInvoice.Columns)
            {

                d.DefaultCellStyle.ForeColor = Color.Red;
            }
            dgvInvoice.Columns[3].DefaultCellStyle.BackColor = Color.Blue;


            txtDate.Text = DateTime.Now.ToString("dd/mm/yyyy");
            Dictionary<int, string> itemData = new Dictionary<int, string>();
            itemData.Add(350, "Laptop Dell");
            itemData.Add(400, "Laptop Samsung");
            itemData.Add(750, "Laptop Toshipa");
            itemData.Add(300, "Laptop Acer");
            itemData.Add(1050, "Laptop Mac");
            itemData.Add(409, "Laptop Lenovo");
            itemData.Add(55, "Printer HP");
            itemData.Add(23, "mouse Dell");




            cpxItem.DataSource = new BindingSource(itemData, null);
            cpxItem.DisplayMember = "value";
            cpxItem.ValueMember = "key";
            txtPrice.Text = cpxItem.SelectedValue.ToString();




            txtName.Focus();
            txtName.Select();
            txtName.SelectAll();
        }

        private void txtDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtDate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDate_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                txtDate.ContextMenu = new ContextMenu();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                cpxItem.Focus();

            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cpxItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPrice.Text = cpxItem.SelectedValue.ToString();
        }

        private void cpxItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtQuantity.Focus();
                txtQuantity.SelectAll();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cpxItem.SelectedIndex <= -1) return;

            string c = cpxItem.Text;
            string pr = txtPrice.Text;
            string qu = txtQuantity.Text;
            int reslt = int.Parse(txtPrice.Text) * int.Parse(txtQuantity.Text);
            txtTotal.Text = reslt.ToString();
            string t = txtTotal.Text;
            object[] arr = { c, qu, pr, t };

            dgvInvoice.Rows.Add(arr);

            int T = 0;
            foreach (DataGridViewRow r in dgvInvoice.Rows)
            {
                T += Convert.ToInt32(r.Cells["Total"].Value);

            }
            txtTotal.Text = T.ToString();





        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {


                int result = int.Parse(txtPrice.Text) * int.Parse(txtQuantity.Text);
                txtTotal.Text = result.ToString();



                cpxItem.Focus();
            }
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;

            }

        }

        string oldqty = "1";
        private void dgvInvoice_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvInvoice.CurrentRow != null)
            {
                oldqty = dgvInvoice.CurrentRow.Cells["colQty"].Value.ToString();
            }
        }

        private void dgvInvoice_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvInvoice.CurrentRow != null)
            {
                string newQty = dgvInvoice.CurrentRow.Cells["colQty"].Value+"";

                if (oldqty == newQty) return;
                if (!Regex.IsMatch(newQty, "^\\d+$"))
                {
                    dgvInvoice.CurrentRow.Cells["colQty"].Value = oldqty;

                }
                else
                {
                   
                    int p = Convert.ToInt32(dgvInvoice.CurrentRow.Cells["colPrice"].Value);
                    int r = Convert.ToInt32(newQty);
                    dgvInvoice.CurrentRow.Cells["Total"].Value = (p*r);

                    int T = 0;
                    foreach (DataGridViewRow d in dgvInvoice.Rows)
                    {
                        T += Convert.ToInt32(d.Cells["Total"].Value);

                    }
                    txtTotal.Text = T.ToString();

                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
           
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
        }
    }
}