using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarSolicitacaoCompraCommand : IRequest<RegistrarSolicitacaoCompraResponse>
    {
        public string UsuarioSolicitante { get; set; }
        public string NomeFornecedor { get; set; }
        public IList<RegistrarSolicitacaoCompraCommand.Item> Itens { get; set; }  

        public class Item 
        {
            public Guid ProdutoId { get; set; }

            public int Qtd { get; set; }
        }
    }
}

