using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfApp1.Helper;
using WpfApp1.Model;
using WpfApp1.View;


namespace WpfApp1.ViewModel
{
    public class ProductViewModel : INotifyPropertyChanged
    {


        public ObservableCollection<Product> ListPerson { get; set; } = new ObservableCollection<Product>();
        public ObservableCollection<ProductDPO> ListPersonDPO { get; set; } = new ObservableCollection<ProductDPO>();
        private List<User> Users;

        private UserViewModel vmUser;

        readonly string path = @"..\..\..\DataModels\ProductData.json";

        string _jsonProducts = String.Empty;
        public string Error { get; set; }

        public ObservableCollection<Product>? LoadProduct()
        {
            _jsonProducts = File.ReadAllText(path);
            if (_jsonProducts != null) 
            {
                ListPerson = JsonConvert.DeserializeObject<ObservableCollection<Product>>(_jsonProducts);
                return ListPerson;
            }

            else
            {
                return null;
            }
        }

        private void SaveChanges(ObservableCollection<Product> listProducts)
        {
            var JsonProduct = JsonConvert.SerializeObject(listProducts);

            try
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    writer.Write(JsonProduct);
                }
            }
            catch (IOException e) 
            { 
                Error = "Ошибка записи json файла /n" + e.Message;
            }
        }
        private ProductDPO selectedPersonDPO;
        public ProductDPO SelectedPersonDPO
        {
            get { return selectedPersonDPO; }
            set
            {
                selectedPersonDPO = value;
                OnPropertyChanged("SelectedPersonDpo");
            }
        }



        public ProductViewModel()
        {
            ListPerson = new ObservableCollection<Product>();
            ListPersonDPO = new ObservableCollection<ProductDPO>();
            ListPerson = LoadProduct();
            ListPersonDPO = GetListProductDPO();
            /*
            this.ListPerson.Add(
            new Product
            {
                Id = 1,
                UserId = 1,
                name = "Playstation 5",
                price = 80000
            });
            this.ListPerson.Add(
            new Product
            {
                Id = 2,
                UserId = 2,
                name = "XboX Series X",
                price = 50000
            });
            this.ListPerson.Add(
            new Product
            {
                Id = 3,
                UserId = 3,
                name = "iPhone 15 Pro Max",
                price = 150000
            });
            this.ListPerson.Add(
            new Product
            {
                Id = 4,
                UserId = 3,
                name = "XboX controller",
                price = 6000
            });
            ListPersonDPO = GetListProductDPO();
            */
        }

        #region AddPerson
        private RelayCommand addPerson;
        public RelayCommand AddPerson
        {
            get
            {
                return addPerson ??
                    (addPerson = new RelayCommand(obj =>
                    {
                        WindowNewProducts wnPerson = new WindowNewProducts
                        {
                            Title = "Новый сотрудник"
                        };

                        int maxIdPerson = MaxId() + 1;
                        ProductDPO per = new ProductDPO()
                        {
                            Id = maxIdPerson
                        };
                        wnPerson.DataContext = per;

                        vmUser = new UserViewModel();
                        Users = vmUser.ListUser.ToList();
                        wnPerson.CbUser.ItemsSource = Users;

                        if (wnPerson.ShowDialog() == true)
                        {
                            User r = (User)wnPerson.CbUser.SelectedValue;
                            per.User = r.NameUser;
                            ListPersonDPO.Add(per);
                            Product p = new Product();
                            p = p.CopyFromProductDPO(per);
                            ListPerson.Add(p);
                            SaveChanges(ListPerson);
                        }
                    }, (obj) => true));
            }
        }

        #endregion
        #region EditProduct
        private RelayCommand editProduct;
        public RelayCommand EditProduct
        {
            get
            {
                return editProduct ??
                    (editProduct = new RelayCommand(obj =>
                    {
                        WindowNewProducts wnProduct = new WindowNewProducts()
                        {
                            Title = "Редактирование данных товаров",
                        };
                        ProductDPO productDPO = SelectedPersonDPO;
                        ProductDPO tempProduct = new ProductDPO();
                        tempProduct = productDPO.ShallowCopy();
                        wnProduct.DataContext = tempProduct;

                        vmUser = new UserViewModel();
                        Users = vmUser.ListUser.ToList();
                        wnProduct.CbUser.ItemsSource = Users;


                        if (wnProduct.ShowDialog() == true)
                        {

                            User r = (User)wnProduct.CbUser.SelectedValue;
                            productDPO.User = r.NameUser;
                            productDPO.Name = tempProduct.Name;
                            productDPO.Price = tempProduct.Price;
                            FindPerson finder = new FindPerson(productDPO.Id);
                            List<Product> listPerson = ListPerson.ToList();
                            Product? p = listPerson.Find(new Predicate<Product>(finder.PersonPredicate));
                            p = p.CopyFromProductDPO(productDPO);
                            SaveChanges(ListPerson);
                        }
                    }, (obj) => SelectedPersonDPO != null && ListPerson.Count > 0));
            }
        }
        #endregion

        public ObservableCollection<ProductDPO> GetListProductDPO()
        {
            foreach (var person in ListPerson)
            {
                ProductDPO p = new ProductDPO();
                p = p.CopyFromPerson(person);
                ListPersonDPO.Add(p);
            }
            return ListPersonDPO;
        }

        #region DeletePerson

        private RelayCommand deletePerson;
        public RelayCommand DeletePerson
        {
            get
            {
                return deletePerson ??
                    (deletePerson = new RelayCommand(obj =>
                    {
                        ProductDPO product = SelectedPersonDPO;
                        MessageBoxResult result = MessageBox.Show("Удалить данные по товару: \n" +
                            product.Price + " " + product.Name, "Предупреждение", MessageBoxButton.OKCancel,
                            MessageBoxImage.Warning);
                        if (result == MessageBoxResult.OK)
                        {

                            Product per = new Product();
                            per = per.CopyFromProductDPO(product);
                            ListPerson.Remove(per);
                            ListPersonDPO.Remove(product);
                            SaveChanges(ListPerson);
                        }
                    }, (obj) => SelectedPersonDPO != null && ListPersonDPO.Count > 0));
            }
        }



        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListPerson)
            {
                if (max < r.Id)
                {
                    max = r.Id;
                };
            }
            return max;
        }

        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

