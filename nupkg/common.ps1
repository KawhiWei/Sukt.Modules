# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$rootFolder = Join-Path $packFolder "../"

# List of solutions
$solutions = (
    "Sukt.Modules"
)

# List of projects
$projects = (
    "Framework/src/Sukt.Module.Core",
    "Framework/src/Sukt.AspNetCore",
    "Framework/src/Sukt.Aop",
    "Framework/src/Sukt.CodeGenerator",
    "Framework/src/Sukt.EntityFrameworkCore",
    "Framework/src/Sukt.MongoDB",
    "Framework/src/Sukt.Redis",
    "Framework/src/Sukt.SeriLog",
    "Framework/src/Sukt.Swagger",
    "Framework/src/Sukt.TestBase",
    "Framework/src/Sukt.WebSocketServer",
    "Framework/src/Sukt.MQTransaction",
    "Framework/src/Sukt.MQTransaction.RabbitMQ"
)