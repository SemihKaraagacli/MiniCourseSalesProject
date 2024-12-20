﻿using Microsoft.AspNetCore.Mvc;
using MiniCourseSalesProject.Service.User;
using MiniCourseSalesProject.Service.User.Dtos;

namespace MiniCourseSalesProject.Api.Controllers
{
    public class UserController(UserService userService) : CustomControllerBase
    {

        //user ekleme silme güncelleme görüntüleme

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpRequest signUpRequest)
        {
            var result = await userService.SignUp(signUpRequest);
            return CreateObjectResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await userService.Get();
            return CreateObjectResult(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await userService.Get(id);
            return CreateObjectResult(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await userService.Delete(Id);
            return CreateObjectResult(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateRequest userUpdateRequest)
        {
            var result = await userService.Update(userUpdateRequest);
            return CreateObjectResult(result);
        }

    }
}
