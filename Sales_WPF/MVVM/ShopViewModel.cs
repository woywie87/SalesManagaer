using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SQLiteNetExtensions.Extensions;
using Sales_WPF.MVVM;
using System.Globalization;
using System.Windows;

namespace Sales_WPF
{
    class ShopViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Events> _listShopDays;

        public Events selectedEvent { get; set; }

        //public ObservableCollection<Products> ListProducts { get; set; }
        //public ObservableCollection<Events> ListShopDays { get; set; }


        private ObservableCollection<Products> _listProducts;
        public ObservableCollection<Products> ListProducts
        {

            get { return _listProducts ?? (ListProducts = new ObservableCollection<Products>()); }
            set { _listProducts = value; OnPropertyChanged("ListProducts"); }

        }




        public ObservableCollection<Events> ListShopDays
        {

            get { return _listShopDays ?? (_listShopDays = new ObservableCollection<Events>()); }
            set { _listShopDays = value; OnPropertyChanged("ListShopDays"); }

        }





        /*
        {
            get
            {
                return _listShopDays;
            }

            set
            {

                _listShopDays = value;
                OnPropertyChanged("ListShopDays");
           
            }
        }
        */




        public ObservableCollection<SaleDetails> ListSales { get; set; }


        public SaleDetails newSale { get; set; }
        public Products selectedProduct { get; set; }
        public SaleDetails selectedSale { get; set; }

        public DateTime selectedDay { get; set; }

        public Events newEvent { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }




        public ObservableCollection<Events> GetAllShopDays()
        {

            List<Events> list;
            ObservableCollection<Events> list2;
            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
            {
                list = db.GetAllWithChildren<Events>(recursive: true).OrderByDescending(x => DateTime.Parse(x.EventDate)).ToList();

            }
            list2 = new ObservableCollection<Events>(list);
            return list2;
        }





        RelayCommand _addSaleCommand;
        public ICommand AddSaleCommand
        {
            get
            {
                if (_addSaleCommand == null)
                {
                    _addSaleCommand = new RelayCommand(param => this.AddSale());
                }
                return _addSaleCommand;
            }
        }




        RelayCommand _delSaleCommand;
        public ICommand DeleteSaleCommand
        {
            get
            {
                if (_delSaleCommand == null)
                {
                    _delSaleCommand = new RelayCommand(param => this.DeleteSale());
                }
                return _delSaleCommand;
            }
        }



        RelayCommand _addEventCommand;
        public ICommand AddEventCommand
        {
            get
            {
                if (_addEventCommand == null)
                {
                    _addEventCommand = new RelayCommand(param => this.AddEvent());
                }
                return _addEventCommand;
            }
        }

        RelayCommand _deleteEventCommand;
        public ICommand DeleteEventCommand
        {
            get
            {
                if (_deleteEventCommand == null)
                {
                    _deleteEventCommand = new RelayCommand(param => this.DeleteEvent());
                }
                return _deleteEventCommand;
            }
        }



        private RelayCommand _editingCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_editingCommand == null)
                {
                    _editingCommand = new RelayCommand(param => this.EditSale());
                }
                return _editingCommand;
            }
        }

       

        private void EditSale()
        {

            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
            {

                db.Update(selectedSale);
                SaleSum = selectedEvent.ListSales.Sum(x => x.Price * x.Qty);
            }


        }

        private void AddEvent()
        {

            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
            {
                var c = EventDay;
                newEvent.EventTypeID = 4;
                // newEvent.EventDate = EventDay.ToString("dd-MM-yy"); ;

                DateTime date = new DateTime();
                date = DateTime.Parse(EventDay.ToShortDateString());


                CultureInfo polish = new CultureInfo("pl-PL");
                string day = polish.DateTimeFormat.DayNames[(int)date.DayOfWeek];

                newEvent.EventName = day + " " + date.ToString("dd-MM-yyyy");
               // newEvent.EventName = day + " " + date.ToString("yyyy-MM-dd");
                newEvent.EventDate = date.ToString("yyyy-MM-dd");

                db.Insert(newEvent);

                ListShopDays.Add(newEvent);
                ListShopDays = GetAllShopDays();

                newEvent = new Events();
                selectedEvent = new Events();

            }

        }

        private int _saleqty;
        public int SaleQty
        {
            get
            {
                return _saleqty;
            }

            set
            {

                _saleqty = value;
                if(selectedProduct!=null)
                ProdStock = selectedProduct.ProductStock  - _saleqty;

                OnPropertyChanged("SaleQty");
            }
        }



        public int SalePrice { get; set; }
        public bool SaleRegister { get; set; }
        public int _prodstock { get; set; }

        public int ProdStock
        {
            get
            {
                return _prodstock;
            }

            set
            {

                _prodstock = value;
              
                OnPropertyChanged("ProdStock");
            }
        }







        public DateTime EventDay { get; set; }

        public int _salesum { get; set; }

        public int SaleSum
        {
            get
            {
                return _salesum;
            }

            set
            {

                _salesum = value;
                OnPropertyChanged("SaleSum");
            }
        }


        private void AddSale()
        {
            if (selectedEvent.EventID== 0 )
            {

            }

            else
            {
                using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
                {
                    newSale.EventID = selectedEvent.EventID;
                    newSale.ProductID = selectedProduct.ProductID;
                    newSale.Prod = selectedProduct.Prod;
                    newSale.Qty = SaleQty;
                    newSale.Price = SalePrice;
                    newSale.IsRegister = SaleRegister;

  
                    db.Insert(newSale);
                    selectedProduct.ProductStock = ProdStock;
                    db.Update(selectedProduct);
                    SaleQty = 1;

              

                    selectedEvent.ListSales.Add(newSale);
                    selectedEvent.ListSales.OrderByDescending(x => x.SaleDetailID);
                    SaleSum = selectedEvent.ListSales.Sum(x => x.Price*x.Qty);

                    newSale = new SaleDetails();

               

                }
            }
        }


        private void DeleteSale()
        {
           
            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
            {

                var prod = db.Table<Products>().Where(x => selectedSale.ProductID == x.ProductID).FirstOrDefault();

                prod.ProductStock = prod.ProductStock + selectedSale.Qty;
                db.Update(prod);
                //  ListProducts = Common.GetAllProducts();
                var prod2 = ListProducts.Where(x => x.ProductID == prod.ProductID).FirstOrDefault();

                if(prod2!=null)  prod2.ProductStock = prod.ProductStock + selectedSale.Qty;


                db.Delete(selectedSale);
                selectedEvent.ListSales.Remove(selectedSale);
                SaleSum = selectedEvent.ListSales.Sum(x => x.Price * x.Qty);
                SaleQty = SaleQty;





            }
        
        }

        private void DeleteEvent()
        {
            var result = MessageBox.Show("Usunąć cały dzień","Ostrzeżenie",  MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
                {
                    db.DeleteAll(selectedEvent.ListSales);
                    db.Delete(selectedEvent);
                    ListShopDays.Remove(selectedEvent);
                }
            }
           

        }



        public ShopViewModel()
        {
            SaleQty = 1;
            ListShopDays = GetAllShopDays();
            ListProducts = Common.GetAllProducts();
            selectedEvent = new Events();
            selectedProduct = new Products();

            newSale = new SaleDetails();
            newEvent = new Events();
            selectedSale = new SaleDetails();
           // SaleSum = selectedEvent.ListSales.Sum(x => x.Price*x.Qty);


        }

 

    }
}
