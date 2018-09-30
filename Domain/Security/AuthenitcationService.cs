namespace Domain.Security
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class AuthService : IAuthService
    {
        private readonly HtmlToWordDbContext _context;
        private readonly IHashProvider _hashingProvider;

        public AuthService(HtmlToWordDbContext context, IHashProvider hashingProvider)
        {
            _context = context;
            _hashingProvider = hashingProvider;
        }

        public async Task<AuthenicationResult> AuthenticateAsync(string userName, string plainPassword)
        {
            var user = await _context.Users
                                     .SingleOrDefaultAsync(x => x.NormalisedUserName == userName);

            if (user == null)
                return new AuthenicationResult();

            var salt = user.Salt.ToByteArray();

            var password = _hashingProvider.GeneratePBKDF2Hash(plainPassword, salt).ToHexString();

            if (user.Password != password)
                return new AuthenicationResult();

            return new AuthenicationResult
            {
                State = AuthenicationResultState.Pass,
                User = user
            };
        }


        public enum AuthenicationResultState
        {
            Pass,
            Fail
        }

        public class AuthenicationResult
        {
            public User User { get; set; }

            public AuthenicationResultState State { get; set; } = AuthenicationResultState.Fail;
        }
    }
}
