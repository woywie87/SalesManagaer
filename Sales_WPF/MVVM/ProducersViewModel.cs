using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Sales_WPF.MVVM;


namespace Sales_WPF
{
    class ProducersViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Producers> _listProducers;


        public ObservableCollection<Producers> ListProducers
        {

            get { return _listProducers ?? (_listProducers = new ObservableCollection<Producers>()); }
            set { _listProducers = value; OnPropertyChanged("ListProducers"); }

        }

        public Producers newProducer { get; set; }
        public Producers selectedProducer { get; set; }




        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }




        public ObservableCollection<Producers> GetAllProducers()
        {
            List<Producers> list;
            ObservableCollection<Producers> list2;
            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
            {
                list = db.Table<Producers>().Where(x=>x.ProducerStatus==1).ToList();

            }
            list2 = new ObservableCollection<Producers>(list);
            return list2;
        }


        RelayCommand _addProducerCommand;
        public ICommand AddProducerCommand
        {
            get
            {
                if (_addProducerCommand == null)
                {
                    _addProducerCommand = new RelayCommand(param => this.AddProducer());
                }
                return _addProducerCommand;
            }
        }


        RelayCommand _delProducerCommand;
        public ICommand DelProducerCommand
        {
            get
            {
                if (_delProducerCommand == null)
                {
                    _delProducerCommand = new RelayCommand(param => this.DeleteProducer());
                }
                return _delProducerCommand;
            }
        }




        public ProducersViewModel()
        {

            ListProducers = GetAllProducers();
            newProducer = new Producers();
            selectedProducer = new Producers();


        }

        private void AddProducer()
        {
            var p = newProducer;
            p.ProducerStatus = 1;
          //  p.ProducerID = selectedProducer.ProducerID;
            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
            {
                db.Insert(p);
                ListProducers = GetAllProducers();


            }
          
        }


        private void DeleteProducer()
        {
            Producers prod;
            var vp = selectedProducer;

            
            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), "sale.db"))
            {
                prod = db.Table<Producers>().Where(x => x.ProducerID == vp.ProducerID).FirstOrDefault();
                prod.ProducerStatus = 0;
                db.Update(prod);

            }
            ListProducers = GetAllProducers();
            
        }



    }
}
