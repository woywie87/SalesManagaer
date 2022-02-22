using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SQLiteNetExtensions.Extensions;
using Sales_WPF.MVVM;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;

namespace Sales_WPF
{
    class CustomReportsViewModel :  INotifyPropertyChanged
    {
        public List<Report> ListReports { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;

        public List<Report> GetReports()
        {
            List<Report> list = new List<Report>();

            try
            {
                

                XmlSerializer serializer = new XmlSerializer(typeof(ReportConfig));
                StreamReader reader = new StreamReader("reports.xml");

                ReportConfig obj = (ReportConfig)serializer.Deserialize(reader);
                list = obj.ReportList;


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


        public CustomReportsViewModel()
        {
       
            ListReports = Common.GetReports();



           






        }

    }

 

}
