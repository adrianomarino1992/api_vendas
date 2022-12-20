using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCompra.Application.SolicitacaoCompra.Query.ObterCompra
{
    public class ObterSolicitacaoCompraPorIdResponse
    {
        public Exception Exception { get; set; }
        public string Message { get; set; }
        public SolicitacaoCompra Solicitacao { get; set; } 
        public bool Sucesso{ get; set; }

       public class SolicitacaoCompra
        {

            public string UsuarioSolicitante { get; set; }
            public string NomeFornecedor { get; set; }
            public Guid Id { get; set; }
            public DateTime DateTime { get; set; }

            public List<SolicitacaoCompra.Item> Itens = new List<Item>();
          

            public class Item
            {
                public Guid ProdutoId { get; set; }
                public String Produto { get; set; }
                public int Qtd { get; set; }

                public decimal SubTotal { get; set; }
            }
        }

       

    }
}

