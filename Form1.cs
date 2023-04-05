using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT07
{
    public partial class Form1 : Form
    {
        private string selectId;
        public Form1()
        {
            InitializeComponent();
            ShowDGV();
            setCBBSortBy();
        }
        public void ShowDGV()
        {
            DBQL_SVDataContext db = new DBQL_SVDataContext();
            var s = db.SVs.Select(p => new { p.MSSV, p.NameSV, p.LopSH.NameLop, p.NS, p.DTB, p.Gender, p.Photo, p.HB, p.CCCD })
                .OrderBy(p => p.MSSV);
            dataGridView1.DataSource = s;
        }
        public void setCBBSortBy()
        {
            var n = dataGridView1.Columns.Cast<DataGridViewColumn>()
                    .Select(p => p.HeaderText).ToList();
            foreach (string col in n)
            {
                cbbSortBy.Items.Add(col);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(this);
            f.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chưa chọn hàng rồi kìa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Form2 f = new Form2(this, selectId);
                f.Show();
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                DataGridViewRow r = dataGridView1.Rows[e.RowIndex];
                selectId = r.Cells[0].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DBQL_SVDataContext db = new DBQL_SVDataContext();
                foreach (DataGridViewRow i in dataGridView1.SelectedRows)
                {
                    string id = i.Cells[0].Value.ToString();
                    var s = db.SVs.Where(p => p.MSSV == id).FirstOrDefault();
                    db.SVs.DeleteOnSubmit(s);
                    db.SubmitChanges();
                }
                dataGridView1.DataSource = db.SVs.ToList();
            }
            else
            {
                MessageBox.Show("Chưa chọn hàng rồi kìa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            DBQL_SVDataContext db = new DBQL_SVDataContext();
            string na = txtSearch.Text;
            var s = db.SVs.Where(p => p.NameSV.Contains(na))
                .Select(p => new { p.MSSV, p.NameSV, p.LopSH.NameLop, p.NS, p.DTB, p.Gender, p.Photo, p.HB, p.CCCD })
                .OrderBy(p => p.DTB).ThenBy(p => p.MSSV);
            dataGridView1.DataSource = s;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DBQL_SVDataContext db = new DBQL_SVDataContext();
            string na = txtSearch.Text;
            var s = db.SVs.Where(p => p.NameSV.Contains(na))
                .Select(p => new { p.MSSV, p.NameSV, p.LopSH.NameLop, p.NS, p.DTB, p.Gender, p.Photo, p.HB, p.CCCD });
            dataGridView1.DataSource = s;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
