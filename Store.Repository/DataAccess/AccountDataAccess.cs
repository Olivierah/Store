﻿using Store.Repository.Context;
using SecureIdentity.Password;
using Store.Domain.Dtos;
using Store.Domain.Entities;

namespace Store.Repository.DataAccess
{
    public class AccountDataAccess
    {
        public async static void CreateNewAccount(UserDto userDto)
        {
            using (var context = new StoreDataContext())
            {
                var user = new User
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    PasswordHash = PasswordHasher.Hash(userDto.Password),
                    Cpf = userDto.Cpf,
                    Street = userDto.Street,
                    Neighborhood = userDto.Neighborhood,
                    ZipCode = userDto.ZipCode,
                    StreetNumber = userDto.StreetNumber,
                    BirthDate = userDto.BirthDate,
                    Complement = userDto.Complement,
                    Gender = userDto.Gender,
                };
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
