using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SistemaCompra.Infra.Data
{
    public class Repository<T> : SistemaCompra.Domain.Core.IRepository<T> where T : SistemaCompra.Domain.Core.Model.Entity
    {
        protected readonly SistemaCompraContext _context;

        public Repository(SistemaCompraContext context)
        {
            this._context = context;
        }

        public void Atualizar(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Excluir(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public T Obter(Guid id)
        {
            return _context.Set<T>().Where(c => c.Id == id).FirstOrDefault();
        }

        public void Registrar(T entity)
        {
            _context.Set<T>().Add(entity);
        }
    }
}
