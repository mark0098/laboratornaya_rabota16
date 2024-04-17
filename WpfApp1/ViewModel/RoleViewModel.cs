using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Windows;
using WpfApp1.Helper;
using WpfApp1.Model;
using WpfApp1.View;

namespace WpfApp1.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private User selectedUser;
        public ObservableCollection<User> ListUser { get; set; } = new ObservableCollection<User>();

        readonly string path = @"..\..\..\DataModels\UserData.json";
        string _jsonUsers = string.Empty;

        public ObservableCollection<User>? LoadUser()
        {
            _jsonUsers = File.ReadAllText(path);

            if (_jsonUsers != null)
            {
                ListUser = JsonConvert.DeserializeObject<ObservableCollection<User>>(_jsonUsers);
                return ListUser;
            }

            else
            {
                return null;
            }
        }

        private void SaveChanges(ObservableCollection<User> listUser)
        {
            var jsonUser = JsonConvert.SerializeObject(listUser);
            try
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    writer.Write(jsonUser);
                }
            }
            catch (IOException e)
            {
                Error = "Ошибка записи json файла /n" + e.Message;
            }
        }

        public string Error { get; set; }
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
                EditUser.CanExecute(true);
            }
        }
        public UserViewModel()
        {

            ListUser = LoadUser();
            /*
            this.ListUser.Add(new User
            {
                Id = 1,
                NameUser = "Deadline090"
            });
            this.ListUser.Add(new User
            {
                Id = 2,
                NameUser = "mrusakov1"
            });
            this.ListUser.Add(new User
            {
                Id = 3,
                NameUser = "mark_hope"
            });
            */
        }

        private RelayCommand addUser;
        public RelayCommand AddUser
        {
            get
            {
                return addUser ??
                 (addUser = new RelayCommand(obj =>
                 {
                     WindowNewUser wnUser = new WindowNewUser
                     {
                         Title = "Новый пользователь",
                     };

                     int maxIdUser = MaxId() + 1;
                     User User = new User { Id = maxIdUser };
                     wnUser.DataContext = User;
                     if (wnUser.ShowDialog() == true)
                     {
                         ListUser.Add(User);
                         SaveChanges(ListUser);
                     }
                     SelectedUser = User;
                 }));
            }
        }

        #region EditUser
        private RelayCommand editUser;
        public RelayCommand EditUser
        {
            get
            {
                return editUser ??
                    (editUser = new RelayCommand(obj =>
                    {
                        WindowNewUser wnUser = new WindowNewUser()
                        { 
                            Title = "Редактирование пользователя", 
                        };
                        User user = SelectedUser;
                        User tempUser = new User();
                        tempUser = user.ShallowCopy();
                        wnUser.DataContext = tempUser;
                        if (wnUser.ShowDialog() == true)
                        {
                            user.NameUser = tempUser.NameUser;
                            List<User> listUser = ListUser.ToList();
                            SaveChanges(ListUser);
                        }
                    }, (obj) => SelectedUser != null && ListUser.Count > 0));
            }
        }
        #endregion
        private RelayCommand deleteUser;
        public RelayCommand DeleteUser
        {
            get
            {
                return deleteUser ??
                    (deleteUser = new RelayCommand(obj =>
                    {
                        User User = SelectedUser;
                        MessageBoxResult result = MessageBox.Show("Удалить пользователя: " +
                            User.NameUser, "Предупреждение", MessageBoxButton.OKCancel,
                            MessageBoxImage.Warning);
                        if (result == MessageBoxResult.OK)
                        {
                            ListUser.Remove(User);
                            SaveChanges(ListUser);
                        }
                    }, (obj) => SelectedUser != null && ListUser.Count > 0));
            }
        }

        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListUser)
            {
                if (max < r.Id)
                {
                    max = r.Id;
                };
            }
            return max;
        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")

        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}


