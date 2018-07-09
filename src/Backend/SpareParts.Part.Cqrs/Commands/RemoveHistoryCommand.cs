using System;
using SpareParts.Cqrs;

namespace SpareParts.Part.Cqrs.Commands
{
    public class RemovePartCommand : CommandBase
    {
        public RemovePartCommand(string code)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
        }
   
        public string Code { get; }
    }
}
