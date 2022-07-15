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

    public partial class FrmKulup : Form
    {
        
        public FrmKulup()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=RECEP;Initial Catalog=Okul;Integrated Security=True");
        void listele()
        {
            //listeleme işlemi yaptık.
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLKULUPLER", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void FrmKulup_Load(object sender, EventArgs e)
        {
            listele();
        }
        
        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand insert = new SqlCommand("INSERT INTO TBLKULUPLER (KULUPAD) VALUES (@P1)", baglanti);
            insert.Parameters.AddWithValue("@P1",TxtKulupAdı.Text);
            insert.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Kulup Listeye Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtKulupId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtKulupAdı.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand delete = new SqlCommand("Delete From TBLKULUPLER WHERE KULUPID=@p1", baglanti);
            delete.Parameters.AddWithValue("@p1", TxtKulupId.Text);
            delete.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Kulüp Silme İşlemi Gerçekleşti.");
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand updateclub = new SqlCommand("Update TBLKULUPLER Set KULUPAD=@p1 WHERE KULUPID=@p2", baglanti);
            updateclub.Parameters.AddWithValue("@p1", TxtKulupAdı.Text);
            updateclub.Parameters.AddWithValue("@p2", TxtKulupId.Text);
            updateclub.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kulüp Güncelleme Başarılı");
            listele();
        }
    }
}
