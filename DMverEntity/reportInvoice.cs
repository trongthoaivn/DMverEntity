using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DMverEntity.Entity;

namespace DMverEntity
{

    public partial class reportInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        connectDBEntity mod = new connectDBEntity();
        string ID;
        public reportInvoice(string id)
        {
            InitializeComponent();
            ID = id;
            load();
        }
        private void load()
        {
            
        }
    }
}
