namespace ProjetoHospital.Services
{
    using ProjetoHospital.Entities;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public interface IUsuarioService
    {
        Task<ResponseModel<Usuario>> Cadastrar(UsuarioViewModel cadastro);

        Task<ResponseModel<List<PerfilViewModel>>> GetPerfis();
    }
}