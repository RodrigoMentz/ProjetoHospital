namespace ProjetoHospital.Services
{
    using ProjetoHospital.Entities;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public class ManutencaoService(
        IGenericRepository<Manutencao> manutencaoRepository)
        : IManutencaoService
    {
        public async Task<ResponseModel<List<ManutencaoViewModel>>> GetAsync()
        {
            var manutencoesDb = await manutencaoRepository
                .FindAllAsync(m => m.DataDeConclusao == null, m => m.Setor)
                .ConfigureAwait(false);

            var manutencoes = manutencoesDb
                .OrderByDescending(m => m.DataDeSolicitacao)
                .Select(m => new ManutencaoViewModel(
                    m.Id,
                    m.IdSolicitante,
                    m.NomeSolicitante,
                    m.ContatoSolicitante,
                    m.Turno,
                    m.Setor.Id,
                    m.Setor.Nome,
                    m.DataDeSolicitacao,
                    m.Descricao))
                .ToList();

            var response = new ResponseModel<List<ManutencaoViewModel>>
            {
                Data = manutencoes
            };

            return response;
        }

        public async Task<ResponseModel<ManutencaoViewModel>> GetDetalhesDaManutencaoAsync(
            ManutencaoViewModel manutencao)
        {
            var manutencaoDb = await manutencaoRepository
                .FindAsync(m => m.Id == manutencao.Id, m => m.Setor)
                .ConfigureAwait(false);

            var manutencaoResponse = new ManutencaoViewModel(
                manutencaoDb.Id,
                manutencaoDb.IdSolicitante,
                manutencaoDb.NomeSolicitante,
                manutencaoDb.ContatoSolicitante,
                manutencaoDb.Turno,
                manutencaoDb.Setor.Id,
                manutencaoDb.Setor.Nome,
                manutencaoDb.DataDeSolicitacao,
                manutencaoDb.Descricao);

            var response = new ResponseModel<ManutencaoViewModel>
            {
                Data = manutencaoResponse
            };

            return response;
        }

        public async Task<ResponseModel> CriarAsync(
            ManutencaoViewModel manutencao)
        {
            var manutencaoDb = new Manutencao
            {
                IdSolicitante = manutencao.IdSolicitante,
                NomeSolicitante = manutencao.NomeSolicitante,
                ContatoSolicitante = manutencao.ContatoSolicitante,
                Turno = manutencao.Turno,
                SetorId = manutencao.SetorId,
                DataDeSolicitacao = manutencao.DataDeSolicitacao,
                Descricao = manutencao.Descricao,
                Status = "Pendente",
            };

            await manutencaoRepository
                .InsertAsync(manutencaoDb)
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }

        public async Task<ResponseModel> AtualizarAsync(
            ManutencaoViewModel manutencao)
        {
            var manutencaoDb = await manutencaoRepository
                .FindAsync(manutencao.Id)
                .ConfigureAwait(false);

            manutencaoDb.NomeSolicitante = manutencao.NomeSolicitante;
            manutencaoDb.ContatoSolicitante = manutencao.ContatoSolicitante;
            manutencaoDb.Turno = manutencao.Turno;
            manutencaoDb.SetorId = manutencao.SetorId;
            manutencaoDb.DataDeSolicitacao = manutencao.DataDeSolicitacao;
            manutencaoDb.Descricao = manutencao.Descricao;

            await manutencaoRepository
                .UpdateAsync(manutencaoDb)
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }
    }
}