# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$rootFolder = Join-Path $packFolder "../"

# List of solutions
$solutions = (
    "Sukt.Modules"
)

# List of projects
$projects = (
    "Sukt.Modules/src/Sukt.Module.Core",
    "Sukt.Modules/src/Sukt.Aop",
    "Sukt.Modules/src/Sukt.AspNetCore",
    "Sukt.Modules/src/Sukt.AutoMapper",
    "Sukt.Modules/src/Sukt.CodeGenerator",
    "Sukt.Modules/src/Sukt.EntityFrameworkCore",
    "Sukt.Modules/src/Sukt.MongoDB",
    "Sukt.Modules/src/Sukt.MultiTenancy",
    "Sukt.Modules/src/Sukt.Redis",
    "Sukt.Modules/src/Sukt.SeriLog",
    "Sukt.Modules/src/Sukt.Swagger",
    "Sukt.Modules/src/Sukt.TestBase",
    "Sukt.Modules/src/Sukt.WebScoket"
)