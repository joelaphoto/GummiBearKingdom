# Gummi Bear Kingdom Product Site

Website for Gummi Bear Kingdom

#### By **Joel Adams**

## Description

 This website allows a user to add Product entries to a database for a fictional company called Gummi Bear Kingdom. Once an entry has been created the user may edit the entry or delete it from the database. Users may also add reviews with a rating out of 5 to a product. The top three rated products display on the landing page.

*GitHub repo:* https://github.com/joelaphoto/GummiBearKingdom

## Setup/Installation Requirements
Requires .NET Framework SDK, MAMP

1. Download or clone Github respository.
2. Open MAMP and configure MySql port to 8889.
3. Run command "dotnet restore" on folder containing .csproj file.
2. Run command "dotnet ef database update" in the main and test project folders to create database and test database structure.
3. Run command "dotnet run" and navigate to http://localhost:XXXXX as indicated by console.
4. If you would like to use dummy database information open PhpMyAdmin through MAMP, drop the gummibear database, and import database from gummibear.sql in top level directory of project.

## Known Bugs
* No known bugs at this time.

## Technologies Used
* .NET Framework
* Entity Framework
* Microsoft Visual Studio 2017
* Atom
* MAMP

## Support and contact details

_Please contact  the creator through Github.com: joelaphoto_

### License

*{This software is licensed under the MIT license}*

Copyright (c) 2018 **Joel Adams**
