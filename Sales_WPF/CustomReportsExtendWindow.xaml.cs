using Sales_WPF.MVVM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace Sales_WPF
{
    /// <summary>
    /// Interaction logic for CustomReportsExtendWindow.xaml
    /// </summary>
    public partial class CustomReportsExtendWindow : Window
    {

        public Report report;
        public string PrepareQuery(string query,string par)
        {
            Regex yourRegex = new Regex(@"\{([^\}]+)\}");
            string result = yourRegex.Replace(query, par);

            return result;



        }
        
        public CustomReportsExtendWindow(Report rep,string parameter)
        {

            
            report = rep;
            /*
            string qr = rep.Query;

            
            foreach (var c in rep.ParamsList)
            {
                    qr = qr.Replace('{' + c.Name + '}', c.Value);

            }
            */

          //  string qr = "";
            //qr =PrepareQuery(report.Query, parameter);
           // report.Query = qr;
            InitializeComponent();
            DataTable dt = new DataTable();

            dt = Common.ExecuteQuery(report, parameter);

            dtgExtendReport.DataContext = dt.DefaultView;

          //  Common.SetDataGridLayout(rep, ref dtgExtendReport);








        }

        private void dtgExtendReport_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dtgExtendReport.CurrentCell.Column != null)
            {
                int index = dtgExtendReport.CurrentCell.Column.DisplayIndex;
                DataRowView dataRow = (DataRowView)dtgExtendReport.SelectedItem;
                List<Report> ExtendReports;
                Report extReport = new Report();
                string cellValue = dataRow.Row.ItemArray[index].ToString();
                // var content = cellInfo.Column.GetCellContent(cellInfo.Item);
                var c = dtgExtendReport.CurrentCell.Column;

                if (report.ColumnList[c.DisplayIndex].PassToReport != null)
                {

                    ExtendReports = Common.GetReports(Int16.Parse(report.ColumnList[c.DisplayIndex].PassToReport));
                    extReport = ExtendReports.FirstOrDefault();
                    // string par = selectedReport.ColumnList[c.DisplayIndex].Name;
                    extReport.ParamsList = report.ParamsList;

                    CustomReportsExtendWindow win = new CustomReportsExtendWindow(extReport, cellValue);
                    win.Show();

                }
            }

          

        }

        private void dtgExtendReport_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var item = e.Row.Item as DataRowView;

            try
            {

                string color = item.Row["RowColor"].ToString();

                if (color.ToString() != "")
                {
                    e.Row.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));


                }
            }
            catch { }
        }



        private void dtgExtendReport_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            for (int i = 0; i < report.ColumnList.Count; i++)
            {
                e.Column.Visibility = Visibility.Collapsed;
            }




            for (int i = 0; i < report.ColumnList.Count; i++)
            {
                if (e.Column.Header.ToString().ToLower() == report.ColumnList[i].Name.ToLower())
                {
                    e.Column.Header = report.ColumnList[i].Caption;
                    e.Column.Visibility = Visibility.Visible;

                    var style = new Style(typeof(System.Windows.Controls.Primitives.DataGridColumnHeader));

                    var value = Enum.Parse(typeof(HorizontalAlignment), report.ColumnList[i].Align);

                    var AlignSetter = new Setter(Control.HorizontalContentAlignmentProperty, value);
                    var BackgroundSeter = new Setter(DataGridColumnHeader.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(report.ColumnList[i].Backgorund)));
                    var ForegroundSeter = new Setter(DataGridColumnHeader.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(report.ColumnList[i].Foreground)));
                    style.Setters.Add(AlignSetter);
                    style.Setters.Add(BackgroundSeter);
                    style.Setters.Add(ForegroundSeter);
                    e.Column.HeaderStyle = style;

                }

            }

        }
    }
}
