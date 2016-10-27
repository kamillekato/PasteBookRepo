using PastebookDataLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess
{ 
    
    public class BaseRepository<C> : IRepository<C> where C : class
    {
        public virtual bool Add(C item)
        {
            bool returnValue = false;
            try
            {
                using(var context = new DB_PASTEBOOKEntities())
                {
                    context.Entry(item).State = EntityState.Added;
                    returnValue = context.SaveChanges() != 0;
                }
            }
            catch  
            {
                return false;
            }
            return returnValue;
        } 

        public virtual bool Remove(C item)
        {
            bool returnValue = false;
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    context.Entry(item).State = EntityState.Deleted;
                    returnValue = context.SaveChanges() != 0;
                }
            }
            catch 
            {

                throw;
            }
            return returnValue;
        } 
        
        public virtual bool Remove(Expression<Func<C,bool>> where)
        {
            bool returnValue = false;
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    var item = context.Set<C>().Where<C>(where).FirstOrDefault();
                    context.Entry(item).State = EntityState.Deleted;
                    returnValue = context.SaveChanges() != 0;
                }
            }
            catch 
            {
                return returnValue;
            }
            return returnValue;
        } 

        public virtual bool Update(C item)
        {
            bool returnValue = false;
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    context.Entry(item).State = EntityState.Modified;
                    returnValue = context.SaveChanges() != 0;
                }
            }
            catch
            {
                return false;
            }
            return returnValue;
        }

        public virtual List<C> GetAll()
        {
            List<C> getAll = new List<C>();
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    getAll = context.Set<C>().AsNoTracking().ToList<C>();
                }
            }
            catch
            {
                return null;
            }
            return getAll;
        } 

        public virtual List<C> GetList(Expression<Func<C,bool>> where)
        {
            List<C> getList = new List<C>();
            try
            {
                using (var context =new DB_PASTEBOOKEntities())
                {
                    getList = context.Set<C>().AsNoTracking()
                            .Where(where).ToList();
                }
            }
            catch 
            {
                return getList;
            }
            return getList;
        } 

        public virtual C Get(Expression<Func<C,bool>> where)
        {
            C item = null;
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    item = context.Set<C>().Where(where).FirstOrDefault(); 
                }
            }
            catch 
            {
                return item;
            }
            return item;
        }

        public virtual C Get(Expression<Func<C,bool>> where,Func<C,C> selectProperty)
        {
            C item = null;
            try
            {
                using (var context= new DB_PASTEBOOKEntities())
                {
                    item = context.Set<C>().Where(where).Select(selectProperty).FirstOrDefault();
                }
            }
            catch 
            {
                return item;
            }
            return item;
        }

        public virtual string Get(Expression<Func<C,bool>> where,Func<C,string> selectProperty)
        {
            string returnValue = string.Empty;
            try
            {
                using (var context =new DB_PASTEBOOKEntities())
                {
                    returnValue = context.Set<C>().Where(where).Select(selectProperty).FirstOrDefault();
                }
            }
            catch 
            {
                return returnValue;
            }
            return returnValue;
        }

        public virtual int Get(Expression<Func<C,bool>> where,Func<C,int> selectProperty) 
        {
            int returnValue = 0;
            try
            {
                using (var context = new DB_PASTEBOOKEntities())
                {
                    returnValue = context.Set<C>().Where(where).Select(selectProperty).FirstOrDefault();
                }
            }
            catch 
            {
                return returnValue;
            }
            return returnValue;
        }

        
    }
}
