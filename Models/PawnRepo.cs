using ThePlayfulPawn.Data;
using Microsoft.EntityFrameworkCore.

namespace ThePlayfulPawn.Models {
    public class PawnRepo<T> where T : class {
        private readonly PawnDbContext _context;
        private readonly DbSet<T> _dbSet;
        public PawnRepo (PawnDbContext context){
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IEnumerable<T> GetAll() => _dbSet.ToList();
        public T GetById (int id) => _dbSet.Find(id);
        public void Add(T item) 
        [
            _dbSet.Add(item);
            _context.SaveChanges();
        ]
        public void Update(T item){
            _dbSet.Update(item);
            _context.SaveChanges();
        }
        public void Delete(int id){
            var item = _dbSet.Find(id);
            if (item != null){
                _dbSet.Remove(item);
                _context.SaveChanges()
            }
        }
    }

}