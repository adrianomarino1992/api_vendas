using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NSubstitute;
using MediatR;
using SistemaCompra.API.SolicitacaoCompra;
using SolicitacaoAgg = SistemaCompra.Application.SolicitacaoCompra;
using System.Threading.Tasks;


namespace SistemaCompra.API.Test.Controllers.SolicitacaoCompra
{
    public class TesteDeEndPointsSolicitacaoController
    {
        /*
         * 
         *  teste utilizando o NSubstitute para simular um mediator e testar APENAS o comportamento do 
         *  comtroller, pois os testes dos comandos e querys será feito em seu pacote de testes        
         * 
         */
        [Fact]
        public void TestarObterPorIdComNSubistitute()
        {
            IMediator mediator = NSubstitute.Substitute.For<IMediator>();
            var request = new SolicitacaoAgg.Query.ObterCompra.ObterSolicitacaoCompraPorIdQuery() 
            {
                Id = Guid.NewGuid()
            };

            Task r = new Task<SolicitacaoAgg.Query.ObterCompra.ObterSolicitacaoCompraPorIdResponse>(() =>
            {

                return new SolicitacaoAgg.Query.ObterCompra.ObterSolicitacaoCompraPorIdResponse()
                {
                    Message = "Exemplo de resposta",
                    Sucesso = true,
                    Solicitacao = new SolicitacaoAgg.Query.ObterCompra.ObterSolicitacaoCompraPorIdResponse.SolicitacaoCompra
                    {
                        DateTime = DateTime.Now.AddDays(-50),
                        NomeFornecedor = "Adriano from testes",
                        UsuarioSolicitante = "Adriano from testes",
                        Id = request.Id
                    }
                };

            });

            r.Start();

            mediator.Send(request).ReturnsForAnyArgs(r);

            SolicitacaoCompraController controller = new SolicitacaoCompraController(mediator);

            var result = controller.ObterPorId(request).Result;

            Assert.NotNull(result);
            Microsoft.AspNetCore.Mvc.ObjectResult okResult = result as Microsoft.AspNetCore.Mvc.ObjectResult;
            Assert.NotNull(okResult);
            var response = okResult.Value as SolicitacaoAgg.Query.ObterCompra.ObterSolicitacaoCompraPorIdResponse;
            Assert.NotNull(response);
            Assert.True(response.Sucesso);
            Assert.Equal(response.Solicitacao.Id,request.Id);

        }


        /*
        * 
        *  teste utilizando um IMediator FAKE para simular um mediator e testar APENAS o comportamento do 
        *  comtroller, pois os testes dos comandos e querys será feito em seu pacote de testes        
        * 
        */
        [Fact]
        public void TestarObterPorIdCOmFakeMediator()
        {
            var request = new SolicitacaoAgg.Query.ObterCompra.ObterSolicitacaoCompraPorIdQuery()
            {
                Id = Guid.NewGuid()
            };

            IMediator mediator = new FakeMediator(new Task<SolicitacaoAgg.Query.ObterCompra.ObterSolicitacaoCompraPorIdResponse>(() =>
            {

                return new SolicitacaoAgg.Query.ObterCompra.ObterSolicitacaoCompraPorIdResponse()
                {
                    Message = "Exemplo de resposta",
                    Sucesso = true,
                    Solicitacao = new SolicitacaoAgg.Query.ObterCompra.ObterSolicitacaoCompraPorIdResponse.SolicitacaoCompra
                    {
                        DateTime = DateTime.Now.AddDays(-50),
                        NomeFornecedor = "Adriano from testes",
                        UsuarioSolicitante = "Adriano from testes",
                        Id = request.Id
                    }
                };

            }));            

            SolicitacaoCompraController controller = new SolicitacaoCompraController(mediator);

            var result = controller.ObterPorId(request).Result;

            Assert.NotNull(result);
            Microsoft.AspNetCore.Mvc.ObjectResult okResult = result as Microsoft.AspNetCore.Mvc.ObjectResult;
            Assert.NotNull(okResult);
            var response = okResult.Value as SolicitacaoAgg.Query.ObterCompra.ObterSolicitacaoCompraPorIdResponse;
            Assert.NotNull(response);
            Assert.True(response.Sucesso);
            Assert.Equal(response.Solicitacao.Id, request.Id);

        }
    }


    
}
