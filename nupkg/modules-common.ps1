# Paths 获取完整的相对路径
$packFolder = (Get-Item -Path "./" -Verbose).FullName
# 
$rootFolder = Join-Path $packFolder "../"

# List of solutions
$solutions = (
    "modules/identity",
    "modules/permissionmanagement"
)

# List of projects
$projects = (
     # modules/identity
    "modules/identity/src/Sukt.Identity.Api",
    "modules/identity/src/Sukt.Identity.Application",
    "modules/identity/src/Sukt.Identity.Domain",
    "modules/identity/src/Sukt.Identity.Domain.Shared",
    "modules/identity/src/Sukt.Identity.Dto",
    "modules/identity/src/Sukt.Identity.EntityFrameworkCore",
    "modules/identity/src/Sukt.Identity.Query",

     # modules/permissionmanagement
    "modules/permissionmanagement/src/Sukt.PermissionManagement.Api",
    "modules/permissionmanagement/src/Sukt.PermissionManagement.Application",
    "modules/permissionmanagement/src/Sukt.PermissionManagement.Domain",
    "modules/permissionmanagement/src/Sukt.PermissionManagement.Domain.Shared",
    "modules/permissionmanagement/src/Sukt.PermissionManagement.Dto",
    "modules/permissionmanagement/src/Sukt.PermissionManagement.EntityFrameworkCore",
    "modules/permissionmanagement/src/Sukt.PermissionManagement.Query"

)