using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VocaPOS
{
    public partial class POS : Form
    {
        public POS()
        {
            InitializeComponent();
        }

        private void POS_Load(object sender, EventArgs e)
        {
            string[] table = { "1", "2", "3", "Qty", "4", "5", "6", "% Disc", "7", "8", "9", "Price", "+/-", "0", ",", "x" };
            // Variabel untuk mengontrol row yang akan ditambahkan
            int totalItems = table.Length;
            int rowsNeeded = (int)Math.Ceiling(totalItems / 4.0); // Hitung jumlah row yang diperlukan

            // Loop untuk menambahkan row ke dalam DataGridView
            int index = 0;
            for (int i = 0; i < rowsNeeded; i++)
            {
                // Tambahkan row baru
                dataGridView1.Rows.Add();

                // Isi setiap cell di row dengan data dari array
                for (int j = 0; j < 4; j++)
                {
                    if (index < totalItems) // Pastikan tidak melampaui panjang array
                    {
                        DataGridViewButtonCell buttonCell = new DataGridViewButtonCell();
                        buttonCell.Value = table[index]; // Set value sesuai data di array
                        dataGridView1.Rows[i].Cells[j] = buttonCell; // Assign ke cell di DataGridView
                        index++;
                    }
                }
            }

            // Mengatur agar tidak ada padding/margin di dalam cell untuk button
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 56; // Set tinggi row (sesuaikan sesuai kebutuhan)
            }
        }
    }
}
