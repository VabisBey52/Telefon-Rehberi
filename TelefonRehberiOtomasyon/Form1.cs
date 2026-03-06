using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TelefonRehberiOtomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbTelRehberAEntities db=new DbTelRehberAEntities();
        //Entity de LINQ sorgular...
        private void Form1_Load(object sender, EventArgs e)
        {
            var kisiler=db.TBLKISILER.ToList();// kişiler tablosundakileri listele
           //var bir kayıt bir nesne
           dataGridView1.DataSource=kisiler;

            dataGridView1.Font = new Font("verdana",12,FontStyle.Bold);

            dataGridView1.Columns[0].Width = 60;

            dataGridView1.Columns[0].HeaderText = "ID";

            dataGridView1.ForeColor = Color.DarkBlue;

            dataGridView1.Columns[8].Visible = false;
            //selection mode de fullrowselect tüm satırı seçer.
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //YENİ KAYIT EKLE

            TBLKISILER yeni= new TBLKISILER();//şu an yeni kayıt da (null null null)
            yeni.AD = textBox2.Text;
            yeni.SOYAD = textBox4.Text;
            yeni.MAİL= textBox6.Text;
            yeni.ADRES = textBox8.Text;
            yeni.MESLEK= textBox9.Text;
            yeni.TEL=textBox3.Text;
            yeni.DOGUMGUNU = Convert.ToDateTime(dateTimePicker1.Value.ToString());
            if (radioButton1.Checked)
            {
                yeni.CINSIYET = "E";
            }
            else
            {
                yeni.CINSIYET = "K";
            }
            db.TBLKISILER.Add(yeni);
            db.SaveChanges();
            MessageBox.Show(MessageBoxIcon.Information+ "Bluetooth devices is connection Successfuly");

            var kisiler = db.TBLKISILER.ToList();
            
            dataGridView1.DataSource = kisiler;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var kisiler = db.TBLKISILER.ToList();// kişiler tablosundakileri listele
            //var bir kayıt bir nesne
            dataGridView1.DataSource = kisiler;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int silinecekkisiID=Convert.ToInt32(textBox1.Text);//texbox 1 den al
            var silinecekkayit = db.TBLKISILER.Find(silinecekkisiID);//kisiler tablosunda kaydı bul

            db.TBLKISILER.Remove(silinecekkayit);//kaydı sil

            db.SaveChanges();//değişiklikleri kaydet

            MessageBox.Show("Kayıt başarıyla silindi! "+ MessageBoxIcon.Warning);

            var kisiler=db.TBLKISILER.ToList();//listeyi yenile
            dataGridView1.DataSource = kisiler;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox6.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var guncellenecekkayit = db.TBLKISILER.Find(int.Parse(textBox1.Text));

            guncellenecekkayit.AD = textBox2.Text;
            guncellenecekkayit.SOYAD = textBox4.Text;
            guncellenecekkayit.MESLEK = textBox9.Text;
            guncellenecekkayit.ADRES = textBox8.Text;
            guncellenecekkayit.MAİL = textBox6.Text;
            guncellenecekkayit.TEL = textBox3.Text;
            guncellenecekkayit.DOGUMGUNU = Convert.ToDateTime(dateTimePicker1.Value);
            if (radioButton1.Checked)
            {
                guncellenecekkayit.CINSIYET = "E";
            }
            else
            {
                guncellenecekkayit.CINSIYET = "K";
            }

            db.SaveChanges();
            MessageBox.Show("kaydınız güncellendi");

            var kisiler = db.TBLKISILER.ToList();// listeyi yenile
            dataGridView1.DataSource = kisiler;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //datagridview üzerine tıkladığımızda seçilen kaydın verilerini textboxlara taşıyalım.

            //seçilen satır nosunu alalım
            int satirno = dataGridView1.SelectedCells[0].RowIndex;

            //buradaki kaydın ID değerine ulaşalım
            int secilenKayitID = int.Parse(dataGridView1.Rows[satirno].Cells[0].Value.ToString());
            //int yerine string yaz ismi alır


            //bu ID ye ait olan kaydı bul
            //kayıtlarda var kullanılır.
            var guncellenecekKayit=db.TBLKISILER.Find(secilenKayitID);

            textBox1.Text=guncellenecekKayit.KISIID.ToString();
            textBox2.Text=guncellenecekKayit.AD.ToString();
            textBox4.Text=guncellenecekKayit.SOYAD.ToString();
            textBox9.Text=guncellenecekKayit.MESLEK.ToString();
            textBox6.Text=guncellenecekKayit.MAİL.ToString();
            textBox3.Text=guncellenecekKayit.TEL.ToString();
            textBox8.Text=guncellenecekKayit.ADRES.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(guncellenecekKayit.DOGUMGUNU.Value);
            if (guncellenecekKayit.CINSIYET=="E")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }


        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            var bul = from x in db.TBLKISILER
                      where x.AD.Contains(tbara.Text) || x.SOYAD.Contains(tbara.Text)
                      select x;

            dataGridView1.DataSource = bul.ToList();
        }

        private void textBox7_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                var bul = from x in db.TBLKISILER
                          where x.CINSIYET == "E"
                          select x;

                dataGridView1.DataSource = bul.ToList();
            }
            else
            {
                var bul = from x in db.TBLKISILER
                          select x;
                dataGridView1.DataSource = bul.ToList();
            }
        }
    }
}
