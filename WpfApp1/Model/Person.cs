using WpfApp1.ViewModel;

namespace WpfApp1.Model
{
    public class Product
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public Product() { }
        public Product(int id, int UserId, string name,
        int price)
        {
            this.Id = id;
            this.UserId = UserId;
            this.name = name;
            this.price = price;
        }

        // 13-14
        public Product CopyFromProductDPO(ProductDPO p)
        {
            UserViewModel vmUser = new UserViewModel();
            int UserId = 0;
            foreach (var r in vmUser.ListUser)
            {
                if (r.NameUser == p.User)
                {
                    UserId = r.Id;
                    break;
                }
            }
            if (UserId != 0)
            {
                this.Id = p.Id;
                this.UserId = UserId;
                this.name = p.Name;
                this.price = p.Price;
            }
            return this;
        }

    }
}
