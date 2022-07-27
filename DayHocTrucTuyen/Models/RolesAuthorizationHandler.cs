using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace DayHocTrucTuyen.Models
{
    public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
    {
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
        {

            //Nếu không có đăng nhập thì truyền url xử lý đăng nhập
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            //Nếu không có vai trò nào được đưa ra thì thì mặc định người dùng không có quyền truy cập
            var validRole = false;
            if (requirement.AllowedRoles == null || requirement.AllowedRoles.Any() == false)
            {
                validRole = true;
            }
            else
            {
                //var userCode = context.User.Claims.First().Value;
                var userCode = context.User.Claims.FirstOrDefault(x => x.Type == "MaNd").Value;
                var roles = requirement.AllowedRoles;

                validRole = (db.NguoiDungs.FirstOrDefault(x => x.MaNd == userCode && roles.Contains(x.MaLoai)) != null);
            }

            if (validRole)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
