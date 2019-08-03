using System.Threading.Tasks;

namespace Pandemonium.Classes
{
    public abstract class ActionBase
    {
        abstract public Task DoChaos { get; set; }

        abstract public Task UndoChaos { get; set; }
    }
}
