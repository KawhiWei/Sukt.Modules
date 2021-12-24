# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$rootFolder = Join-Path $packFolder "../"

# List of solutions
$solutions = (
    "Sukt.Modules"
)

# List of projects
$projects = (
    "src/Sukt.Module.Core",
    "src/Sukt.Aop",
    "src/Sukt.AutoMapper",
    "src/Sukt.CodeGenerator",
    "src/Sukt.EntityFrameworkCore",
    "src/Sukt.MongoDB",
    "src/Sukt.MultiTenancy",
    "src/Sukt.Redis",
    "src/Sukt.SeriLog",
    "src/Sukt.Swagger",
    "src/Sukt.TestBase",
    "src/Sukt.WebSocketServer",
    "src/Sukt.MQTransaction",
    "src/Sukt.MQTransaction.RabbitMQ"
)