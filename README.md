# This project runs Docker

To build docker :
docker build -t itkwiatek/platformservice .

To run (create new container) docker :
docker run -p 8080:80 -d itkwiatek/platformservice

To push changes : 
docker push itkwiatek/platformservice

To get docker containers running
docker ps

To start docker:
docker start e996d1c46162

To stop docker:
docker stop e996d1c46162

# PlatformSerive 

To run : 
dotnet run

To stop : 
ctrl + c
