using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentalCarManagement
{
    public partial class PrintRes : Form
    {
        public PrintRes()
        {
            InitializeComponent();
        }

        private void PrintRes_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'DSReservation.DsRes'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.DsResTableAdapter.Fill(this.DSReservation.DsRes);

            this.RptViewRes.RefreshReport();
        }
    }
}
