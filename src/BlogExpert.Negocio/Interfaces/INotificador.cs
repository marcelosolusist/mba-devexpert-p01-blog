using BlogExpert.Negocio.Notificacoes;

namespace BlogExpert.Negocio.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Adicionar(Notificacao notificacao);
    }
}
