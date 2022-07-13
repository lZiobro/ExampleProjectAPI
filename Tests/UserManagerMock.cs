using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public static class UserManagerMock
    {
        public static List<ApplicationUser> SampleUserList = new List<ApplicationUser>
            {
                new ApplicationUser() {UserName="Bowashe", Id="a"},
                new ApplicationUser() {UserName="Bowashe2", Id="b"}
            };
        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }

        public static void SetupIdentity(this ControllerBase controller, string identityName = "a")
        {
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(x => x.User.Identity.Name).Returns(identityName);
            var controllerContext = new ControllerContext() { HttpContext = (httpContextMock.Object) };

            controller.ControllerContext = controllerContext;
        }
    }
}
