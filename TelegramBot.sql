create database TelegramBotInterfaceForDispatcher
go

use TelegramBotInterfaceForDispatcher

create table User_Role
(
id_Role int primary key identity,
Role_Name varchar(20)
)
go

insert into User_Role(Role_Name)
Values
('Клиент'),
('Диспетчер'),
('Менеджер')
go

create table Users
(
id_User int primary key identity,
User_Chat_id BIGINT,
UserName varchar(20),
UserRole int foreign key references User_Role(id_Role),
UserPassword int,
UserNumberPhone varchar(15),
UserEmail varchar(50)
)
go


Create table ChangeOfDuty
(
id_Change int primary key identity,
Actions varchar(100),
id_User int foreign key references Users(id_User),
Data_Time DateTime
)
go

create table AllEvent
(
id_Event int primary key identity,
id_User int foreign key references Users(id_User),
Data_Event datetime,
Text_Event varchar(1000)
)
go

create table Note
(
id_Note int primary key identity,
id_User int foreign key references Users(id_User),
Date_Time datetime,
Text_Note varchar(1000)
)
go

create table Request_Status
(
id_Status int primary key identity,
name_Status varchar(20)
)
go

insert into Request_Status(name_Status)
values ('Ожидает'),('Выполняется'),('Выполнена')

create table Request_Client
(
id_Request int primary key identity,
id_User int foreign key references Users(id_User),
date_Request datetime,
text_Request varchar(1000),
text_Working varchar(1000),
id_Status int foreign key references Request_Status(id_Status),
id_executer int foreign key references Users(id_User)
)
go



create table Report
(
id_Report int primary key identity,
id_User_Creator int foreign key references Users(id_User),
Data_of_create datetime,
Link_in_textFile varchar(20)
)
go


insert into AllEvent(id_User,Data_Event,Text_Event)
values (@User_id, @DateTime, @Text)

insert into ChangeOfDuty(Actions,id_User)
values (@Text,@User_id)

insert into Users(User_Chat_id,UserName)
values (@User_Chat_idPROC,@UserNameProc)


select id_Request, UserName , date_Request, text_Request,name_Status
from Request_Client
inner join Users on Request_Client.id_User = Users.id_User 
inner join Request_Status on Request_Client.id_Status = Request_Status.id_Status
WHERE Request_Client.id_Status = 1

SELECT id_Request, UserName , date_Request, text_Request, name_Status 
FROM Request_Client 
inner join Users on Request_Client.id_User = Users.id_User 
inner join Request_Status on Request_Client.id_Status = Request_Status.id_Status 
WHERE Request_Client.id_User = {Name} and Request_Client.id_Status IN(1,2)





