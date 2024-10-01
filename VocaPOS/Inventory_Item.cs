using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VocaPOS
{
    public partial class Inventory_Item : Form
    {
        VocaPOSEntities db = new VocaPOSEntities();
        bool createMode = true;
        public Inventory_Item()
        {
            InitializeComponent();
        }

        private void Inventory_Item_Load(object sender, EventArgs e)
        {
            //Read Data on database inventory & kategori
            inventory_table.DataSource = db.Inventory.Where(f => f.IsDeleted == false).ToList();
            kategoriBindingSource.DataSource = db.Kategori.ToList();

            //Open Create data mode
            inventoryBindingSource.AddNew();
        }

        private void inventoryDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (inventoryDataGridView.Rows[e.RowIndex].DataBoundItem is Inventory i)
            {
                if (e.ColumnIndex == kategori_ID.Index) e.Value = i.Kategori.NamaKategori;
                if (e.ColumnIndex == Kategori_Item.Index)
                {
                    // Memisahkan deskripsi menjadi kata-kata
                    string[] words = i.Deskripsi.Split(' ');

                    // Mengambil kata-kata yang diakhiri dengan '#'
                    var filteredWords = words.Where(word => word.StartsWith("#"))
                                             .Select(word => word.TrimEnd('#'));

                    // Menggabungkan hasil menjadi satu string dipisahkan oleh koma
                    string kategori_Des = string.Join(",", filteredWords);
                    kategori_Des = kategori_Des.Replace("#", "");
                    
                    e.Value = kategori_Des;
                }
            }
        }

        private void inventoryDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (inventoryDataGridView.SelectedRows[0].DataBoundItem is Inventory i)
            {
                inventoryBindingSource.DataSource = db.Inventory.AsNoTracking().FirstOrDefault(f => f.InventoryID == i.InventoryID);

                createMode = !createMode;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validasi terlebih dahulu
            if (!Validate(inventoryBindingSource))
            {
                return;
            }

            if (createMode && inventoryBindingSource.Current is Inventory i)
            {
                db.Inventory.Add(i);  // Menambahkan Inventory ke database
                db.SaveChanges();  // Simpan perubahan ke database
            }
            else if (createMode == false && inventoryBindingSource.Current is Inventory inventory)
            {
                // Jika dalam mode edit (createMode == false), perbarui data yang ada
                var existingInventory = db.Inventory.Find(inventory.InventoryID);
                if (existingInventory != null)
                {
                    // Update properti dari item yang dipilih
                    existingInventory.NamaItem = inventory.NamaItem;
                    existingInventory.KategoriID = inventory.KategoriID;
                    existingInventory.Deskripsi = inventory.Deskripsi;
                    // Tambahkan update lainnya di sini jika ada properti lain

                    db.SaveChanges();  // Simpan perubahan ke database
                    MessageBox.Show("Data berhasil diperbarui!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button4.PerformClick();
                }
                else
                {
                    MessageBox.Show("Item tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            OnLoad(EventArgs.Empty);  // Muat ulang data
        }

        // Ubah return type ke bool untuk menunjukkan validasi berhasil/gagal
        static bool Validate(BindingSource bindingSource)
        {
            if (bindingSource.Current is Inventory i)
            {
                if (string.IsNullOrWhiteSpace(i.NamaItem))
                {
                    MessageBox.Show("Mohon mengisi data Nama Item yang masih kosong", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(i.Deskripsi))
                {
                    MessageBox.Show("Mohon mengisi data Deskripsi yang masih kosong", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            return true;  // Jika validasi berhasil
        }

        private void button4_Click(object sender, EventArgs e)
        {
            inventoryBindingSource.Clear();
            createMode = true;
            OnLoad(EventArgs.Empty);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (inventoryDataGridView.SelectedRows[0].DataBoundItem is Inventory i)
            {
                var existingInventory = db.Inventory.Find(i.InventoryID);
                if(MessageBox.Show("Apakah anda yakin ingin menghapus data","valiasi",MessageBoxButtons.OKCancel,MessageBoxIcon.Stop)==DialogResult.OK && existingInventory != null)
                existingInventory.IsDeleted = true;
                button4.PerformClick();

                db.SaveChanges();
            }
        }
    }
}

