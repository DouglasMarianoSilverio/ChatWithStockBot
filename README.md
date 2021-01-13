# ChatWithStockBot

This Chat is under construction and will allow the users to get stock information (only for US).

You need to run Docker using this line command line  to use RabbitMq service.

    docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-managementdocker 

you need also to create the local database using entityFramework under CWSB.Services.Api project to create the local DB.

    Update-Database 

Ensure that you have configured **Multiple** startups projects to Run the API, MVC and also StockBot project, but this last one is not working yet.

I hope you enjoy it!

**Working**: 
* Authentication API using Identity.
* API to receive messages from clients.
* Service to send a message using RabbitMQ.
* Stock Bot is listening the queue
* Functionality for Bot to get stock information.
* Chat client. 

**To-do (Backend)**
* Improve the use of RabbitMQ with Servicebus.
* Transform the bot into a distributed Service.


