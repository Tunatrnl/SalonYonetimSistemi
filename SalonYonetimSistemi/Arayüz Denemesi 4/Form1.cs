using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SalonYonetimSistemi
{
    public partial class Form1 : Form
    {
        private SQLiteConnection connection;
        private SQLiteCommand command;
        private SQLiteDataAdapter dataAdapter;
        private DataTable dataTable = new DataTable();

        public Form1()
        {
            InitializeComponent();
            SetConnection();
            LoadData();
        }

        private void SetConnection()
        {
            connection = new SQLiteConnection("Data Source=\"C:\\Users\\merto\\source\\repos\\Arayüz Denemesi 4\\database.db\";Version=3;New=True;Compress=True;");
            try
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    MessageBox.Show("Veritabanına başarıyla bağlandı!");
                }
                command = connection.CreateCommand();
                command.CommandText = "CREATE TABLE IF NOT EXISTS Bilet (ID INTEGER PRIMARY KEY AUTOINCREMENT, KullaniciAdi TEXT, FilmAdi TEXT, SeansSaati TEXT, Tarih TEXT, KoltukSayisi INTEGER)";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanına bağlanırken bir hata oluştu: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                dataAdapter = new SQLiteDataAdapter("SELECT * FROM Bilet", connection);
                SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter);
                dataTable.Clear();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Yeni bir komut oluştur
                command.CommandText = "INSERT INTO Bilet (KullaniciAdi, FilmAdi, SeansSaati, Tarih, KoltukSayisi) VALUES (@KullaniciAdi, @FilmAdi, @SeansSaati, @Tarih, @KoltukSayisi)";
                command.Parameters.AddWithValue("@KullaniciAdi", textBox1.Text);
                command.Parameters.AddWithValue("@FilmAdi", comboBox1.Text);
                command.Parameters.AddWithValue("@SeansSaati", comboBox2.Text);
                command.Parameters.AddWithValue("@Tarih", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@KoltukSayisi", numericUpDown1.Value);

                // Komutu yürüt
                command.ExecuteNonQuery();

                // Verileri yeniden yükle
                LoadData();

                // Parametreleri temizle
                command.Parameters.Clear();

                // Ekleme işlemi tamamlandı mesajı ver
                MessageBox.Show("Bilet başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
