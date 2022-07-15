using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulProje
{
    public partial class FrmDersler : Form
    {
        public FrmDersler()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        DataSet1TableAdapters.TBLDERSLERTableAdapter ds = new DataSet1TableAdapters.TBLDERSLERTableAdapter();
        private void FrmDersler_Load(object sender, EventArgs e)
        {
            //Datagridview'da dersleri listeledik.

            dataGridView1.DataSource = ds.DersListesi();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            ds.AddLesson(TxtDersAdı.Text);
            MessageBox.Show("Ders Ekleme İşlemi Başarılı Şekilde Yapılmıştır.");
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.DersListesi();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            //dersıd string olarak tanımlanmış.Delete etmesi için byte türüne çevrilmesi gerekiyor.
            ds.DeleteLesson(byte.Parse(TxtDersId.Text));
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            ds.UpdateLesson(TxtDersAdı.Text, byte.Parse(TxtDersId.Text));
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtDersId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtDersAdı.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}
