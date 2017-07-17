Create Database ProIntBD
go
use ProIntBD
go
/*--------------------------TABLAS-------------------------------------------*/
Create table Persona(
CI int not null primary key,
nombre varchar(50) not null,
apellidoPaterno varchar(50) not null,
apellidoMaterno varchar(50) not null,
telefono int not null,
direccion varchar(200) not null
)
go
Create table PersonalMinisterio(
nroRegistroMins int not null identity primary key,
loginMinistro varchar(30) not null,
passMinistro varchar(100) not null,
correo varchar(80) unique not null,
firmaDigital varchar(500) not null unique,
CI int not null foreign key references Persona (CI)
)
go

Create table Colegio(
idColegio int not null identity primary key,
nombre varchar(50) not null,
direccion varchar(50) not null,
correo varchar(80) unique not null,
nroRegistroMins int not null foreign key references PersonalMinisterio (nroRegistroMins)
)
go
Create table PersonalColegio(
nroRegistroPColegio int not null identity primary key,
loginPColegio varchar(30) not null,
passPColegio varchar(100) not null,
correo varchar(80) unique not null,
CI int not null foreign key references Persona (CI),
idColegio int not null foreign key references Colegio(idColegio)
)
go
Create table Rol(
idRol int not null identity primary key,
nombre varchar(30) not null,
descripcion varchar(300) not null
)
go
Create table Privilegios(
idPrivilegios int not null identity primary key,
nombre varchar(50) not null,
descripcion varchar(300) not null,
idRol int not null foreign key references Rol (idRol)
)
go
Create table Gestion(
idGestion int not null identity primary key,
nombrePromo varchar(100) not null,
año int not null,
idColegio int not null foreign key references Colegio(idColegio)
)
go
Create table GrupoDiploma(
idGrupoDiploma int not null identity primary key,
nombre varchar(50) not null,
idColegio int not null foreign key references Colegio (idColegio)
)
go
Create table ListadeEstudiantes(
idListaEstudiante int not null identity primary key,
nombre varchar(50) not null,
apellidoPaterno varchar(50) not null,
apellidoMaterno varchar(50) not null,
correo varchar(80) unique not null,
pararelo varchar(50) not null,
promedio float not null,
idGrupoDiploma int not null foreign key references GrupoDiploma (idGrupoDiploma),
idGestion int not null foreign key references Gestion(idGestion)
)
go
Create table Legalizacion(
idLegalizacion int not null identity primary key,
estado varchar(30) not null,
descripcion varchar(100) not null,
firmaDigital varchar(800) not null,
fechaL datetime not null,
nroRegistroMins int not null foreign key references PersonalMinisterio (nroRegistroMins)
)
go
Create table Diploma(
idDiploma int not null identity primary key,
fecha datetime not null,
metadatos varchar(300) not null,
codigohex varchar(300) unique not null,
idLegalizacion int null foreign key references Legalizacion (idLegalizacion),
idGrupoDiploma int not null foreign key references GrupoDiploma (idGrupoDiploma)
)
go
Create table Estudiante(
nroRegistroEst int not null identity primary key,
loginEstudiante varchar(30) not null,
passEstudiante varchar(100) not null,
correo varchar(80) unique not null,
CI int not null foreign key references Persona (CI),
idDiploma int not null foreign key references Diploma(idDiploma),
idListaEstudiante int not null foreign key references ListadeEstudiantes (idListaEstudiante)
)
go
Create table PColegio_Rol(
idRol int not null foreign key references Rol (idRol),
nroRegistroPColegio int not null foreign key references PersonalColegio (nroRegistroPColegio)
)
go
create table Bitacora
(
	codbit int not null identity primary key, 
	descripcion varchar(300) not null,
	fecha datetime not null,
	terminal varchar(100) not null,
	usuario varchar(100) not null,
	aplicacion varchar(100) not null
)