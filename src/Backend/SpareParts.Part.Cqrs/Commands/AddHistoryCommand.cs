using System;
using SpareParts.Cqrs;

namespace SpareParts.Part.Cqrs.Commands
{
    public class AddPartCommand : CommandBase
    {
        public AddPartCommand(string code, string name)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Name = name ?? throw new ArgumentNullException(nameof(name));          
        }

        public string Name { get; }
   
        public string Code { get; }
    }
}
