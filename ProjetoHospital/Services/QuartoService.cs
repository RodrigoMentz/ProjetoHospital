namespace ProjetoHospital.Services
{
    using ProjetoHospital.Entities;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public class QuartoService(
        IGenericRepository<Quarto> quartoRepository)
        : IQuartoService
    {
        public async Task<ResponseModel<List<QuartoViewModel>>> GetAsync()
        {
            var quartos = await quartoRepository
                .FindAllAsync(q => !q.SoftDelete, q => q.Setor)
                .ConfigureAwait(false);

            var quartosViewModel = quartos
                .Select(q => new QuartoViewModel(
                    q.Id,
                    q.Nome,
                    q.Setor.Id,
                    q.Setor.Nome,
                    q.Capacidade,
                    q.Ativo))
                .ToList();

            var response = new ResponseModel<List<QuartoViewModel>>
            {
                Data = quartosViewModel
            };

            return response;
        }

        public async Task<ResponseModel> CriarAsync(
            QuartoViewModel quarto)
        {
            var quartoDb = new Quarto(
                quarto.Nome,
                quarto.IdSetor,
                quarto.Capacidade,
                quarto.Ativo);

            await quartoRepository
                .InsertAsync(quartoDb)
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }

        public async Task<ResponseModel> AtualizarAsync(
            QuartoViewModel quarto)
        {
            var quartoDb = await quartoRepository
                .FindAsync(quarto.Id)
                .ConfigureAwait(false);

            quartoDb.Nome = quarto.Nome;
            quartoDb.IdSetor = quarto.IdSetor;
            quartoDb.Capacidade = quarto.Capacidade;
            quartoDb.Ativo = quarto.Ativo;

            await quartoRepository
                .UpdateAsync(quartoDb)
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }

        public async Task<ResponseModel> DeletarAsync(
            QuartoViewModel quarto)
        {
            var quartoDb = await quartoRepository
                .FindAsync(quarto.Id)
                .ConfigureAwait(false);

            quartoDb.SoftDelete = true;

            await quartoRepository
                .UpdateAsync(quartoDb)
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }
    }
}