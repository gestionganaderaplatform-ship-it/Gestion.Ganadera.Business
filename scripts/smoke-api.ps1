param(
    [string]$BaseUrl = "https://localhost:7181",
    [int]$TimeoutSeconds = 15
)

$healthUrl = "$BaseUrl/health"

try {
    $response = Invoke-WebRequest -Uri $healthUrl -TimeoutSec $TimeoutSeconds -UseBasicParsing

    if ($response.StatusCode -ne 200) {
        throw "La API respondió con estado $($response.StatusCode)."
    }

    Write-Host "Smoke test OK: $healthUrl respondió 200."
    exit 0
}
catch {
    Write-Error "Smoke test FAILED: $($_.Exception.Message)"
    exit 1
}
