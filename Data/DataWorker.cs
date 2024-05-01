using khanami.Model;
using Microsoft.EntityFrameworkCore;

namespace khanami.Data
{
    public class DataWorker
    {

        private readonly DBContext _dbContext;
        public DataWorker(DBContext context)
        {
            _dbContext = context;
        }


        public List<Item> GetItems()
        {
            return _dbContext.Items.ToList();



        }

        public Item GetItem(int id,string name,string des,decimal price)
        {
            var NewItem= new Item { Id = id,Name = name,Description = des,Price = price };
            _dbContext.Items.Add(NewItem);

            return NewItem;
        }


    }
}
 