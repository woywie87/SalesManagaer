using System;
using System.Collections.Generic;
using System.ComponentModel;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using Sales_WPF;
using SQLitePCL;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Data;
using System.Linq;

namespace Sales_WPF
{

    [XmlRoot("config")]
    public class ReportConfig
    {
        [XmlArray("reports")]
        [XmlArrayItem("report")]
        public List<Report> ReportList { get; set; }


    }

    public class Report
    {
        [XmlElement("id")]
        public int ReportID { get; set; }
        [XmlElement("title")]
        public string Name { get; set; }
        [XmlElement("type")]
        public string Type { get; set; }
        [XmlElement("query")]
        public string Query { get; set; }

        [XmlElement("chartlabels")]
        public string ChartLabels { get; set; }
        [XmlElement("chartlvalues")]
        public string ChartValues { get; set; }

        [XmlArray("columns")]
        [XmlArrayItem("column")]
        public List<Column> ColumnList { get; set; }

        [XmlArray("params")]
        [XmlArrayItem("param")]
        public List<Param> ParamsList { get; set; }


    }


    public class Column
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("caption")]
        public string Caption { get; set; }
        [XmlAttribute("format")]
        public string Format { get; set; }
        [XmlAttribute("background")]
        public string Backgorund { get; set; }
        [XmlAttribute("foreground")]
        public string Foreground { get; set; }
        [XmlAttribute("align")]
        public string Align { get; set; }
        [XmlAttribute("passtoreport")]
        public string PassToReport { get; set; }


    }

    public class Param
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("caption")]
        public string Caption { get; set; }
        [XmlAttribute("value")]
        public string Value { get; set; }



    }

    public class Events

    {

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        [PrimaryKey, AutoIncrement]
        public int EventID { get; set; }

        //public string EventName { get; set; }

        private string _eventname { get; set; }
        public string EventName
        {
            get { return _eventname; }
            set
            {
                if (value == _eventname) return;
                _eventname = value;
                OnPropertyChanged("EventName");
            }

        }



        public string EventDate { get; set; }


        public int EventTypeID { get; set; }



        /*
        public string DateSubstr
        {
           
            get
            {
                DateTime date = new DateTime();
                date = DateTime.Parse(EventDate);

                return  date.ToString("dd-MM-yy");

            }

        }

        public string NameDate
        {

            get
            {


                return EventName + " " + DateSubstr;

            }

        }
 
    */


        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<SaleDetails> ListSales { get; set; }
      


        
        public Events()
        {

            ListSales = new ObservableCollection<SaleDetails>();
          
        }
        


        }

    }

