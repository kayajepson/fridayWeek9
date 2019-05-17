# _Hair Salon_

#### By _**Kaya Jepson**_

## Description

_A program that will show stylists at a salon and their corresponding client list._

| Behavior | Input | Output |
| ------------- |:-------------:| -----:|
| The program should allow employees(stylists) to input new stylists into the database | "kara" | kara |
| The program should allow employees(stylists) to input new clients into the database | "sara" | sara |
| The program should not allow clients to be inputted if there are no stylists in the database | -- | -- |
| The program should return information on each stylist or client when clicked on | kara | name, specialty |
| The program should allow you to delete both stylists and clients separately | -- | -- |
| The program should delete all clients of a deleted stylist | -- | -- |

## Setup/Installation Requirements

* _Clone project from github_
* _view code in text editor if necessary_
* _any text inside quotations ("") should be performed inside your terminal._
* _use mysql to manage database (google for operating system specifics on how to connect)_
* _"CREATE DATABASE kaya_jepson;"_
* _"USE kaya_jepson;"_
* _"CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255), specialty VARCHAR(255));"_
* _"CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylistId VARCHAR(255));"_
* _use "dotnet restore" and "dotnet build" and "dotnet run" in the production folder (HairSalon)_
* _View at localhost:5000_
* _use the UI to modify stylists and clients_

## Known Bugs

_None_

## Support and contact details

_No support offered_

## Technologies Used

_C#_

### License

Copyright <2019> <Kaya Jepson>
