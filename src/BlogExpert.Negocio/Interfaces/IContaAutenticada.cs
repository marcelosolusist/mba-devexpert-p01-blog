namespace BlogExpert.Negocio.Interfaces
{
    public interface IContaAutenticada
    {
        public string Email { get; }
        public bool EstaAutenticada { get; }
        public bool EhAdministrador { get; }
    }
}
