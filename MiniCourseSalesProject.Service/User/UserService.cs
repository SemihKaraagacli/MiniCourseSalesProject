using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Service.OrderService.Dtos;
using MiniCourseSalesProject.Service.User.Dtos;
using System.Net;

namespace MiniCourseSalesProject.Service.User
{
    public class UserService(UserManager<AppUser> userManager)
    {
        public async Task<ServiceResult<Guid>> SignUp(SignUpRequest signUpRequest)
        {
            var newUser = new AppUser
            {
                UserName = signUpRequest.UserName,
                Email = signUpRequest.Email,
                Wallet = signUpRequest.Wallet,
                Orders = new List<Order>()
            };
            var result = await userManager.CreateAsync(newUser, signUpRequest.Password);
            if (!result.Succeeded)
            {
                var errorList = result.Errors.Select(x => x.Description).ToList();
                return ServiceResult<Guid>.Fail(errorList, HttpStatusCode.BadRequest);
            }
            return ServiceResult<Guid>.Success(newUser.Id, HttpStatusCode.OK);
        }
        public async Task<ServiceResult> Update(UserUpdateRequest userUpdateRequest)
        {
            var hasUser = await userManager.FindByIdAsync(userUpdateRequest.Id.ToString());
            if (hasUser is null)
            {
                return ServiceResult.Fail("User not found.", HttpStatusCode.NotFound);
            }
            hasUser.Id = userUpdateRequest.Id;
            hasUser.Email = userUpdateRequest.Email;
            hasUser.UserName = userUpdateRequest.UserName;
            hasUser.Wallet = userUpdateRequest.Wallet;
            var result = await userManager.UpdateAsync(hasUser);
            if (!result.Succeeded)
            {
                var errorList = result.Errors.Select(x => x.Description).ToList();
                return ServiceResult<Guid>.Fail(errorList, HttpStatusCode.BadRequest);
            }
            return ServiceResult.Success(HttpStatusCode.OK);
        }
        public async Task<ServiceResult> Delete(Guid Id)
        {
            var hasUser = await userManager.FindByIdAsync(Id.ToString());
            if (hasUser is null)
            {
                return ServiceResult.Fail("User not found.", HttpStatusCode.NotFound);
            }
            var result = await userManager.DeleteAsync(hasUser);
            if (!result.Succeeded)
            {
                var errorList = result.Errors.Select(x => x.Description).ToList();
                return ServiceResult<Guid>.Fail(errorList, HttpStatusCode.BadRequest);
            }
            return ServiceResult.Success(HttpStatusCode.OK);
        }
        public async Task<ServiceResult<List<UserDto>>> Get()
        {
            var allUser = await userManager.Users.Select(x => new UserDto
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                Wallet = x.Wallet,  // Wallet'ı burada alıyoruz
                Orders = x.Orders.Select(o => new OrderDto
                {
                    Id = o.Id  // Sadece ID'yi alıyoruz
                }).ToList()
            }).ToListAsync();
            return ServiceResult<List<UserDto>>.Success(allUser, HttpStatusCode.OK);
        }
        public async Task<ServiceResult<UserDto>> Get(Guid Id)
        {
            var hasUser = await userManager.FindByIdAsync(Id.ToString());
            if (hasUser is null)
            {
                return ServiceResult<UserDto>.Fail("User not found.", HttpStatusCode.NotFound);
            }
            var userDto = new UserDto
            {
                Id = hasUser.Id,
                UserName = hasUser.UserName,
                Email = hasUser.Email,
                Wallet = hasUser.Wallet,  // Wallet'ı burada alıyoruz
                Orders = hasUser.Orders.Select(o => new OrderDto
                {
                    Id = o.Id  // Sadece ID'yi alıyoruz
                }).ToList()
            };
            return ServiceResult<UserDto>.Success(userDto, HttpStatusCode.OK);
        }
        public async Task<ServiceResult<UserDto>> GetUserWithOrdersAsync(Guid userId)
        {
            var user = await userManager.Users.Include(u => u.Orders).FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
            {
                return ServiceResult<UserDto>.Fail("User not found.", HttpStatusCode.NotFound);
            }
            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Wallet = user.Wallet,  // Wallet'ı burada alıyoruz
                Orders = user.Orders.Select(o => new OrderDto
                {
                    Id = o.Id  // Sadece ID'yi alıyoruz
                }).ToList()
            };
            return ServiceResult<UserDto>.Success(userDto, HttpStatusCode.OK);
        }
    }
}
