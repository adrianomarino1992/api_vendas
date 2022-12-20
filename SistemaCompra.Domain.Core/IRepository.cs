using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCompra.Domain.Core
{
    public interface IRepository<T> where T : Model.Entity
    {
        T Obter(Guid id);
        void Registrar(T entity);
        void Atualizar(T entity);
        void Excluir(T entity);
    }
}
