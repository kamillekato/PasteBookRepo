using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{
    public interface IRepository<C> where C : class
    {
        bool Add(C item);
        bool Update(C item);
        bool Remove(C item);
        bool Remove(Expression<Func<C,bool>> where);
        List<C> GetAll();
        List<C> GetList(Expression<Func<C,bool>> where);
        C Get(Expression<Func<C, bool>> where);
        C Get(Expression<Func<C, bool>> where, Func<C, C> selectProperty);
        string Get(Expression<Func<C, bool>> where, Func<C, string> selectProperty);
        int Get(Expression<Func<C, bool>> where, Func<C, int> selectProperty); 
    }
}
