<h1 align="center">
   API using DDD and SOLID principles 
</h1>

</br>
  
<p align="center">
  <a href="#white_check_mark-Features">Features</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#globe_with_meridians-Technologies-and-Concepts-Implemented">Technology</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
   <a href="#gear-Architecture">Architecture</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
   <a href="#round_pushpin-endpoints">Endpoints</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#wrench-How-to-use">How to use</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#memo-Licence">Licence</a>
</p>


## :white_check_mark: Features

* WebAPI was built with .NET 8
* CRUD using ORM Entity Framework Core
* DDD principles
* TDD - Test APIs
* Identity - Control users
* JWT - Users Authentication
* AutoMapper


## :globe_with_meridians: Technologies and Concepts Implemented

This project was developed using:

- .NET 8
- Entity Framework Core 8.0.6
- Identity 8.0.6
- JWT 8.0.6
- AutoMapper 13.0.1
- Swagger 6.4.0

Concepts/Techniques used in:
- Clean Architecture
- ViewModel
- Repository Pattern
- Dependency Injection

## :gear: Architecture

```🌐
src
├── 📂 1- UI
|   ├── 📂 Controllers
|   ├── 📂 Models
|   ├── 📂 Token
├── 📂 2- Domain
|   ├── 📂 Interfaces
|       ├── 📂 Generics
|       ├── 📂 InterfacesServices
|   ├── 📂 Services
├── 📂 3- Infra
|   ├── 📂 Configuration
|   ├── 📂 Migration
|   ├── 📂 Repository
|       ├── 📂 Generics
|       ├── 📂 Repositories
├── 📂 4- Entities
|   ├── 📂 Entities
|   ├── 📂 Enum
├── 📂 5- Test

```

## :round_pushpin: Endpoints
![image](https://github.com/heberGustavo/api-DDD-net6/assets/44476616/6aff890e-751a-44a8-9163-5ba8e6e57fc6)


## :wrench: How to use

Clone that application using [Git](https://git-scm.com) and follow the next steps:

```bash
# 1. Clone this repository
$ git clone https://github.com/heberGustavo/api-DDD-net6.git

# 2. Open the project in Visual Studio

# 3. Execute the build

# 4. Change the Connection String. To modify follow this path:
  4.1 - UI > WebAPI > appsettings.json
  4.2 - Modify the value to "DefaultConnectionString"

# 5. Execute Migration
  5.1 - Open the "Package Manager Console"
  5.2 - Select the project "Infra" in "Default project"
  5.3 - Execute this command: Update-Database

# 6. Run the application

```


## :memo: Licence 
This project is under the MIT license. See the [LICENSE] for more information.


## Autor

| [<img src="https://avatars.githubusercontent.com/u/44476616?v=4" style="max-width: 100%;width: 90px;"><br><sub>Heber Gustavo</sub>](https://github.com/heberGustavo) |
| :---: |
|[Linkedin](https://www.linkedin.com/in/heber-gustavo/)|
