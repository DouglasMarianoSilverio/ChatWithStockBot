# ChatWithStockBot

This Chat is under construction and will allow the users to get stock information (only for US).

You need to run Docker using this line command line  to use RabbitMq service.

docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-managementdocker 

you need also to create the local database using entityFramework.
Update-Database under CWSB.Services.Api project to create the local DB.

Ensure that you have configured Multiple startups projects to Run the API, MVC and also StockBot project, but this last one is not working yet.
