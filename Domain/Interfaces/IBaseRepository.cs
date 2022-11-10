using System;

namespace Domain.Interfaces
{
	public interface IBaseRepository<T> 
	{
        public T Create(T item);
        public T Update(T entity);
        public T Get(int id);
        public bool Exists(int id);
        public bool Delete(int id);
        public bool IsValid(T entity);
        public IEnumerable<T> List();
    }
}

