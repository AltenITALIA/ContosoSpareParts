using System.Security.Claims;
using System.Threading;

namespace SpareParts.Common
{
    public interface IUserAccessor
    {
        ClaimsPrincipal User { get; set; }
    }

    internal class UserAccessor : IUserAccessor
    {
        private static readonly AsyncLocal<ClaimsPrincipal> UserLocal = new AsyncLocal<ClaimsPrincipal>();

        public ClaimsPrincipal User
        {
            get => UserLocal.Value;
            set => UserLocal.Value = value;
        }
    }
}
