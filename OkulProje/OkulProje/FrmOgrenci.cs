using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace OkulProje
{
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=RECEP;Initial Catalog=Okul;Integrated Security=True");

        DataSet1TableAdapters.TBLDERSLERKULUPLERTableAdapter ko = new DataSet1TableAdapters.TBLDERSLERKULUPLERTableAdapter();
        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ko.OgrenciListesi();

            //Kulüpleri çekip aynı zamanda combobox'da yazdırdık.
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select* From TBLKULUPLER", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbOgrenciKulup.DisplayMember = "KULUPAD"; //displaymember gözüken kısım. Yani bana kulupad gözüksün.
            CmbOgrenciKulup.ValueMember = "KULUPID"; //valuemember arkaplanda olan gözükmeyen kısım.
            CmbOgrenciKulup.DataSource = dt;

            baglanti.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        string cinsiyet = " ";
        private void BtnEkle_Click(object sender, EventArgs e)
        {

            if (radioButton1.Checked == true)
            {
                cinsiyet = "Kadın";
            }
            if (radioButton2.Checked == true)
            {
                cinsiyet = "Erkek";
            }
            ko.AddStudent(TxtOgrenciAdı.Text, TxtOgrenciSoyadı.Text, byte.Parse(CmbOgrenciKulup.SelectedValue.ToString()), cinsiyet);
            MessageBox.Show("Öğrenci Ekleme İşlemi Başarılı!");
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ko.OgrenciListesi();
        }

        private void CmbOgrenciKulup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //kulup ıd'sini txt'ye yazdırdık. SelectedValue seçilen değer anlamında.
            //TxtOgrenciId.Text = CmbOgrenciKulup.SelectedValue.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            ko.DeleteStudent(int.Parse(TxtOgrenciId.Text));

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtOgrenciId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtOgrenciAdı.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtOgrenciSoyadı.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            cinsiyet = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            if (cinsiyet == "Kadın")
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            if (cinsiyet == "Erkek")
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
            }
            CmbOgrenciKulup.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();


        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            ko.UpdateStudent(TxtOgrenciAdı.Text, TxtOgrenciSoyadı.Text, byte.Parse(CmbOgrenciKulup.SelectedValue.ToString()), cinsiyet, int.Parse(TxtOgrenciId.Text));
            MessageBox.Show("Kayıt Güncellendi!");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                cinsiyet = "Kadın";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                cinsiyet = "Erkek";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //öğrenci adına göre arama işlemi yaptık.
            dataGridView1.DataSource = ko.OgrenciGetir(TxtAra.Text);
        }
    }
}
