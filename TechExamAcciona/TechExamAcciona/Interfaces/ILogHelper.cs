using log4net;

namespace ExamenTecnicoAcciona.Interfaces
{
    public interface ILogHelper<T>
    {
        ILog Logger { get; set; }
    }
}