public class Products
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        [PrimaryKey, AutoIncrement]
        public int ProductID { get; set; }



         private string _productname;
        public string ProductName 
         {
                get { return _productname; }
                set
                {
                    if (value == _productname) return;
                    _productname = value;
                    OnPropertyChanged("ProductName");
                }

            }

        private int _price;

        public int Price

        {
            get { return _price; }
            set
            {
                if (value == _price) return;
                _price = value;
                OnPropertyChanged("Price");
            }

        }



    


    private string _productcode;
    public string ProductCode

    {
        get { return _productcode; }
        set
        {
            if (value == _productcode) return;
            _productcode = value;
            OnPropertyChanged("ProductCode");
        }

    }


    private int _productstock;
    public int ProductStock

    {
        get { return _productstock; }
        set
        {
            if (value == _productstock) return;
            _productstock = value;
            OnPropertyChanged("ProductStock");
        }

    }


    public double Prod { get; set; }

    public double Tax { get; set; }

    public int ProductStatus { get; set; }

   
        //public Nullable<int> ProducerID { get; set; }
         [ForeignKey(typeof(Producers))]
         public int ProducerID { get; set; }

    [OneToOne]
    public Producers Producer { get; set; }

    public string FullName
    {
        get
        {
            return Producer.ProducerName + " " + ProductName;

        }

    }




}


    public class SaleDetails
    {

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    [PrimaryKey, AutoIncrement]
        public int SaleDetailID { get; set; }

    [ForeignKey(typeof(Events))]
    public int EventID { get; set; }


    [ForeignKey(typeof(Products))]
    public int ProductID { get; set; }

        public int Price { get; set; }

        public int Qty { get; set; }

        public double Prod { get; set; }

        public int SumUp { get; set; }

        public bool IsRegister { get; set; }


    //  [ManyToOne(CascadeOperations = CascadeOperation.All)]
    //   public Events Event { get; set; }

    //  [OneToOne(CascadeOperations = CascadeOperation.All)]
    private string _product;

    [Ignore]
    public string Product
    {
        get
        {
            string pname;

            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
            {
                pname = db.Table<Products>().Where(x => x.ProductID == ProductID).FirstOrDefault().ProductName;


            }
            return pname;

        }
        set
        {
            if (value == _product) return;
            _product = value;
            OnPropertyChanged("Product");
        }

    }



}


    public class CostM
    {

        [PrimaryKey, AutoIncrement]
        public int CostID { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
        public string CostDate { get; set; }



        public string DateSubstr
        {

            get
            {
                DateTime date = new DateTime();
                date = DateTime.Parse(CostDate);

                return " " + date.ToString("dd-MM-yy");

            }

        }


    }


public class Producers
    {
        [PrimaryKey, AutoIncrement]
        public int ProducerID { get; set; }

        public string ProducerName { get; set; }
        public string Info { get; set; }
        public int ProducerStatus { get; set; }

    }




  


    public class repTopProduct
    {

        public int ProductID { get; set; }
        public string ProducerName { get; set; }
        public string ProductName { get; set; }
        public int suma { get; set; }
        public int zysk { get; set; }
        public int cnt { get; set; }
        public int avgprice { get; set; }

    }

    public class repSumEvent
    {

        public int EventID { get; set; }
        public string Name { get; set; }
        public int suma { get; set; }
        public int cnt { get; set; }
        public int zysk { get; set; }
        public int cntSum { get; set; }
        public int EventTypeID { get; set; }

        public string EventDate{ get; set; }


        public string DateSubstr
        {

            get
            {
                DateTime date = new DateTime();
                date = DateTime.Parse(EventDate);

                return date.ToString("dd-MM-yy");

            }

        }


        public string FullName
        {

            get
            {


                return Name + " " + DateSubstr;

            }

        }

    }


    public class repSalesEvents
    {

        public int SaleDetailID { get; set; }
        public int EventID { get; set; }
        public string EventName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int suma { get; set; }
        public int cnt { get; set; }
        public int EventTypeID { get; set; }

    }



    public class repSalesProducers
    {

        public int SaleDetailID { get; set; }
        public int ProductID { get; set; }
        public int ProducerID { get; set; }
        public string ProductName { get; set; }
        public int suma { get; set; }
        public int cnt { get; set; }
        public int EventTypeID { get; set; }

    }


    public class repSumMonth
    {
        public int suma { get; set; }
        public int zysk { get; set; }
        public string month { get; set; }
        public int EventTypeID { get; set; }

        


    }

    public class repSumMonth2
    {
        public int suma { get; set; }
        public int zysk { get; set; }
        public string month { get; set; }
        public string mm { get; set; }


    }


    public class repProducers
    {
        public string ProducerName { get; set; }
        public int suma { get; set; }
        public string zysk { get; set; }
        public string cnt { get; set; }

    }






    public class SaleMonths
    {
        public static Dictionary<int, string> months;

        public static Dictionary<int,string> generateMonths()
        {

            months = new Dictionary<int, string>();

            months.Add(1, "Styczeń");
            months.Add(2, "Luty");
            months.Add(3, "Marzec");
            months.Add(4, "Kwiecień");
            months.Add(5, "Maj");
            months.Add(6, "Czerwiec");
            months.Add(7, "Lipiec");
            months.Add(8, "Sierpień");
            months.Add(9, "Wrzesień");
            months.Add(10, "Październik");
            months.Add(11, "Listopad");
            months.Add(12, "Grudzień");

            return months;

        }


    }


  



    public class vwEventFilter
    {
        public string kod { get; set; }

        public string Name { get; set; }

    }

    public class vwKomisFilter
    {
        public string kod { get; set; }

        public string Name { get; set; }

    }





    public class repSumKomisCust
    {
        public string EventName { get; set; }

        public int suma { get; set; }

        public int zysk { get; set; }
        public int cnt { get; set; }

    }








