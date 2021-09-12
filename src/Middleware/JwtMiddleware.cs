﻿//-----------------------------------------------------------------------
// <copyright file="JwtMiddleware.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>2021/9/1 15:32:25</date>
//-----------------------------------------------------------------------

using LiMeowApi.Repository;
using LiMeowApi.Schema.App;
using LiMeowApi.Schema.User;
using LiMeowApi.Utility;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using LiMeowApi.Entity.User;
using LiMeowApi.Service.Contract;

namespace LiMeowApi.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await AttachUserToContext(context, token);
            }

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, string token)
        {
            if (JwtUtil.VerifyToken(token))
            {
                var userId = JwtUtil.SerializeJwt(token);
                var user = await new Repository<UserEntity>().Get<SimpleUserModel>(x => x.Id == userId);
                context.Items[AppConstant.UserContext] = user;
            }
        }
    }
}