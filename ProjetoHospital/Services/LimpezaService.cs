namespace ProjetoHospital.Services
{
    using Flunt.Notifications;
    using ProjetoHospital.Entities;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public class LimpezaService(
        IGenericRepository<Limpeza> limpezaRepository)
        : ILimpezaService
    {
        public async Task<ResponseModel<LimpezaViewModel>> CriarLimpezaConcorrenteAsync(
            LimpezaConcorrenteViewModel limpeza)
        {
            var limpezaDb = new LimpezaConcorrente
            {
                DataInicioLimpeza = DateTime.Now,
                LeitoId = limpeza.LeitoId,
                UsuarioId = limpeza.UsuarioId,
                TipoLimpeza = TipoLimpezaEnum.Concorrente,
                TirarLixo = limpeza.TirarLixo,
                LimparVasoSanitario = limpeza.LimparVasoSanitario,
                LimparBox = limpeza.LimparBox,
                RevisarMofo = limpeza.RevisarMofo,
                LimparPia = limpeza.LimparPia,
                LimparCama = limpeza.LimparCama,
                LimparMesaCabeceira = limpeza.LimparMesaCabeceira,
                LimparLixeira = limpeza.LimparLixeira
            };

            var response = await limpezaRepository
                .InsertAsync(limpezaDb)
                .ConfigureAwait(false);

            var responseModel = new ResponseModel<LimpezaViewModel>
            {
                Data = new LimpezaViewModel(response.Id)
            };

            return responseModel;
        }

        public async Task<ResponseModel> FinalizarLimpezaConcorrenteAsync(
            LimpezaConcorrenteViewModel limpeza)
        {
            var limpezaDbExistente = await limpezaRepository
                .FindDerivedAsync<LimpezaConcorrente>(
                    l => l.Id == limpeza.Id,
                    l => l.Leito,
                    l => l.Usuario);

            if (limpezaDbExistente == null)
            {
                return new ResponseModel(
                    new List<Notification>
                        {
                            new Notification("Limpeza.FinalizarConcorrente", "Limpeza Inexistente"),
                        });
            }

            limpezaDbExistente.DataFimLimpeza = DateTime.Now;
            limpezaDbExistente.TirarLixo = limpeza.TirarLixo;
            limpezaDbExistente.LimparVasoSanitario = limpeza.LimparVasoSanitario;
            limpezaDbExistente.LimparBox = limpeza.LimparBox;
            limpezaDbExistente.RevisarMofo = limpeza.RevisarMofo;
            limpezaDbExistente.LimparPia = limpeza.LimparPia;
            limpezaDbExistente.LimparCama = limpeza.LimparCama;
            limpezaDbExistente.LimparMesaCabeceira = limpeza.LimparMesaCabeceira;
            limpezaDbExistente.LimparLixeira = limpeza.LimparLixeira;

            await limpezaRepository
                .UpdateAsync(limpezaDbExistente)
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }

        public async Task<ResponseModel<LimpezaViewModel>> CriarLimpezaTerminalAsync(
            LimpezaTerminalViewModel limpeza)
        {
            var limpezaDb = new LimpezaTerminal
            {
                DataInicioLimpeza = DateTime.Now,
                LeitoId = limpeza.LeitoId,
                UsuarioId = limpeza.UsuarioId,
                TipoLimpeza = TipoLimpezaEnum.Terminal,
                TirarLixo = limpeza.TirarLixo,
                LimparVasoSanitario = limpeza.LimparVasoSanitario,
                LimparBox = limpeza.LimparBox,
                RevisarMofo = limpeza.RevisarMofo,
                LimparPia = limpeza.LimparPia,
                LimparCama = limpeza.LimparCama,
                LimparEscadaCama = limpeza.LimparEscadaCama,
                LimparMesaCabeceira = limpeza.LimparMesaCabeceira,
                LimparArmario = limpeza.LimparArmario,
                RecolherRoupaSuja = limpeza.RecolherRoupaSuja,
                RevisarPapelToalhaEHigienico = limpeza.RevisarPapelToalhaEHigienico,
                LimparDispensers = limpeza.LimparDispensers,
                LimparLixeira = limpeza.LimparLixeira,
                LimparTeto = limpeza.LimparTeto,
                LimparParedes = limpeza.LimparParedes,
                LimparChao = limpeza.LimparChao
            };

            await limpezaRepository
                .InsertAsync(limpezaDb)
                .ConfigureAwait(false);

            var response = await limpezaRepository
                .InsertAsync(limpezaDb)
                .ConfigureAwait(false);

            var responseModel = new ResponseModel<LimpezaViewModel>
            {
                Data = new LimpezaViewModel(response.Id)
            };

            return responseModel;
        }

        public async Task<ResponseModel> FinalizarLimpezaTerminalAsync(
            LimpezaTerminalViewModel limpeza)
        {
            var limpezaDbExistente = await limpezaRepository
                .FindDerivedAsync<LimpezaTerminal>(
                    l => l.Id == limpeza.Id,
                    l => l.Leito,
                    l => l.Usuario);

            if (limpezaDbExistente == null)
            {
                return new ResponseModel(
                    new List<Notification>
                        {
                            new Notification("Limpeza.FinalizarTerminal", "Limpeza Inexistente"),
                        });
            }


            limpezaDbExistente.DataFimLimpeza = DateTime.Now;
            limpezaDbExistente.TirarLixo = limpeza.TirarLixo;
            limpezaDbExistente.LimparVasoSanitario = limpeza.LimparVasoSanitario;
            limpezaDbExistente.LimparBox = limpeza.LimparBox;
            limpezaDbExistente.RevisarMofo = limpeza.RevisarMofo;
            limpezaDbExistente.LimparPia = limpeza.LimparPia;
            limpezaDbExistente.LimparCama = limpeza.LimparCama;
            limpezaDbExistente.LimparEscadaCama = limpeza.LimparEscadaCama;
            limpezaDbExistente.LimparMesaCabeceira = limpeza.LimparMesaCabeceira;
            limpezaDbExistente.LimparArmario = limpeza.LimparArmario;
            limpezaDbExistente.RecolherRoupaSuja = limpeza.RecolherRoupaSuja;
            limpezaDbExistente.RevisarPapelToalhaEHigienico = limpeza.RevisarPapelToalhaEHigienico;
            limpezaDbExistente.LimparDispensers = limpeza.LimparDispensers;
            limpezaDbExistente.LimparLixeira = limpeza.LimparLixeira;
            limpezaDbExistente.LimparTeto = limpeza.LimparTeto;
            limpezaDbExistente.LimparParedes = limpeza.LimparParedes;
            limpezaDbExistente.LimparChao = limpeza.LimparChao;

            await limpezaRepository
                .UpdateAsync(limpezaDbExistente)
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }
    }
}