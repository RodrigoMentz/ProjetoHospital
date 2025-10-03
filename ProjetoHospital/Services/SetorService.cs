namespace ProjetoHospital.Services
{
    using ProjetoHospital.Entities;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public class SetorService(
        IGenericRepository<Setor> setorRepository)
        : ISetorService
    {

        public async Task<ResponseModel<List<SetorViewModel>>> GetAsync()
        {
            var setores = await setorRepository
                .FindAllAsync(s => !s.SoftDelete)
                .ConfigureAwait(false);

            var setoresViewModel = setores
                .Select(s => new SetorViewModel(
                    s.Id,
                    s.Nome,
                    s.Ativo))
                .ToList();

            var response = new ResponseModel<List<SetorViewModel>>
            {
                Data = setoresViewModel
            };

            return response;
        }

        public async Task<ResponseModel> CriarAsync
            (SetorViewModel setor)
        {
            var setorDb = new Setor(
                setor.Nome,
                setor.Ativo);

            await setorRepository
                .InsertAsync(setorDb)
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }

        public async Task<ResponseModel> AtualizarAsync
            (SetorViewModel setor)
        {
            var setorDb = await setorRepository
                .FindAsync(setor.Id)
                .ConfigureAwait(false);

            setorDb.Nome = setor.Nome;
            setorDb.Ativo = setor.Ativo;

            await setorRepository
                .UpdateAsync(setorDb)
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }

        public async Task<ResponseModel> DeletarAsync
            (SetorViewModel setor)
        {
            var setorDb = await setorRepository
                .FindAsync(setor.Id)
                .ConfigureAwait(false);

            setorDb.SoftDelete = true;

            await setorRepository
                .UpdateAsync(setorDb)
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }
    }
}