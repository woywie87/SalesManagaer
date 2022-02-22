using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Sales_WPF.MVVM;
using SQLiteNetExtensions.Extensions;
using System.Windows.Threading;
using System.Windows;

namespace Sales_WPF
{
    class ProductsViewModel : INotifyPropertyChanged
    {

       // private static ProductsViewModel instance = new ProductsViewModel();

      //  public static ProductsViewModel Instance { get { return instance; } }
        private ObservableCollection<Products> _listproducts;


        public ObservableCollection<Products> ListProducts
        {

            get { return _listproducts ?? (_listproducts = new ObservableCollection<Products>()); }
            set { _listproducts = value; OnPropertyChanged("ListProducts"); }

        }
        public List<Producers> ListProducers { get; set; }
        public Products newProduct { get; set; }
        public Producers selectedProducer { get; set; }
        public Products selectedProduct { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }




        public List<Producers> GetAllProducers()
        {
            List<Producers> list;
            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
            {
                list = db.Table<Producers>().ToList();

            }
            return list;
        }


        RelayCommand _addProductCommand;
        public ICommand AddProductCommand
        {
            get
            {
                if (_addProductCommand == null)
                {
                    _addProductCommand = new RelayCommand(param => this.AddProduct());
                }
                return _addProductCommand;
            }
        }


        RelayCommand _delProductCommand;
        public ICommand DelProductCommand
        {
            get
            {
                if (_delProductCommand == null)
                {
                    _delProductCommand = new RelayCommand(param => this.DeleteProduct());
                }
                return _delProductCommand;
            }
        }









        public ProductsViewModel()
        {
            
            ListProducts = Common.GetAllProducts();

            ListProducers = GetAllProducers();
            newProduct = new Products();
            selectedProducer = new Producers();
            selectedProduct = new Products();



        }

        private void AddProduct()
        {
            var p = newProduct;
            if(p.ProductName!=null)
                {
                p.ProductStatus = 1;
                p.ProducerID = selectedProducer.ProducerID;
                using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
                {
                    db.Insert(p);
                    ListProducts = Common.GetAllProducts();


                }
            }
          
        }


        private void DeleteProduct()
        {
            Products prod;
            var vp = selectedProduct;

            
            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
            {
                prod = db.Table<Products>().Where(x => x.ProductID == vp.ProductID).FirstOrDefault();
                prod.ProductStatus = 0;
                db.Update(prod);

            }
            ListProducts = Common.GetAllProducts();
            
        }



    }
}
