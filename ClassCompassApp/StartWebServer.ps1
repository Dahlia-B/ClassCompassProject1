$listener = New-Object System.Net.HttpListener
$listener.Prefixes.Add('http://localhost:5003/')
$listener.Prefixes.Add('http://+:5003/')
$listener.Start()

Write-Host '🌐 ClassCompass Frontend Server Running!' -ForegroundColor Green
Write-Host '==========================================' -ForegroundColor Green
Write-Host 'Local Access: http://localhost:5003' -ForegroundColor Cyan
Write-Host 'Network Access: http://192.168.68.83:5003' -ForegroundColor Cyan
Write-Host 'Press Ctrl+C to stop the server' -ForegroundColor Yellow
Write-Host '==========================================' -ForegroundColor Green

try {
    while ($listener.IsListening) {
        $context = $listener.GetContext()
        $request = $context.Request
        $response = $context.Response
        
        $localPath = $request.Url.LocalPath
        if ($localPath -eq '/') { $localPath = '/index.html' }
        
        $filePath = Join-Path 'wwwroot' $localPath.TrimStart('/')
        
        if (Test-Path $filePath) {
            $content = Get-Content $filePath -Raw -Encoding UTF8
            $buffer = [System.Text.Encoding]::UTF8.GetBytes($content)
            $response.ContentLength64 = $buffer.Length
            
            if ($filePath.EndsWith('.html')) {
                $response.ContentType = 'text/html; charset=utf-8'
            } elseif ($filePath.EndsWith('.css')) {
                $response.ContentType = 'text/css'
            } elseif ($filePath.EndsWith('.js')) {
                $response.ContentType = 'application/javascript'
            }
            
            $response.OutputStream.Write($buffer, 0, $buffer.Length)
            Write-Host "$(Get-Date -Format 'HH:mm:ss') - Served: $localPath" -ForegroundColor Gray
        } else {
            $response.StatusCode = 404
            $notFound = '<h1>404 - File Not Found</h1>'
            $buffer = [System.Text.Encoding]::UTF8.GetBytes($notFound)
            $response.ContentLength64 = $buffer.Length
            $response.OutputStream.Write($buffer, 0, $buffer.Length)
        }
        
        $response.OutputStream.Close()
    }
} catch {
    Write-Host 'Server stopped or error occurred' -ForegroundColor Yellow
} finally {
    $listener.Stop()
    Write-Host 'ClassCompass frontend server stopped.' -ForegroundColor Yellow
}
