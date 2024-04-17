using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp1.ViewModel;

namespace WpfApp1.Model
{
    public class ProductDPO : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string user { get; set; }
        public string User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("user");
            }
        }
        private string name { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        private int price { get; set; }
        public int Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        public ProductDPO() { }
        public ProductDPO(int id, string User, string name, int
        price)
        {
            this.Id = id;
            this.User = User;
            this.name = name;
            this.Price = price;
        }

        public ProductDPO ShallowCopy()
        {
            return (ProductDPO)this.MemberwiseClone();
        }

        public ProductDPO CopyFromPerson(Product person)
        {
            ProductDPO proDPO = new ProductDPO();
            UserViewModel vmUser = new UserViewModel();
            string User = string.Empty;
            foreach (var r in vmUser.ListUser)
            {
                if (r.Id == person.UserId)
                {
                    User = r.NameUser;
                    break;
                }
            }
            if (User != string.Empty)
            {
                proDPO.Id = person.Id;
                proDPO.User = User;
                proDPO.Name = person.name;
                proDPO.Price = person.price;
            }
            return proDPO;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
