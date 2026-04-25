# ============================================================
# Script para configurar secretos de DESARROLLO local
# ============================================================
# Este script NO se usa en produccion.
# Solo configura tu equipo local para poder ejecutar la API.
#
# PASOS:
# 1. Abre PowerShell en la carpeta scripts/
# 2. Ejecuta: .\setup-secrets.ps1
# 3. Sigue las instrucciones en pantalla
# ============================================================

$ErrorActionPreference = "Stop"

$projectPath = Join-Path $PSScriptRoot ".." "Gestion.Ganadera.Business.API"

if (-not (Test-Path $projectPath)) {
    Write-Host ""
    Write-Host "[ERROR] No se encontro el proyecto API" -ForegroundColor Red
    Write-Host "Ejecuta este script desde la carpeta scripts/ del proyecto" -ForegroundColor Yellow
    exit 1
}

Set-Location $projectPath

Write-Host ""
Write-Host "==================================================" -ForegroundColor Cyan
Write-Host "  CONFIGURACION DE SECRETOS - DESARROLLO LOCAL" -ForegroundColor Cyan
Write-Host "==================================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Este script configurara tu equipo local para que la API" -ForegroundColor White
Write-Host "pueda conectarse a SQL Server y validar JWT externo si decides habilitarlo." -ForegroundColor White
Write-Host ""
Write-Host "Los secretos se guardan SOLO en tu computadora (User Secrets)." -ForegroundColor Gray
Write-Host "NO se suben a Git. Son valores de DESARROLLO, no produccion." -ForegroundColor Gray
Write-Host ""

# Inicializar User Secrets si no existe
$userSecretsIdFile = Join-Path $projectPath "UserSecretsIdFile"
if (-not (Test-Path $userSecretsIdFile)) {
    dotnet user-secrets init | Out-Null
    Write-Host "[OK] User Secrets inicializado" -ForegroundColor Green
}

# ============================================================
# 1. CADENA DE CONEXION A SQL SERVER
# ============================================================
Write-Host ""
Write-Host "-----------------------------------------------------------" -ForegroundColor Yellow
Write-Host "1. CONEXION A SQL SERVER" -ForegroundColor Yellow
Write-Host "-----------------------------------------------------------" -ForegroundColor Yellow
Write-Host ""
Write-Host "Como te conectas a SQL Server?" -ForegroundColor White
Write-Host "  [1] Autenticacion Windows (mi usuario de Windows)" -ForegroundColor Gray
Write-Host "  [2] Autenticacion SQL (usuario y contrasena de SQL Server)" -ForegroundColor Gray
Write-Host ""

$opcion = Read-Host "Selecciona una opcion (1 o 2)"

if ($opcion -eq "1") {
    $dbConnection = "Server=localhost;Database=AppDb;Trusted_Connection=True;TrustServerCertificate=True"
} else {
    Write-Host ""
    Write-Host "Ingresa los datos de tu SQL Server:" -ForegroundColor White
    $server = Read-Host "  Servidor (ej: localhost o 192.168.1.10)"
    $database = Read-Host "  Base de datos (ej: AppDb)"
    $username = Read-Host "  Usuario SQL"
    $password = Read-Host "  Contrasena SQL"
    $dbConnection = "Server=$server;Database=$database;User=$username;Password=$password;TrustServerCertificate=True"
}

Write-Host ""
Write-Host "Cadena de conexion a guardar:" -ForegroundColor Gray
Write-Host "  $dbConnection" -ForegroundColor DarkGray
Write-Host ""
$confirmar = Read-Host "Guardar esta conexion? (S/n)"
if ($confirmar -ne "n" -and $confirmar -ne "N") {
    dotnet user-secrets set "ConnectionStrings:DefaultConnection" $dbConnection | Out-Null
    Write-Host "[OK] Conexion guardada" -ForegroundColor Green
}

