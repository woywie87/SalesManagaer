using Sales_WPF.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Sales_WPF
{
    class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<object> _children;

        public MainViewModel()
        {
            _children = new ObservableCollection<object>();
            _children.Add(new ProductsViewModel());
         _children.Add(new ProducersViewModel());
            _children.Add(new ShopViewModel());
            _children.Add(new CustomReportsViewModel());
        }

        public ObservableCollection<object> Children { get { return _children; } }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }





        RelayCommand _clickProductTab;
        public ICommand ClickProductTab
        {
            get
            {
                if (_clickProductTab == null)
                {
                
                    _clickProductTab = new RelayCommand(param => this.RefreshProductViewModel());
                }
                return _clickProductTab;
            }
        }


        private void RefreshProductViewModel()
        {


        //    _children[0] = new ProductsViewModel();
          

        }




    }
}
