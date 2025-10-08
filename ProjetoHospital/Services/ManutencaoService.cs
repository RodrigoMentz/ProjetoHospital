namespace ProjetoHospital.Services
{
    using ProjetoHospital.Entities;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public class ManutencaoService(
        IGenericRepository<Manutencao> manutencaoRepository)
        : IManutencaoService
    {
        public async Task<ResponseModel> CriarAsync(
            ManutencaoViewModel manutencao)
        {
            var manutencaoDb = new Manutencao
            {
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