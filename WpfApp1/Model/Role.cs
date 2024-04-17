using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace WpfApp1.Model
{
    public class User : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string userName { get; set; }
        public string NameUser
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("userName");
            }
        }
        public User() { }
        public User(int id, string userName)
        {
            this.Id = id;
            this.NameUser = userName;
        }

        public User ShallowCopy()
        {
            return (User)this.MemberwiseClone();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]
string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
