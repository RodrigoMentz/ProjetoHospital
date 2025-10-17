namespace ProjetoHospital.Services
{
    using Microsoft.AspNetCore.SignalR;
    using ProjetoHospital.Entities;
    using ProjetoHospital.Hub;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public class RevisaoService(
        IGenericRepository<Revisao> revisaoRepository,
        IGenericRepository<Limpeza> limpezaRepository,
        IHubContext<AtualizacaoHub> atualizacaoHub)
        : IRevisaoService
    {
        public async Task<ResponseModel<RevisaoViewModel>> GetDetalhesDaRevisaoAsync(
            RevisaoViewModel revisao)
        {
            var revisaoDb = await revisaoRepository
                .FindAsync(r => r.Id == revisao.Id, r => r.Leito, r => r.Leito.Quarto, r => r.Leito.Quarto.Setor)
                .ConfigureAwait(false);

            var revisaoResponse = new RevisaoViewModel(
                revisaoDb.Id,
                revisaoDb.Observacoes,
                revisaoDb.DataSolicitacao,
                revisaoDb.DataInicioLimpeza,
                revisaoDb.DataFimLimpeza,
                revisaoDb.SolicitanteId,
                revisaoDb.ExecutanteId,
                revisaoDb.LimpezaId,
                revisaoDb.LeitoId,
                revisaoDb.Leito.Nome,
                revisaoDb.Leito.Quarto.Nome,
                revisaoDb.Leito.Quarto.IdSetor,
                revisaoDb.Leito.Quarto.Setor.Nome);

            var response = new ResponseModel<RevisaoViewModel>
            {
                Data = revisaoResponse
            };

            return response;
        }

        public async Task<ResponseModel<List<RevisaoViewModel>>> GetRevisoesQueNecessitamLimpezaAsync()
        {
            var revisoesDb = await revisaoRepository
                .FindAllAsync(r => r.DataFimLimpeza == null && r.NecessitaLimpeza && r.ExecutanteId == null, r => r.Leito, r => r.Leito.Quarto, r => r.Leito.Quarto.Setor)
                .ConfigureAwait(false);

            var listaRevisao =  revisoesDb.Select(r => new RevisaoViewModel(
                r.Id,
                r.Observacoes,
                r.DataSolicitacao,
                r.DataInicioLimpeza,
                r.DataFimLimpeza,
                r.SolicitanteId,
                r.ExecutanteId,
                r.LimpezaId,
                r.LeitoId,
                r.Leito.Nome,
                r.Leito.Quarto.Nome,
                r.Leito.Quarto.IdSetor,
                r.Leito.Quarto.Setor.Nome))
                .ToList();

            var response = new ResponseModel<List<RevisaoViewModel>>
            {
                Data = listaRevisao
            };

            return response;
        }

        public async Task<ResponseModel<List<RevisaoViewModel>>> GetRevisoesQueNecessitamLimpezaENaoForamTerminadasPeloUsuarioAsync(
            UsuarioViewModel usuario)
        {
            var revisoesDb = await revisaoRepository
                .FindAllAsync(r => r.DataFimLimpeza == null && r.NecessitaLimpeza && r.ExecutanteId == usuario.Id, r => r.Leito, r => r.Leito.Quarto, r => r.Leito.Quarto.Setor)
                .ConfigureAwait(false);

            var listaRevisao = revisoesDb.Select(r => new RevisaoViewModel(
                r.Id,
                r.Observacoes,
                r.DataSolicitacao,
                r.DataInicioLimpeza,
                r.DataFimLimpeza,
                r.SolicitanteId,
                r.ExecutanteId,
                r.LimpezaId,
                r.LeitoId,
                r.Leito.Nome,
                r.Leito.Quarto.Nome,
                r.Leito.Quarto.IdSetor,
                r.Leito.Quarto.Setor.Nome))
                .ToList();

            var response = new ResponseModel<List<RevisaoViewModel>>
            {
                Data = listaRevisao
            };

            return response;
        }

        public async Task<ResponseModel<List<NecessidadeDeRevisaoViewModel>>> ConsultarLimpezasQuePrecisamDeRevisaoAsync(
            UsuarioViewModel usuario)
        {
            var limpezasFinalizadasSemRevisao = await limpezaRepository
                .FindAllDerivedAsync<Limpeza>(
                    l => l.DataFimLimpeza != null
                        && !l.Revisado,
                    l => l.Leito,
                    l => l.Leito.Quarto,
                    l => l.Leito.Quarto.Setor);

            var revisoesNaoTerminadas = await revisaoRepository
                .FindAllAsync(r => r.DataSolicitacao == DateTime.MinValue && r.SolicitanteId == usuario.Id,
                    r => r.Leito,
                    r => r.Leito.Quarto,
                    r => r.Leito.Quarto.Setor,
                    r => r.Limpeza);

            //TODO: ver revisoes que foram iniciadas e nao foram terminadas

            var revisoesNecessarias = new List<NecessidadeDeRevisaoViewModel>();

            foreach (var limpezaSemRevisao in limpezasFinalizadasSemRevisao)
            {
                var necessitaRevisao = new NecessidadeDeRevisaoViewModel(
                    limpezaSemRevisao.Id,
                    limpezaSemRevisao.LeitoId,
                    limpezaSemRevisao.Leito.Nome,
                    limpezaSemRevisao.Leito.Quarto.Nome,
                    limpezaSemRevisao.Leito.Quarto.Setor.Nome,
                    limpezaSemRevisao.TipoLimpeza);

                revisoesNecessarias.Add(necessitaRevisao);
            }

            foreach (var revisao in revisoesNaoTerminadas)
            {
                var necessitaRevisao = new NecessidadeDeRevisaoViewModel(
                    revisao.LimpezaId,
                    revisao.LeitoId,
                    revisao.Leito.Nome,
                    revisao.Leito.Quarto.Nome,
                    revisao.Leito.Quarto.Setor.Nome,
                    revisao.Limpeza.TipoLimpeza,
                    revisao.Id,
                    revisao.SolicitanteId);

                revisoesNecessarias.Add(necessitaRevisao);
            }

            var response = new ResponseModel<List<NecessidadeDeRevisaoViewModel>>(revisoesNecessarias);

            return response;
        }

        public async Task<ResponseModel<RevisaoViewModel>> CriarAsync(
            RevisaoViewModel revisao)
        {
            var revisaoDb = new Revisao(
               revisao.Observacoes,
               revisao.NecessitaLimpeza,
               revisao.DataSolicitacao = DateTime.MinValue,
               revisao.SolicitanteId,
               revisao.LimpezaId,
               revisao.LeitoId);

            await revisaoRepository
                .InsertAsync(revisaoDb)
                .ConfigureAwait(false);

            var limpezaDb = await limpezaRepository
                .FindAsync(revisaoDb.LimpezaId)
                .ConfigureAwait(false);

            limpezaDb.Revisado = true;

            await limpezaRepository
                .UpdateAsync(limpezaDb)
                .ConfigureAwait(false);

            var responseRevisao = new RevisaoViewModel(revisaoDb.Id);

            var response = new ResponseModel<RevisaoViewModel>(responseRevisao);

            return response;
        }

        public async Task<ResponseModel> AtualizarAsync(
            RevisaoViewModel revisao)
        {
            var revisaoDb = await revisaoRepository
                .FindAsync(revisao.Id)
                .ConfigureAwait(false);

            revisaoDb.Observacoes = revisao.Observacoes;
            revisaoDb.NecessitaLimpeza = revisao.NecessitaLimpeza;
            revisaoDb.DataSolicitacao = DateTime.Now;

            await revisaoRepository
                .UpdateAsync(revisaoDb)
                .ConfigureAwait(false);

            await atualizacaoHub.Clients
                .Group($"atualizacao")
                .SendAsync(
                    "NovaAtualizacao")
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }

        public async Task<ResponseModel> AtenderAsync(
            RevisaoViewModel revisao)
        {
            var revisaoDb = await revisaoRepository
                .FindAsync(revisao.Id)
                .ConfigureAwait(false);

            revisaoDb.DataInicioLimpeza = revisao.DataInicioLimpeza;
            revisaoDb.ExecutanteId = revisao.ExecutanteId;

            await revisaoRepository
                .UpdateAsync(revisaoDb)
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }

        public async Task<ResponseModel> FinalizarAsync(
           RevisaoViewModel revisao)
        {
            var revisaoDb = await revisaoRepository
                .FindAsync(revisao.Id)
                .ConfigureAwait(false);

            revisaoDb.DataFimLimpeza = revisao.DataFimLimpeza;

            await revisaoRepository
                .UpdateAsync(revisaoDb)
                .ConfigureAwait(false);

            await atualizacaoHub.Clients
                .Group($"atualizacao")
                .SendAsync(
                    "NovaAtualizacao")
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }

        public async Task<ResponseModel> DeletarAsync(
            RevisaoViewModel revisao)
        {
            var revisaoDb = await revisaoRepository
                .FindAsync(revisao.Id)
                .ConfigureAwait(false);

            revisaoDb.SoftDelete = true;

            await revisaoRepository
                .UpdateAsync(revisaoDb)
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }
    }
}