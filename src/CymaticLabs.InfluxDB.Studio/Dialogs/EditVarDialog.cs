using CymaticLabs.InfluxDB.Data;
using CymaticLabs.InfluxDB.Studio.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CymaticLabs.InfluxDB.Studio.Dialogs
{
    public partial class EditVarDialog : Form
    {
        public EditVarDialog(QueryResultsControl queryResultsControl)
        {
            InitializeComponent();
            this.queryResultsControl = queryResultsControl;
        }

        private QueryResultsControl queryResultsControl;
        private InfluxDbSeries influxDbSeries;
        private IList<Object> curRecoder;

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var valueCol=this.influxDbSeries.Columns.Except(new List<string> { "time", "var_name" }).ToList();
            var timeStamp = (DateTime)this.curRecoder.ElementAt(this.influxDbSeries.GetColumnIndex("time"));
            var tags = new Dictionary<string, object>();
            tags.Add("var_name", this.curRecoder.ElementAt(this.influxDbSeries.GetColumnIndex("var_name")));
            var values = new Dictionary<string, object>();
            valueCol.ForEach(a => {
                values.Add(a, this.curRecoder.ElementAt(this.influxDbSeries.GetColumnIndex(a)));
            });
            values["var_value"] = double.Parse(this.textBox4.Text.Trim());
            this.queryResultsControl.writeData(tags,values,timeStamp);

        }
        public void RefreshDisplay(InfluxDbSeries influxDbSeries, IList<Object> curRecoder) {
            this.influxDbSeries = influxDbSeries;
            this.curRecoder = curRecoder;
            for (int i = 0; i < curRecoder.Count; i++) {
                var txtName=  "textBox" + (i+1);
                var allController = this.Controls.Cast<Control>();
                string str=curRecoder[i].ToString();
                if (curRecoder[i] is DateTime) {
                    str = ((DateTime)curRecoder[i]).AddHours(8).ToString();
                }
                allController.FirstOrDefault(a => a.Name.Equals(txtName)).Text = str;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.queryResultsControl.editVarDialog = null;
            this.Close();
            this.Dispose();
            
        }
    }
}