# ============================================================
# 2. CLAVE JWT
# ============================================================
Write-Host ""
Write-Host "-----------------------------------------------------------" -ForegroundColor Yellow
Write-Host "2. CLAVE JWT (para validacion JWT externa)" -ForegroundColor Yellow
Write-Host "-----------------------------------------------------------" -ForegroundColor Yellow
Write-Host ""
Write-Host "La clave JWT se usa para validar tokens externos cuando" -ForegroundColor White
Write-Host "trabajas con secreto compartido. Para desarrollo puedes usar" -ForegroundColor White
Write-Host "cualquier texto de 32 o mas caracteres." -ForegroundColor White
Write-Host ""
Write-Host "Ejemplo de clave segura para desarrollo:" -ForegroundColor Gray
Write-Host "  MiClaveSecretaParaDesarrollo2024!" -ForegroundColor DarkGray
Write-Host ""

$jwtSigningKey = Read-Host "Ingresa tu SigningKey JWT (minimo 32 caracteres, Enter para usar una de prueba)"
if ([string]::IsNullOrWhiteSpace($jwtSigningKey)) {
    $jwtSigningKey = "CLAVE_JWT_DESARROLLO_TEMPORAL_32CHARS"
}

if ($jwtSigningKey.Length -lt 32) {
    Write-Host "[WARNING] Tu clave tiene menos de 32 caracteres. Funcionara, pero no es lo ideal." -ForegroundColor Yellow
}

dotnet user-secrets set "Jwt:SigningKey" $jwtSigningKey | Out-Null
Write-Host "[OK] Clave JWT guardada" -ForegroundColor Green

# ============================================================
# 3. LICENCIA AUTOMAPPER (OPCIONAL)
# ============================================================
Write-Host ""
Write-Host "-----------------------------------------------------------" -ForegroundColor Yellow
Write-Host "3. LICENCIA AUTOMAPPER (OPCIONAL)" -ForegroundColor Yellow
Write-Host "-----------------------------------------------------------" -ForegroundColor Yellow
Write-Host ""
Write-Host "Solo configura esta clave si ya tienes una licencia real." -ForegroundColor White
Write-Host "Si la dejas vacia, el template no guardara ningun valor de licencia." -ForegroundColor White
Write-Host ""
$autoMapperKey = Read-Host "Licencia AutoMapper real (Enter para omitir)"
if ([string]::IsNullOrWhiteSpace($autoMapperKey)) {
    dotnet user-secrets remove "AutoMapper:LicenseKey" | Out-Null
    Write-Host "[OK] Licencia AutoMapper omitida" -ForegroundColor Green
} else {
    dotnet user-secrets set "AutoMapper:LicenseKey" $autoMapperKey | Out-Null
    Write-Host "[OK] Licencia AutoMapper guardada" -ForegroundColor Green
}

# ============================================================
# RESUMEN
# ============================================================
Write-Host ""
Write-Host "==================================================" -ForegroundColor Green
Write-Host "  CONFIGURACION COMPLETA" -ForegroundColor Green
Write-Host "==================================================" -ForegroundColor Green
Write-Host ""
Write-Host "Secrets guardados en tu equipo:" -ForegroundColor White
Write-Host ""
dotnet user-secrets list
Write-Host ""
Write-Host "==================================================" -ForegroundColor Cyan
Write-Host "  PROXIMOS PASOS" -ForegroundColor Cyan
Write-Host "==================================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "1. Ejecuta las migraciones de base de datos:" -ForegroundColor White
Write-Host "   dotnet ef database update ^" -ForegroundColor Gray
Write-Host "     --project .\Gestion.Ganadera.Business.Infrastructure ^" -ForegroundColor Gray
Write-Host "     --startup-project .\Gestion.Ganadera.Business.API" -ForegroundColor Gray
Write-Host ""
Write-Host "2. Ejecuta la API:" -ForegroundColor White
Write-Host "   dotnet run --project .\Gestion.Ganadera.Business.API" -ForegroundColor Gray
Write-Host ""
Write-Host "3. Abre http://localhost:5000/swagger para ver la API" -ForegroundColor White
Write-Host ""
Write-Host "==================================================" -ForegroundColor Green
