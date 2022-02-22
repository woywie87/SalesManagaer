using Sales_WPF.MVVM;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Xml;
using System.Linq;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Threading;
using System.Text.RegularExpressions;

namespace Sales_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow  : Window
    {


        public SeriesCollection SeriesCollection { get; set; }
        public List<string>Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public Report selectedReport;

        public Events selectedEvent;


        public Products selectedProduct;

        public ObservableCollection<SaleDetails> ListSaleDetails { get; set; }

        public SaleDetails selectedSale { get; set; }

        public repProducers selectedrepProducers;
        DataTable dt;

        public MainWindow()
        {
            InitializeComponent();

         
            



            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yy";
            Thread.CurrentThread.CurrentCulture = ci;


            datePicker.SelectedDate = DateTime.Now.Date;

       
            


        }


        private void dtgProducts_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var p= e.Row.DataContext as Products;
            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
            {
                db.Update(p);

            }

        }


       


        private void lstReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedReport = (Report)lstCustomReports.SelectedItem;
            if (selectedReport.ParamsList.Count > 0)
            {
                btnGenerateReport.Visibility = Visibility.Visible;


            }
            else btnGenerateReport.Visibility = Visibility.Collapsed;

            if (stkParams.Children.Count > 0)
            {
                stkParams.Children.RemoveRange(0, stkParams.Children.Count);
             
            }


            dtgCustomReport.Visibility = Visibility.Visible;

     
            dt = new DataTable();
            dt = Common.ExecuteQuery(selectedReport);
           
                foreach (var c in selectedReport.ParamsList)
                {
                    Label lbl = new Label();
                    lbl.Content = c.Caption;
                    stkParams.Children.Add(lbl);
                    TextBox txb = new TextBox();
                    txb.Name = c.Name;
                    txb.Text = c.Value;

                    stkParams.Children.Add(txb);


                }
            
            dtgCustomReport.DataContext = dt.DefaultView;

           
            UpdateLayout();
            if (selectedReport.ChartLabels != null && selectedReport.ChartValues != null)
            {
                PublishChart();
            }
            else
            {
                ReportChart.Visibility = Visibility.Collapsed;

            }

        }

        public void PublishChart()
        {
            ReportChart.Visibility = Visibility.Visible;

            var columnLabels = selectedReport.ColumnList.FirstOrDefault(x => x.Name == selectedReport.ChartLabels);
            var columnValues = selectedReport.ColumnList.FirstOrDefault(x => x.Name == selectedReport.ChartValues);

            if (columnLabels != null && columnValues != null)
            {
                ReportChartLabels.Title = columnLabels.Caption;
                ReportChartValue.Title = columnValues.Caption;


                ChartValues<double> CV = new ChartValues<double>();
                var cv = dt.AsEnumerable().Select(r => Convert.ToDouble(r.Field<int>(selectedReport.ChartValues)))
                    .ToList();
                CV.AddRange(cv);


                SeriesCollection = new SeriesCollection();


                Labels = new List<string>();
                var cl = dt.AsEnumerable().Select(r => r.Field<string>(selectedReport.ChartLabels)).ToList();
                Labels.AddRange(cl);


                SeriesCollection.Add(new LineSeries
                {
                    Title = "",
                    Values = CV,
                    LineSmoothness = 0,
                    PointGeometrySize = 15,
                    PointForeground = Brushes.IndianRed
                });

                SeriesCollection[0].Values.Add(12d);

                ReportChart.DataContext = SeriesCollection;
                ReportChartLabels.DataContext = Labels;

            }

            else
            {

                ReportChart.DataContext = null;
                ReportChartLabels.DataContext = null;
            }




        }



        private void dtgShopSales_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            /*
            var sl = e.Row.DataContext as SaleDetails;
            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
            {
                db.Update(sl);
                
            }
            */
        }


        private void cmbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var pr = (Products)cmbProducts.SelectedItem;
            if (pr != null)
            {

                txtSalePrice.Text = pr.Price.ToString();
    
            }

            txtSaleQty.Text = txtSaleQty.Text;


        }

        private void dtgProducers_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var sl = e.Row.DataContext as Producers;
            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
            {
                db.Update(sl);
            }

        }

        private void dtgSumProducers_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            /*
            selectedReportItem = new repProducers();

            selectedReportItem = (repProducers)lstReport5.SelectedItem;

            if (selectedReportItem != null)
            {

                ReporShop.SelectedItem = pivitemProducers;

                var prod = myProducers.Find(x => x.ProducerID == selectedReportItem.ProducerID);

                cmbfilter.SelectedItem = (Producers)prod;
            }

            */


        }

        private void dtgCustomReport_LoadingRow(object sender, DataGridRowEventArgs e)
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

        private void dtgCustomReport_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {


            if (dtgCustomReport.CurrentCell.Column != null)
            {
                int index = dtgCustomReport.CurrentCell.Column.DisplayIndex;

                DataRowView dataRow = (DataRowView)dtgCustomReport.SelectedItem;
                List<Report> ExtendReports;
                Report extReport = new Report();
                string cellValue = dataRow.Row.ItemArray[index].ToString();
                var c = dtgCustomReport.CurrentCell.Column;

                if (selectedReport.ColumnList[c.DisplayIndex].PassToReport != null)
                {

                    ExtendReports = Common.GetReports(Int16.Parse(selectedReport.ColumnList[c.DisplayIndex].PassToReport));
                    extReport = ExtendReports.FirstOrDefault();
                    extReport.ParamsList = selectedReport.ParamsList;

                    CustomReportsExtendWindow win = new CustomReportsExtendWindow(extReport, cellValue);
                    win.Show();

                }
            }
        }

        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
           string qr = selectedReport.Query;
           foreach (var c in stkParams.Children)
            {

                if(c is TextBox)
                {
                    TextBox txb = (TextBox)c;
                    //qr= qr.Replace('{' + txb.Name + '}', txb.Text);
                    foreach( var p in selectedReport.ParamsList)
                    {
                        if(txb.Name==p.Name)
                        {
                            p.Value = txb.Text;

                        }


                    }
                   

                }

            }

            dt = Common.ExecuteQuery(selectedReport);
            dtgCustomReport.DataContext = dt.DefaultView;
            PublishChart();
         // Common.SetDataGridLayout(selectedReport, ref dtgCustomReport);






        }

        private void lstShopDays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEvent = (Events)lstShopDays.SelectedItem;
            if(selectedEvent !=null)  txbSalesSum.Text = selectedEvent.ListSales.Sum(x => x.Price * x.Qty).ToString();
        }

        private void dtgCustomReport_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {


          //  e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);


            for (int i = 0; i < selectedReport.ColumnList.Count; i++)
            {
                e.Column.Visibility = Visibility.Collapsed;
            }



             for (int i = 0; i < selectedReport.ColumnList.Count; i++)
            {
                if (e.Column.Header.ToString().ToLower() == selectedReport.ColumnList[i].Name.ToLower())
                {
                    e.Column.Header = selectedReport.ColumnList[i].Caption;
                    e.Column.Visibility = Visibility.Visible;

                    var style = new Style(typeof(System.Windows.Controls.Primitives.DataGridColumnHeader));

                    var value = Enum.Parse(typeof(HorizontalAlignment), selectedReport.ColumnList[i].Align);

                    var AlignSetter = new Setter(Control.HorizontalContentAlignmentProperty, value);
                    var BackgroundSeter = new Setter(DataGridColumnHeader.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(selectedReport.ColumnList[i].Backgorund)));
                    var ForegroundSeter = new Setter(DataGridColumnHeader.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(selectedReport.ColumnList[i].Foreground)));
                    style.Setters.Add(AlignSetter);
                    style.Setters.Add(BackgroundSeter);
                    style.Setters.Add(ForegroundSeter);
                    e.Column.HeaderStyle = style;

                }

            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var sl = e.Source as Producers;
            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
            {
                db.Update(sl);
            }
        }

        private void TxtSaleQty_OnTextChanged(object sender, TextChangedEventArgs e)
        {
        //  var pr = (Products)cmbProducts.SelectedItem;
       //   if (pr !=null && txtSaleQty.Text !="") txtProdStockInfo.Text = CalculatedStock(Convert.ToInt16(txtSaleQty.Text), pr.ProductStock).ToString();
        }

        private int CalculatedStock(int saleQty, int stockProd)
        {
            
            int stock = stockProd - saleQty;
            return stock;


        }

        private void TxtSaleQty_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

       
    }
}
