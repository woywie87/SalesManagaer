using SQLiteNetExtensions.Extensions;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Sales_WPF.MVVM
{
    public  static class Common
    {


        public static string PrepareQuery(string query, string par)
        {
            Regex yourRegex = new Regex(@"\{([^\}]+)\}");
            string result = yourRegex.Replace(query, par);

            return result;
        }


        public static ObservableCollection<Products> GetAllProducts()
        {
            List<Products> list;
            ObservableCollection<Products> list2;
            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
            {
                list = db.GetAllWithChildren<Products>(recursive: true).ToList().Where(x => x.ProductStatus == 1).OrderBy(x => x.Producer.ProducerName).ToList();

            }
            list2 = new ObservableCollection<Products>(list);
            return list2;
        }



        public static void SetDataGridLayout(Report rep, ref DataGrid dtg)
        {
            
            for (int i = 0; i < dtg.Columns.Count; i++)
            {
                dtg.Columns[i].Visibility = Visibility.Collapsed;

            }




            for (int i = 0; i < rep.ColumnList.Count; i++)
            {

                var style = new Style(typeof(System.Windows.Controls.Primitives.DataGridColumnHeader));

                var value = Enum.Parse(typeof(HorizontalAlignment), rep.ColumnList[i].Align);

                var AlignSetter = new Setter(Control.HorizontalContentAlignmentProperty, value);
                var BackgroundSeter = new Setter(DataGridColumnHeader.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(rep.ColumnList[i].Backgorund)));
                var ForegroundSeter = new Setter(DataGridColumnHeader.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(rep.ColumnList[i].Foreground)));
                style.Setters.Add(AlignSetter);
                style.Setters.Add(BackgroundSeter);
                style.Setters.Add(ForegroundSeter);

                dtg.Columns[i].HeaderStyle = style;

                if(dtg.Columns[i].Header.ToString() == rep.ColumnList[i].Name)
                {
                    dtg.Columns[i].Header = rep.ColumnList[i].Caption;
                    dtg.Columns[i].Visibility = Visibility.Visible;

                }
             
            }

        }



        public static DataTable ExecuteQuery(Report rep, string parameter ="")
        {
            DataTable dt = new DataTable();

            if (
                   rep.Query.Contains("create") || rep.Query.Contains("delete") || rep.Query.Contains("alter") || rep.Query.Contains("update") ||
                   rep.Query.Contains("drop") || rep.Query.Contains("trunc") || rep.Query.Contains("insert")
               )

            { }
            else
            {
                //string qr = rep.Query.ToLower();
                string qr = rep.Query;

                foreach (var c in rep.ParamsList)
                {
                    qr = qr.Replace('{' + c.Name + '}', c.Value);

                }
               // rep.Query = qr;

                qr = qr.Replace("{parameter}", parameter);


                string ConString = "sale.db";
                using (SQLiteConnection con = new SQLiteConnection(ConString))
                {

                    using (var statement = con.Prepare(qr))
                    {
                        try
                        {
                            for (int i = 0; i < statement.ColumnCount; i++)
                            {
                                
                                DataColumn column = new DataColumn(statement.ColumnName(i));
                             
                                    foreach (var l in rep.ColumnList)
                                    {
                                        if (column.ColumnName.ToLower() == l.Name.ToLower())
                                        {
                                          //  column.ColumnName = l.Caption;

                                            if (l.Format == "decimal") column.DataType = System.Type.GetType("System.Int32");
                            
                                    }
                                    


                                    }
                                    dt.Columns.Add(column);
                                


                            }

                            while (statement.Step() == SQLiteResult.ROW)
                            {

                                DataRow dr;
                                dr = dt.NewRow();

                                for (int i = 0; i < statement.ColumnCount; i++)
                                {
                                    dr[i] = statement[i];
                                }
                                dt.Rows.Add(dr);
                            }




                        }
                        catch (System.Data.DuplicateNameException)
                        {
                            // MessageBox.Show(duplcol);
                            statement.Dispose();
                        }


                     
                    }

                }

            }
            return dt;


        }

        public static List<Report> GetReports(int ?reportid =null)
        {
            List<Report> list = new List<Report>();

            try
            {


                XmlSerializer serializer = new XmlSerializer(typeof(ReportConfig));
                StreamReader reader = new StreamReader("reports.xml");

                ReportConfig obj = (ReportConfig)serializer.Deserialize(reader);

                if (reportid != null)
                {
                    list = obj.ReportList.FindAll(x => x.ReportID ==reportid);
                }

                else list = obj.ReportList.FindAll(x => x.Type =="");

                return list;

            }
            catch (System.Xml.XmlException)
            {
                //  MessageBox.Show(xmlfile);
                return list;

            }

            catch (System.IO.FileNotFoundException)
            {
                //MessageBox.Show(reportfile);
                return list;
            }
        }



    }
}
