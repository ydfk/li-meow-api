//-----------------------------------------------------------------------
// <copyright file="UserService.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>2021/9/1 16:09:43</date>
//-----------------------------------------------------------------------
using FastHttpApi.Entity.User;
using FastHttpApi.Repository;
using FastHttpApi.Schema.User;
using FastHttpApi.Service.Contract;
using FastHttpApi.Utility;
using System.Threading.Tasks;

namespace FastHttpApi.Service.Implement
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _userRepository;

        public UserService(IRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> GetUserById(string id)
        {
            return await _userRepository.Get<UserModel>(x => x.Id == id);
        }

        public async Task<UserModel> GetUserByUserName(string userName)
        {
            return await _userRepository.Get<UserModel>(x => x.UserName == userName);
        }

        public async Task<UserModel> AddUser(UserModel user)
        {
            if (await _userRepository.Count(x => x.UserName == user.UserName) == 0)
            {
                user.Password = SecurityUtil.Md5Password(user.UserName, user.Password);
                return await _userRepository.Save(user);
            }

            return null;
        }

        public async Task<bool> ModifyPassword(string userId, string oldPassword, string newPassword)
        {
            if (!newPassword.Equals(oldPassword))
            {
                var user = await _userRepository.Get<UserModel>(x => x.Id == userId);
                if (user != null)
                {
                    if (SecurityUtil.Md5Password(user.UserName, oldPassword) == user.Password)
                    {
                        user.Password = SecurityUtil.Md5Password(user.UserName, newPassword);
                        await _userRepository.Update(user);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}