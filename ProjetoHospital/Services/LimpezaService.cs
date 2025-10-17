namespace ProjetoHospital.Services
{
    using Flunt.Notifications;
    using Microsoft.AspNetCore.SignalR;
    using ProjetoHospital.Entities;
    using ProjetoHospital.Hub;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    public class LimpezaService(
        IGenericRepository<Limpeza> limpezaRepository,
        IGenericRepository<Leito> leitoRepository,
        IGenericRepository<Revisao> revisaoRepository,
        IHubContext<AtualizacaoHub> atualizacaoHub)
        : ILimpezaService
    {
        public async Task<ResponseModel<List<LeitoStatusLimpezaViewModel>>> ConsultarListaStatusLimpezaAsync()
        {
            var leitos = await leitoRepository
                .FindAllAsync(
                    l => !l.SoftDelete
                    && l.Ativo,
                    l => l.Quarto,
                    l => l.Quarto.Setor);

            var leitoIds = leitos
                .Select(l => l.Id)
                .ToList();

            var limpezasHoje = await limpezaRepository
                .FindAllDerivedAsync<Limpeza>(
                    l => leitoIds.Contains(l.LeitoId)
                    && (l.DataFimLimpeza == null
                        || l.DataInicioLimpeza.Date == DateTime.Today),
                    l => l.Leito,
                    l => l.Usuario);

            var listaStatus = new List<LeitoStatusLimpezaViewModel>();

            foreach (var leito in leitos)
            {

                var existeLimpezaTerminalDepoisDaLiberacao = await limpezaRepository
                    .FindAllDerivedAsync<LimpezaTerminal>(
                        l => l.LeitoId == leito.Id
                        && leito.UltimaModificacao != null
                        && leito.UltimaModificacao != DateTime.MinValue
                        && l.DataInicioLimpeza.Date >= leito.UltimaModificacao.Value.Date)
                    .ConfigureAwait(false);

                var status = new LeitoStatusLimpezaViewModel
                {
                    LeitoId = leito.Id,
                    LeitoNome = leito.Nome,
                    QuartoNome = leito.Quarto.Nome,
                    QuartoId = leito.Quarto.Id,
                    SetorId = leito.Quarto.IdSetor,
                    SetorNome = leito.Quarto.Setor.Nome,
                    Ocupado = leito.Ocupado,
                    PrecisaLimpezaConcorrente = leito.Ocupado && !limpezasHoje.OfType<LimpezaConcorrente>().Any(l => l.LeitoId == leito.Id) && leito.UltimaModificacao.Value.Date != DateTime.Today.Date,
                    PrecisaLimpezaTerminal = !leito.Ocupado
                        && leito.UltimaModificacao != null
                        && existeLimpezaTerminalDepoisDaLiberacao.Count() == 0,
                    PrecisaDeRevisao = await PrecisaRevisaoParaAUltimaLimpezaDoLeito(leito.Id).ConfigureAwait(false),
                    PrecisaDeLimpezaDeRevisao = await ExisteRevisaoParaAUltimaLimpezaEPrecisaDeLimpezaDoLeito(leito.Id).ConfigureAwait(false),
                    PrecisaDeLimpezaEmergencial = limpezasHoje.OfType<LimpezaEmergencial>().Any(l => l.LeitoId == leito.Id && l.DataFimLimpeza == null && l.UsuarioId == null),
                    LimpezaEmAndamento = limpezasHoje.Any(l => l.LeitoId == leito.Id && l.DataFimLimpeza == null),
                    DataHoraUltimaLimpeza = await GetUltimaLimpezaDataAsync(leito.Id).ConfigureAwait(false)
                };

                listaStatus.Add(status);
            }

            return new ResponseModel<List<LeitoStatusLimpezaViewModel>>
            {
                Data = listaStatus
            };
        }

        private async Task<DateTime> GetUltimaLimpezaDataAsync(int leitoId)
        {
            var limpezas = await limpezaRepository
                .FindAllDerivedAsync<Limpeza>(l => l.LeitoId == leitoId);

            if (limpezas == null
                || !limpezas.Any())
            {
                return DateTime.MinValue;
            }

            var ultimaData = limpezas
                .Max(l => l.DataFimLimpeza ?? DateTime.MinValue);

            return ultimaData;
        }

        private async Task<bool> PrecisaRevisaoParaAUltimaLimpezaDoLeito(int leitoId)
        {
            var limpezas = await limpezaRepository
                .FindAllDerivedAsync<Limpeza>(l => l.LeitoId == leitoId);

            var ultimaLimpeza = limpezas
                .OrderByDescending(l => l.DataFimLimpeza)
                .FirstOrDefault();

            if ((ultimaLimpeza != null
                && ultimaLimpeza.Revisado)
                || ultimaLimpeza == null)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ExisteRevisaoParaAUltimaLimpezaEPrecisaDeLimpezaDoLeito(int leitoId)
        {
            var limpezas = await limpezaRepository
                .FindAllDerivedAsync<Limpeza>(l => l.LeitoId == leitoId);

            var ultimaLimpeza = limpezas
                .OrderByDescending(l => l.DataFimLimpeza)
                .FirstOrDefault();

            if (ultimaLimpeza != null)
            {
                var revisao = await revisaoRepository
                    .FindAsync(r => r.LimpezaId == ultimaLimpeza.Id);

                if (revisao != null
                    && revisao.NecessitaLimpeza
                    && revisao.DataFimLimpeza == null)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<ResponseModel<List<LimpezaViewModel>>> ConsultarLimpezasDoLeito(
            LeitoViewModel leito)
        {
            var limpezas = await limpezaRepository
                .FindAllDerivedAsync<Limpeza>(
                    l => l.LeitoId == leito.Id
                    && l.DataInicioLimpeza.Date >= DateTime.Now.AddDays(-30).Date,
                    l => l.Leito,
                    l => l.Usuario);

            var listaLimpezas = limpezas
                .Select(l => new LimpezaViewModel(
                    l.Id,
                    l.LeitoId,
                    l.Leito.Nome,
                    l.UsuarioId,
                    new UsuarioViewModel(
                        l.Usuario.Id,
                        l.Usuario.Nome),
                    l.TipoLimpeza,
                    l.DataInicioLimpeza,
                    l.DataFimLimpeza))
                .ToList();

            return new ResponseModel<List<LimpezaViewModel>>
            {
                Data = listaLimpezas
            };
        }

        public async Task<ResponseModel<List<LimpezaViewModel>>> ConsultarLimpezasNaoEncerradasDoUsuario(
            UsuarioViewModel usuario)
        {
            var limpezas = await limpezaRepository
                .FindAllDerivedAsync<Limpeza>(
                    l => l.UsuarioId == usuario.Id && l.DataFimLimpeza == null && l.TipoLimpeza != TipoLimpezaEnum.Emergencial,
                    l => l.Leito,
                    l => l.Leito.Quarto,
                    l => l.Leito.Quarto.Setor,
                    l => l.Usuario);

            var listaLimpezas = limpezas
                .Select(l => new LimpezaViewModel(
                    l.Id,
                    l.LeitoId,
                    l.Leito.Nome,
                    l.Leito.Quarto.Nome,
                    l.Leito.Quarto.IdSetor,
                    l.Leito.Quarto.Setor.Nome,
                    l.UsuarioId,
                    new UsuarioViewModel(
                        l.Usuario.Id,
                        l.Usuario.Nome),
                    l.TipoLimpeza,
                    l.DataInicioLimpeza,
                    l.DataFimLimpeza))
                .ToList();

            return new ResponseModel<List<LimpezaViewModel>>
            {
                Data = listaLimpezas
            };
        }

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

            await atualizacaoHub.Clients
                .Group($"atualizacao")
                .SendAsync(
                    "NovaAtualizacao")
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

            await atualizacaoHub.Clients
                .Group($"atualizacao")
                .SendAsync(
                    "NovaAtualizacao")
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }

        public async Task<ResponseModel<LimpezaConcorrenteViewModel>> ConsultarLimpezaConcorrenteAsync(
           LimpezaViewModel limpeza)
        {
            var limpezaDb = await limpezaRepository
                .FindDerivedAsync<LimpezaConcorrente>(
                    l => l.Id == limpeza.Id,
                    l => l.Leito,
                    l => l.Leito.Quarto);

            if (limpezaDb == null)
            {
                return new ResponseModel<LimpezaConcorrenteViewModel>(
                    null,
                    new List<Notification>
                        {
                            new Notification("Limpeza.ConsultarLimpeza", "Limpeza Inexistente"),
                        });
            }

            var limpezaConcorrente = new LimpezaConcorrenteViewModel(
                limpezaDb.TirarLixo,
                limpezaDb.LimparVasoSanitario,
                limpezaDb.LimparBox,
                limpezaDb.RevisarMofo,
                limpezaDb.LimparPia,
                limpezaDb.LimparCama,
                limpezaDb.LimparMesaCabeceira,
                limpezaDb.LimparLixeira);

            limpezaConcorrente.Id = limpezaDb.Id;
            limpezaConcorrente.LeitoNome = limpezaDb.Leito.Nome;
            limpezaConcorrente.NomeQuarto = limpezaDb.Leito.Quarto.Nome;

            var response = new ResponseModel<LimpezaConcorrenteViewModel>(limpezaConcorrente);

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

            var response = await limpezaRepository
                .InsertAsync(limpezaDb)
                .ConfigureAwait(false);

            await atualizacaoHub.Clients
                .Group($"atualizacao")
                .SendAsync(
                    "NovaAtualizacao")
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

            await atualizacaoHub.Clients
                .Group($"atualizacao")
                .SendAsync(
                    "NovaAtualizacao")
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }

        public async Task<ResponseModel<LimpezaTerminalViewModel>> ConsultarLimpezaTerminalAsync(
           LimpezaViewModel limpeza)
        {
            var limpezaDb = await limpezaRepository
                .FindDerivedAsync<LimpezaTerminal>(
                    l => l.Id == limpeza.Id,
                    l => l.Leito,
                    l => l.Leito.Quarto);

            if (limpezaDb == null)
            {
                return new ResponseModel<LimpezaTerminalViewModel>(
                    null,
                    new List<Notification>
                        {
                            new Notification("Limpeza.ConsultarLimpeza", "Limpeza Inexistente"),
                        });
            }

            var limpezaConcorrente = new LimpezaTerminalViewModel(
                limpezaDb.TirarLixo,
                limpezaDb.LimparVasoSanitario,
                limpezaDb.LimparBox,
                limpezaDb.RevisarMofo,
                limpezaDb.LimparPia,
                limpezaDb.LimparCama,
                limpezaDb.LimparEscadaCama,
                limpezaDb.LimparMesaCabeceira,
                limpezaDb.LimparArmario,
                limpezaDb.RecolherRoupaSuja,
                limpezaDb.RevisarPapelToalhaEHigienico,
                limpezaDb.LimparDispensers,
                limpezaDb.LimparLixeira,
                limpezaDb.LimparTeto,
                limpezaDb.LimparParedes,
                limpezaDb.LimparChao);

            limpezaConcorrente.Id = limpezaDb.Id;
            limpezaConcorrente.LeitoNome = limpezaDb.Leito.Nome;
            limpezaConcorrente.NomeQuarto = limpezaDb.Leito.Quarto.Nome;

            var response = new ResponseModel<LimpezaTerminalViewModel>(limpezaConcorrente);

            return response;
        }

        // TODO: criar cancelar limpeza

        public async Task<ResponseModel<LimpezaViewModel>> CriarLimpezaEmergencialAsync(
            LimpezaEmergencialViewModel limpeza)
        {
            var limpezaDb = new LimpezaEmergencial
            {
                LeitoId = limpeza.LeitoId,
                TipoLimpeza = TipoLimpezaEnum.Emergencial,
                Descricao = limpeza.Descricao,
                IdSolicitante = limpeza.SolicitanteId,
            };

            var response = await limpezaRepository
                .InsertAsync(limpezaDb)
                .ConfigureAwait(false);

            await atualizacaoHub.Clients
                .Group($"atualizacao")
                .SendAsync(
                    "NovaAtualizacao")
                .ConfigureAwait(false);

            var responseModel = new ResponseModel<LimpezaViewModel>
            {
                Data = new LimpezaViewModel(response.Id)
            };

            return responseModel;
        }

        public async Task<ResponseModel<LimpezaViewModel>> AtenderLimpezaEmergencialAsync(
            LimpezaEmergencialViewModel limpeza)
        {
            var limpezaDb = await limpezaRepository
                .FindAsync(limpeza.Id)
                .ConfigureAwait(false);

            limpezaDb.DataInicioLimpeza = limpeza.DataInicioLimpeza ?? DateTime.Now;
            limpezaDb.UsuarioId = limpeza.UsuarioId;

            await limpezaRepository
                .UpdateAsync(limpezaDb)
                .ConfigureAwait(false);

            var responseModel = new ResponseModel<LimpezaViewModel>
            {
                Data = new LimpezaViewModel(limpeza.Id)
            };

            await atualizacaoHub.Clients
                .Group($"atualizacao")
                .SendAsync(
                    "NovaAtualizacao")
                .ConfigureAwait(false);

            return responseModel;
        }

        public async Task<ResponseModel<LimpezaEmergencialViewModel>> ConsultarLimpezaEmergencialAsync(
           LimpezaViewModel limpeza)
        {
            var limpezaDb = await limpezaRepository
                .FindDerivedAsync<LimpezaEmergencial>(
                    l => l.Id == limpeza.Id,
                    l => l.Leito,
                    l => l.Leito.Quarto,
                    l => l.Leito.Quarto.Setor,
                    l => l.Solicitante);

            if (limpezaDb == null)
            {
                return new ResponseModel<LimpezaEmergencialViewModel>(
                    null,
                    new List<Notification>
                        {
                            new Notification("Limpeza.ConsultarLimpeza", "Limpeza Inexistente"),
                        });
            }

            var limpezaEmergencial = new LimpezaEmergencialViewModel(
                limpezaDb.Id,
                limpezaDb.DataInicioLimpeza,
                limpezaDb.DataFimLimpeza ?? DateTime.MinValue,
                limpezaDb.Descricao,
                limpezaDb.LeitoId,
                limpezaDb.IdSolicitante,
                limpezaDb.Solicitante.Nome,
                limpezaDb.Leito.Nome,
                limpezaDb.Leito.Quarto.Nome,
                limpezaDb.Leito.Quarto.IdSetor,
                limpezaDb.Leito.Quarto.Setor.Nome,
                limpezaDb.UsuarioId,
                limpezaDb.Revisado);

            var response = new ResponseModel<LimpezaEmergencialViewModel>(limpezaEmergencial);

            return response;
        }

        public async Task<ResponseModel<List<LimpezaEmergencialViewModel>>> ConsultarLimpezasEmergenciais()
        {
            var limpezas = await limpezaRepository
                .FindAllDerivedAsync<LimpezaEmergencial>(
                    l => l.DataFimLimpeza == null,
                    l => l.Leito,
                    l => l.Leito.Quarto,
                    l => l.Leito.Quarto.Setor,
                    l => l.Usuario,
                    l => l.Solicitante);

            var listaLimpezas = limpezas
                .Select(l => new LimpezaEmergencialViewModel(
                    l.Id,
                    l.DataInicioLimpeza,
                    l.DataFimLimpeza,
                    l.Descricao,
                    l.LeitoId,
                    l.IdSolicitante,
                    l.Solicitante.Nome,
                    l.Leito.Nome,
                    l.Leito.Quarto.Nome,
                    l.Leito.Quarto.IdSetor,
                    l.Leito.Quarto.Setor.Nome,
                    l.UsuarioId,
                    l.Revisado))
                .ToList();

            return new ResponseModel<List<LimpezaEmergencialViewModel>>
            {
                Data = listaLimpezas
            };
        }

        public async Task<ResponseModel> FinalizarLimpezaEmergencialAsync(
            LimpezaEmergencialViewModel limpeza)
        {
            var limpezaDbExistente = await limpezaRepository
                .FindDerivedAsync<LimpezaEmergencial>(
                    l => l.Id == limpeza.Id,
                    l => l.Leito,
                    l => l.Usuario);

            if (limpezaDbExistente == null)
            {
                return new ResponseModel(
                    new List<Notification>
                        {
                            new Notification("Limpeza.FinalizarEmergencial", "Limpeza Inexistente"),
                        });
            }

            limpezaDbExistente.DataFimLimpeza = DateTime.Now;

            await limpezaRepository
                .UpdateAsync(limpezaDbExistente)
                .ConfigureAwait(false);

            await atualizacaoHub.Clients
                .Group($"atualizacao")
                .SendAsync(
                    "NovaAtualizacao")
                .ConfigureAwait(false);

            var response = new ResponseModel();

            return response;
        }
    }
}