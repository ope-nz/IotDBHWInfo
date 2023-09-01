

@echo off
for /f %%A in ('IotDBHWInfo.exe -cores') do set "system_cpu_cores=%%A"
echo %system_cpu_cores%

for /f %%A in ('IotDBHWInfo.exe -memory') do set "system_memory_in_mb=%%A"
echo %system_memory_in_mb%

pause