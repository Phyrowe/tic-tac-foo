using TicTacFoo.Application.Common.Attributes;
using TicTacFoo.Application.Common.Interfaces;

namespace TicTacFoo.Application.Services
{
    [Singleton]
    public class SampleService : ISampleService
    {
        public bool IsWorking()
        {
            return true;
        }
    }
}