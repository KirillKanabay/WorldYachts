using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WorldYachts.Data.Authenticate;
using WorldYachts.Data.Entities;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.DependencyInjections.Services;

namespace WorldYachts.Services.Users
{
    public class UserService:IUserService
    {
        private readonly IWebClientService _webClient;
        private readonly AuthUser _authUser;
        private readonly IHashCalculator _hashCalculator;

        private const string Path = "users";

        public UserService(IWebClientService webClient, AuthUser authUser, IHashCalculator hashCalculator)
        {
            _webClient = webClient;
            _authUser = authUser;
            _hashCalculator = hashCalculator;
        }
        
        public async Task AuthenticateAsync(AuthenticateRequest request)
        {
            request.Password = _hashCalculator.GetHash(request.Password);

            var response = await _webClient
                .PostAsync<AuthenticateRequest, AuthenticateResponse>("users/authenticate", request);

            if (response != null)
            {
                _webClient.Token = response.Data.Token;
                await _authUser.Authenticate(response.Data);
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var response = await _webClient.GetAsync<User>(Path, id);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException("Пользователь не найден");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var response = await _webClient.GetAsync<IEnumerable<User>>(Path);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<User> AddAsync(User user)
        {
            var response = await _webClient.PostAsync<User, User>(Path, user);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception($"Проверьте правильность заполненных данных!");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<User> UpdateAsync(int id, User user)
        {
            var response = await _webClient.PutAsync<User, User>(Path, id, user);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception($"Проверьте правильность заполненных данных!");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }

        public async Task<User> DeleteAsync(int id)
        {
            var response = await _webClient.DeleteAsync<User>(Path, id);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Ошибка выполнения запроса. Код ошибки: {response.StatusCode}");
            }

            return response.Data;
        }
    }
}
