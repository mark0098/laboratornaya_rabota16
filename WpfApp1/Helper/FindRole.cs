using WpfApp1.Model;

namespace WpfApp1.Helper
{
    public class FindUser
    {
        int id;
        public FindUser(int id)
        {
            this.id = id;
        }
        public bool UserPredicate(User User)
        {
            return User.Id == id;
        }
    }
}
