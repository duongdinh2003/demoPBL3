using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT07
{
    public partial class Form2 : Form
    {
        private Form1 f;
        public Form2()
        {
            InitializeComponent();
            setCBBLopSH();
        }

        public Form2(Form1 f)
        {
            InitializeComponent();
            setCBBLopSH();
            this.f = f;
        }
        public Form2(Form1 f, string id)
        {
            InitializeComponent();
            setCBBLopSH();
            this.f = f;
            txtMSSV.Text = id;
            txtMSSV.Enabled = false;
        }
        public void setCBBLopSH()
        {
            DBQL_SVDataContext db = new DBQL_SVDataContext();
            foreach (LopSH i in db.LopSHes)
            {
                cbbLopSH.Items.Add(i.NameLop);
            }    
        }
        public string getIDLop(string namelop)
        {
            DBQL_SVDataContext db = new DBQL_SVDataContext();
            string id = "";
            foreach (LopSH i in db.LopSHes)
            {
                if (namelop == i.NameLop)
                {
                    id = i.IDLop;
                    break;
                }    
            }    
            return id;
        }
        public bool checkMSSV(string id)
        {
            bool t = true;
            DBQL_SVDataContext db = new DBQL_SVDataContext();
            foreach (SV i in db.SVs)
            {
                if (i.MSSV == id)
                {
                    t = false;
                    break;
                }    
            }    
            return t;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DBQL_SVDataContext db = new DBQL_SVDataContext();
            if (txtMSSV.Enabled)
            {
                if (rbtnMale.Checked == true)
                {
                    SV sv = new SV()
                    {
                        MSSV = txtMSSV.Text,
                        NameSV = txtNameSV.Text,
                        ID_Lop = getIDLop(cbbLopSH.SelectedItem.ToString()),
                        NS = dtpNS.Value,
                        DTB = Convert.ToSingle(txtDTB.Text),
                        Gender = true,
                        Photo = ckbAnh.Checked,
                        HB = ckbHB.Checked,
                        CCCD = ckbCCCD.Checked,
                    };
                    if (checkMSSV(txtMSSV.Text))
                    {
                        db.SVs.InsertOnSubmit(sv);
                        db.SubmitChanges();
                    }
                    else
                    {
                        MessageBox.Show("MSSV đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (rbtnFemale.Checked == true)
                {
                    SV sv = new SV()
                    {
                        MSSV = txtMSSV.Text,
                        NameSV = txtNameSV.Text,
                        ID_Lop = getIDLop(cbbLopSH.SelectedItem.ToString()),
                        NS = dtpNS.Value,
                        DTB = Convert.ToSingle(txtDTB.Text),
                        Gender = false,
                        Photo = ckbAnh.Checked,
                        HB = ckbHB.Checked,
                        CCCD = ckbCCCD.Checked,
                    };
                    if (checkMSSV(txtMSSV.Text))
                    {
                        db.SVs.InsertOnSubmit(sv);
                        db.SubmitChanges();
                    }
                    else
                    {
                        MessageBox.Show("MSSV đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }    
            else
            {
                if (rbtnMale.Checked == true)
                {
                    var s = db.SVs.Where(p => p.MSSV == txtMSSV.Text).FirstOrDefault();
                    s.NameSV = txtNameSV.Text;
                    s.ID_Lop = getIDLop(cbbLopSH.SelectedItem.ToString());
                    s.NS = dtpNS.Value;
                    s.DTB = Convert.ToSingle(txtDTB.Text);
                    s.Gender = true;
                    s.Photo = ckbAnh.Checked;
                    s.HB = ckbHB.Checked;
                    s.CCCD = ckbCCCD.Checked;
                    db.SubmitChanges();
                }
                else if (rbtnFemale.Checked == true)
                {
                    var s = db.SVs.Where(p => p.MSSV == txtMSSV.Text).FirstOrDefault();
                    s.NameSV = txtNameSV.Text;
                    s.ID_Lop = getIDLop(cbbLopSH.SelectedItem.ToString());
                    s.NS = dtpNS.Value;
                    s.DTB = Convert.ToSingle(txtDTB.Text);
                    s.Gender = false;
                    s.Photo = ckbAnh.Checked;
                    s.HB = ckbHB.Checked;
                    s.CCCD = ckbCCCD.Checked;
                    db.SubmitChanges();
                }
            }    
            this.Dispose();
            f.ShowDGV();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
