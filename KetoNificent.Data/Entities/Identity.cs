using Microsoft.AspNetCore.Identity;

namespace KetoNificent.Data.Entities;

public class RoleEntity : IdentityRole<int> { }
public class UserRoleEntity : IdentityUserRole<int> { }
public class UserClaimEntity : IdentityUserClaim<int> { }
public class UserLoginEntity : IdentityUserLogin<int> { }
public class UserTokenEntity : IdentityUserToken<int> { }
public class RoleClaimEntity : IdentityRoleClaim<int> { }