using System;
using System.Linq;
using System.Collections.Generic;


using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;



namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }

        public CondicaoPagamento CondicaoPagamento { get; private set; }

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
            CondicaoPagamento = new CondicaoPagamento(0);
            Itens = new List<Item>();
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            if (produto == null)
                throw new BusinessRuleException("O item deve referenciar um produto!");

            if (qtde <= 0)
                throw new BusinessRuleException("O item deve ter uma quantidade maior que zero!");

            Itens.Add(new Item(produto, qtde));

            TotalGeral = (Money)Itens.Sum(u => u.Subtotal.Value);

            if ((decimal)TotalGeral > 50_000)
            {
                this.CondicaoPagamento = new CondicaoPagamento(30);
            }
        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {
            if (itens == null || itens.Count() == 0)
                throw new BusinessRuleException("A solicitação de compra deve possuir itens!");
        }
    }
}
