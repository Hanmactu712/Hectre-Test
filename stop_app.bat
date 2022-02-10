echo on
Pushd "%~dp0"
docker-compose down --rmi all
pause