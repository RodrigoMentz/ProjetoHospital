namespace ProjetoHospital.Services
{
    using Flunt.Notifications;
    using ProjetoHospital.Entities;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public class LeitoService(
        IGenericRepository<Leito> leitoRepository,
        IGenericRepository<Quarto> quartoRepository)
        : ILeitoService
    {
        public async Task<ResponseModel<List<LeitoViewModel>>> GetAsync()
        {
            var leitos = await leitoRepository
                .FindAllAsync(l => !l.SoftDelete,
                    l => l.Quarto,
                    l => l.Quarto.Setor)
                .ConfigureAwait(false);

            var leitosViewModel = leitos
                .Select(l => new LeitoViewModel(
                    l.Id,
                    l.Nome,
                    new QuartoViewModel(
                        l.IdQuarto,
                        l.Quarto.Nome,
                        l.Quarto.IdSetor,
                        l.Quarto.Setor.Nome),
                    l.Ativo))
                .ToList();

            var response = new ResponseModel<List<LeitoViewModel>>
            {
                Data = leitosViewModel
            };

            return response;
        }

        public async Task<ResponseModel> CriarAsync
            (LeitoViewModel leito)
        {
            var quarto = await quartoRepository
                .FindAsync(q => q.Id == leito.IdQuarto, q => q.Leitos)
                .ConfigureAwait(false);

            if (quarto.Capacidade < quarto.Leitos.Count(l => !l.SoftDelete) + 1)
            {
                return new ResponseModel(
                    new List<Notification>
                        {
                            new Notification("Leitos.Criar", "Capacidade do Quarto Atingida"),
                        });
            }

            var leitoDb = new Leito(
                leito.Nome,
                leito.IdQuarto,
                leito.Ativo);

            await leitoRepository
                .InsertAsync(leitoDb)
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }

        public async Task<ResponseModel> AtualizarAsync
            (LeitoViewModel leito)
        {
            var quarto = await quartoRepository
                .FindAsync(q => q.Id == leito.IdQuarto, q => q.Leitos)
                .ConfigureAwait(false);

            if (quarto.Capacidade < quarto.Leitos.Count(l => !l.SoftDelete) + 1)
            {
                return new ResponseModel(
                    new List<Notification>
                        {
                            new Notification("Leitos.Atualizar", "Capacidade do Quarto Atingida"),
                        });
            }

            var leitoDb = await leitoRepository
                .FindAsync(leito.Id)
                .ConfigureAwait(false);

            leitoDb.Nome = leito.Nome;
            leitoDb.IdQuarto = leito.IdQuarto;
            leitoDb.Ativo = leito.Ativo;

            await leitoRepository
                .UpdateAsync(leitoDb)
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }

        public async Task<ResponseModel> DeletarAsync
            (LeitoViewModel leito)
        {
            var leitoDb = await leitoRepository
                .FindAsync(leito.Id)
                .ConfigureAwait(false);

            leitoDb.SoftDelete = true;

            await leitoRepository
                .UpdateAsync(leitoDb)
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }
    }
}