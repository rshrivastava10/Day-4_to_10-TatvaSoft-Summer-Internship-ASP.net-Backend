using Data_Logic_Layer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Logic_Layer
{
    public class DALAdminUser
    {
        private readonly AppDbContext _context;

        public DALAdminUser(AppDbContext context)
        {
            _context = context;
        }

        public string AddUser(User user)
        {
            var result = "";
            try
            {
                var userEmailExists = _context.User.FirstOrDefault(x => !x.IsDeleted && x.EmailAddress == user.EmailAddress);
                if (userEmailExists == null)
                {
                    var newUser = new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        EmailAddress = user.EmailAddress,
                        Password = user.Password,
                        UserType = user.UserType,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                    };
                    _context.User.Add(newUser);
                    _context.SaveChanges();
                    var maxEmployeeId = 0;
                    var lastUserDetail = _context.UserDetail.ToList().LastOrDefault();

                    if (lastUserDetail != null)
                    {
                        maxEmployeeId = Convert.ToInt32(lastUserDetail.EmployeeId);
                    }
                    int newEmployeeId = maxEmployeeId + 1;
                    var newUserDetail = new UserDetail
                    {
                        UserId = newUser.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        EmailAddress = user.EmailAddress,
                        UserType = user.UserType,
                        Name = user.FirstName,
                        Surname = user.LastName,
                        EmployeeId = newEmployeeId.ToString(),
                        Department = "IT",
                        Status = true
                    };
                    _context.UserDetail.Add(newUserDetail);
                    _context.SaveChanges();

                    result = "User Add Suceessfully.";
                }
                else
                {
                    result = "Email is already exists.";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public List<UserDetail> GetUserList()
        {
            var userDetailList = from u in _context.User
                                 join ud in _context.UserDetail on u.Id equals ud.UserId into userDetailGroup
                                 from userDetail in userDetailGroup.DefaultIfEmpty()
                                 where !u.IsDeleted && u.UserType == "user" && !userDetail.IsDeleted
                                 select new UserDetail
                                 {
                                     Id = u.Id,
                                     FirstName = u.FirstName,
                                     LastName = u.LastName,
                                     PhoneNumber = u.PhoneNumber,
                                     EmployeeId = userDetail.EmployeeId,
                                     Department = userDetail.Department,
                                     Status = userDetail.Status,
                                 };
            return userDetailList.ToList();
        }
        public string UpdateUser(int userId, User updatedUser)
        {
            var result = "";
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingUser = _context.User.FirstOrDefault(x => !x.IsDeleted && x.Id == userId);
                    if (existingUser != null)
                    {
                        existingUser.FirstName = updatedUser.FirstName;
                        existingUser.LastName = updatedUser.LastName;
                        existingUser.PhoneNumber = updatedUser.PhoneNumber;
                        existingUser.EmailAddress = updatedUser.EmailAddress;
                        existingUser.Password = updatedUser.Password;
                        existingUser.UserType = updatedUser.UserType;
                        existingUser.CreatedDate = DateTime.UtcNow; // Should this be updated?

                        _context.User.Update(existingUser);
                        _context.SaveChanges();

                        var existingUserDetail = _context.UserDetail.FirstOrDefault(x => !x.IsDeleted && x.UserId == existingUser.Id);
                        if (existingUserDetail != null)
                        {
                            existingUserDetail.FirstName = updatedUser.FirstName;
                            existingUserDetail.LastName = updatedUser.LastName;
                            existingUserDetail.PhoneNumber = updatedUser.PhoneNumber;
                            existingUserDetail.EmailAddress = updatedUser.EmailAddress;
                            existingUserDetail.UserType = updatedUser.UserType;

                            _context.UserDetail.Update(existingUserDetail);
                            _context.SaveChanges();
                        }

                        transaction.Commit();
                        result = "User updated successfully.";
                    }
                    else
                    {
                        result = "User ID does not exist.";
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result = $"Error updating user: {ex.Message}";
                }
            }

            return result;
        }

        public async Task<string> DeleteUser(int userId)
        {
            try
            {
                var result = string.Empty;
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var userDetail = await _context.UserDetail.FirstOrDefaultAsync(x => x.UserId == userId);
                        if (userDetail != null)
                        {
                            userDetail.IsDeleted = true;
                        }
                        var user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId);
                        if (user != null)
                        {
                            user.IsDeleted = true;
                        }

                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync();

                        result = "Delete user successfully";
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw ex;
                    }

                }
                return result;
            }
            catch(Exception ex)
            {
                throw;
            }

        }
    }
}
