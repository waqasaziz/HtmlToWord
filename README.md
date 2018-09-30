# HtmlToWord
DotNet Core web application which converts a given URL's html to dictionary dictionary that contains the frequency of use of each word on that page.


##### Development Enviroment
 - DotNet Core 2.1
 - MySQL 5.7
 - Visual Studio 2017


##### Third Party Server Side Depenedencies
 - Pomelo.EntityFrameworkCore.MySql - v2.1.2
 - HtmlAgilityPack - 1.8.8
 - Moq - v2.1.2


##### Third Party Client Side Depenedencies
 - bootstrap: 3.3.7,
 - jqcloud2: 2.0.3,
 - jquery: 3.3.1,
 - jquery-validation: 1.17.0,
 - jquery-validation-unobtrusive: 3.2.10,
 - requirejs: 2.3.5
##### Third Party Client Side Dev Depenedencies
 - gulp: 3.9.1,
 - gulp-concat: 2.6.1,
 - gulp-cssmin: 0.1.7,
 - gulp-htmlclean: 2.7.22,
 - gulp-less: 4.0.1,
 - gulp-uglify: 2.1.2,
 - merge-stream: 1.0.1,
 - rimraf: 2.6.1

##### Setting up Solution in Development Enviroment
- Install MySQl 5.7 
- run npm install to install all packages
- Open solution, restore nuget pakcges and build entire solution
- Update connectionstring in app setting to correct MySQL connection details

##### Todo
- Encription Keys are stored in appsettings. I wanted to use Azure Key Walt but couldn't get enough time to apply. It uses slot/production app settings at the moment to override keys for each enviroment.
- Unit tests for controllers are not done due to shoratge of time.
