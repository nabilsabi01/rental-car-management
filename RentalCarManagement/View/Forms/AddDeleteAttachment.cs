using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RentalCarManagement.DataBase;
using RentalCarManagement.Models;

namespace RentalCarManagement.View.Forms
{
    public partial class AddDeleteAttachment : Form
    {
        public AddDeleteAttachment()
        {
            InitializeComponent();
        }
        public Guid idRes;
        public byte[] GetBinaryFromFile(string file)
        {
            byte[] bytes;
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
            }
            return bytes;
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Word Documents|*.doc|Excel Worksheets|*.xls;*.xlsx|" +
                        "PowerPoint Presentations|*.ppt" +
                        "|Office Files|*.doc;*.xls;*.xlsx;*.ppt" +
                        "|Image Files|*.jpg;*.png" +
                        "|Text Files|*.txt;" +
                        "|Archives Files|*.zip;*.rar" +
                        "|All Files|*.*",
                Multiselect = true,
                Title = "Sélectionner les fichiers à importer ...."
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (DBRentalCarEntity db = new DBRentalCarEntity())
                {
                    FileInfo finfo;
                    foreach (string file in ofd.FileNames)
                    {
                        finfo = new FileInfo(file);
                        attachments att = new attachments
                        {
                            id = Guid.NewGuid(),
                            file_name = finfo.Name,
                            type_decument = finfo.Extension,
                            centenu_file = GetBinaryFromFile(finfo.FullName),
                            reservation_id = this.idRes
                        };
                        db.attachments.Add(att);
                        db.SaveChanges();
                    }
                    LoadDatAttachment();
                }
            }
        }
        // fill data grid
        private void LoadDatAttachment()
        {
            using (DBRentalCarEntity db = new DBRentalCarEntity())
            {
                dgvAtt.Rows.Clear();
                var list = db.reservations.FirstOrDefault(x => x.id == this.idRes).attachments.Select(
                    p => new
                    {
                        p.id,
                        p.file_name,
                    }).ToList();
                foreach (var f in list)
                {
                    dgvAtt.Rows.Add(f.id,f.file_name);
                }
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Reservations res = new Reservations();
            DialogResult R = MessageBox.Show("Vous-Voulez vraiment Supprimer", "Suppresion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (R == DialogResult.Yes)
            {
                for (int i = 0; i < dgvAtt.Rows.Count; i++)
                {
                    if (dgvAtt.CurrentRow != null)
                    {
                        using (DBRentalCarEntity db = new DBRentalCarEntity())
                        {
                            Guid idAtt = Guid.Parse(dgvAtt.CurrentRow.Cells[0].Value.ToString());
                            attachments att = db.attachments.FirstOrDefault(x => x.id == idAtt);
                            if (att != null)
                            {
                                db.attachments.Remove(att);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                LoadDatAttachment();
                MessageBox.Show("Supression avec succées", "Suppresion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Supression est annulé ", "Suppresion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddDeleteAttachment_Load(object sender, EventArgs e)
        {
            LoadDatAttachment();
        }

        private void BtnDowloand_Click(object sender, EventArgs e)
        {
            if (dgvAtt.SelectedRows.Count > 0)
            {
                using (DBRentalCarEntity db = new DBRentalCarEntity())
                {
                    Guid idAtt = Guid.Parse(dgvAtt.CurrentRow.Cells[0].Value.ToString());
                    attachments att = db.attachments.FirstOrDefault(x => x.id == idAtt);
                    byte[] fileContent = att.centenu_file;
                    string fileName = att.file_name;
                    Stream strm;
                    SaveFileDialog saveFile = new SaveFileDialog
                    {
                        Filter = att.type_decument+"|*"+att.type_decument,
                        FileName = fileName
                    };
                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        if ((strm = saveFile.OpenFile()) != null)
                        {
                            strm.Write(fileContent, 0, fileContent.Length);
                            strm.Close();
                        }
                    }
                }
            }
        }
    }
}
