//-----------------------------------------------------------------------
// <copyright file="UserService.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>2021/9/1 16:09:43</date>
//-----------------------------------------------------------------------
using FastHttpApi.Schema.User;
using System.Threading.Tasks;

namespace FastHttpApi.Service.Contract
{
    public interface IUserService
    {
        Task<UserModel> GetUserById(string id);

        Task<UserModel> AddUser(UserModel user);

        Task<UserModel> GetUserByUserName(string userName);

        Task<bool> ModifyPassword(string userId, string oldPassword, string newPassword);
    }
}