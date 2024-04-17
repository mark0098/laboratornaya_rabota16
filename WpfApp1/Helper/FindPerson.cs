using WpfApp1.Model;

namespace WpfApp1.Helper
{
    public class FindPerson
    {
        public int id;
        public FindPerson(int id)
        {
            this.id = id;
        }
        public bool PersonPredicate(Product p)
        {
            return p.Id == id;
        }
    }
}
