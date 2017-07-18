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
Create table Rol(
idRol int not null identity primary key,
nombre varchar(30) not null,
descripcion varchar(300) not null
)
go
Create table PersonalMinisterio(
nroRegistroMins int not null identity primary key,
loginMinistro varchar(30) not null,
passMinistro varchar(100) not null,
correo varchar(80) unique not null,
firmaDigital varchar(500) not null unique,
CI int not null foreign key references Persona (CI),
idRol int not null foreign key references Rol(idRol)
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
nroRegistroPColegio int not null foreign key references PersonalColegio(nroRegistroPColegio)
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
fechaL date not null,
nroRegistroMins int not null foreign key references PersonalMinisterio (nroRegistroMins)
)
go
Create table Diploma(
idDiploma int not null identity primary key,
fecha date not null,
metadatos varchar(300) not null,
codigohex varchar(300) unique not null,
idLegalizacion int null foreign key references Legalizacion (idLegalizacion),
codigoLegalizacion varchar(800) null,
idGrupoDiploma int not null foreign key references GrupoDiploma (idGrupoDiploma)
)
go
Create table Estudiante(
nroRegistroEst int not null identity primary key,
loginEstudiante varchar(30) not null,
passEstudiante varchar(100) not null,
correo varchar(80) unique not null,
CI int not null foreign key references Persona (CI),
idDiploma int null foreign key references Diploma(idDiploma),
idListaEstudiante int not null foreign key references ListadeEstudiantes (idListaEstudiante),
idRol int not null foreign key references Rol(idRol)
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
-----------------------------------------
use master
go
drop database ProIntBD
--------------------------------------------
insert into Persona values (8177406,'Jose Gerardo','Solano','Romero',70200974,'Calle Cazorla #3415')
insert into Persona values (1111222,'Martha','Menacho','Ruiz',72343235,'Calle Mayor Cl #1241')
insert into Persona values (1111223,'Ricardo','Arauz','Medina',72412354,'Calle Cañoto #2555')
insert into Persona values (1111111,'Carlos','Roca','Chavez',71232425,'Calle Melchor #345')
insert into Persona values (2222222,'Jose','Suarez','Melgar',75322145,'Calle 2 #442')
insert into Persona values (3333333,'Marco','Cardozo','Peralta',70298755,'Calle Rocha #4425')
insert into Persona values (4444444,'Estefani','Suarez','Melgar',70200532,'Calle Mendez #110')
insert into Persona values (5555555,'Ricardo','Suarez','Rocha',72312324,'Av Brasi #213')
insert into Persona values (6666666,'Juan','Rodriguez','Romero',72312343,'Calle Aroma #3124')
insert into Persona values (7777777,'Sarah','Melgar','Lopez',71234312,'Av. Paraguay #3532')

insert into Rol values ('Administrador','Tiene todos los privilegios sobre todas las areas')
insert into Rol values ('PColegio','Tienen control de ABM sobre los grupos de Diplomas, Diplomas, Lista de Estudiantes')
insert into Rol values ('Estudiante','Solo tiene la posibilidad de Visualizar su propio diploma y descargarlo')
insert into Rol values ('Ministro','Tiene acceso a creacion de Colegios y la legalizacion de diplomas')

insert into PersonalMinisterio values ('Mins1','Mins1','josesolano@outlook.com','JSR',8177406,4)
insert into PersonalMinisterio values ('Admin','Admin','Admin@outlook.com','ADM',7777777,1)

insert into Colegio values ('DonBosco','Av Argentina','dbcentral@dbosco.com',1)
insert into Colegio values ('Uboldi','Av Las Americas','uboldicentral@uboldi.com',1)
insert into Colegio values ('Salle','Av Salle','sallecentral@salle.com',1)

insert into PersonalColegio values ('donbosco','donbosco','dbosco@dbosco.com',1111111,1)
insert into PersonalColegio values ('uboldi','uboldi','uboldi@uboldi.com',1111222,2)
insert into PersonalColegio values ('salle','salle','salle@salle.com',1111223,3)


insert into Privilegios values ('Total','tiene acceso a todo',1)

insert into Privilegios values ('Añadir G. Diplomas','Puede Añadir dentro de la tabla G.Diplomas',2)
insert into Privilegios values ('Borrar G.Diplomas','Puede Añadir dentro de la tabla G.Diplomas',2)
insert into Privilegios values ('Modificar G.Diplomas','Puede Añadir dentro de la tabla G.Diplomas',2)

insert into Privilegios values ('Visualizar Diplomas','Puede Visualizar el diploma',3)

insert into Privilegios values ('Añadir Legalizacion','Legalizacion de una tanda de diplomas',4)

insert into Gestion values ('Grobs','2012',1)
insert into Gestion values ('Zeidach','2016',2)
insert into Gestion values ('Yeet','2015',3)

insert into GrupoDiploma values ('Grupo Diploma de Don Bosco',1)
insert into GrupoDiploma values ('Grupo Diploma de Uboldi',2)
insert into GrupoDiploma values ('Grupo Diploma de Salle',3)

-----------------------------Insert Lista Estudiantes por CSV----------------
Bulk insert ListadeEstudiantes
from 'D:\ListaEstudiantes.csv'
with(
	--KeepIdentity,
	FieldTerminator = ';',
	Rowterminator = '\n',
	firstrow = 2
)
truncate table ListadeEstudiantes
select * from ListadeEstudiantes
-------------------------------------------------------------------------------

insert into Legalizacion values ('Realizado','Grupo de Diplomas','A001-JSR','07/01/2013',1)
insert into Legalizacion values ('Realizado','Grupo de Diplomas','A002-JSR','07/01/2013',1)
insert into Legalizacion values ('Realizado','Grupo de Diplomas','A003-JSR','07/01/2013',1)
insert into Legalizacion values ('Realizado','Grupo de Diplomas','A004-JSR','07/01/2013',1)
insert into Legalizacion values ('Realizado','Grupo de Diplomas','A005-JSR','07/01/2013',1)

insert into Diploma values ('20/11/2012','Estudiante Normal','A001',1,'A001-JSR',1)
insert into Diploma values ('20/11/2012','Estudiante Normal','A002',2,'A002-JSR',1)
insert into Diploma values ('20/11/2012','Estudiante sobresaliente','A003',3,'A003-JSR',1)
insert into Diploma values ('20/11/2012','Estudiante sobresaliente','A004',4,'A004-JSR',1)
insert into Diploma values ('20/11/2012','Estudiante sobresaliente','A005',5,'A005-JSR',1)

insert into Diploma values ('15/11/2016','Estudiante Normal','B001',null,null,2)
insert into Diploma values ('15/11/2016','Estudiante sobresaliente','B002',null,null,2)
insert into Diploma values ('15/11/2016','Estudiante Normal','B003',null,null,2)
insert into Diploma values ('15/11/2016','Estudiante sobresaliente','B004',null,null,2)

insert into Diploma values ('05/11/2015','Estudiante sobresaliente','C001',null,null,3)
insert into Diploma values ('05/11/2015','Estudiante sobresaliente','C002',null,null,3)
--insert into Diploma values ('05/11/2015','Estudiante flojo','C003',null,null,3)--Reprobo
insert into Diploma values ('05/11/2015','Estudiante normal','C004',null,null,3)
insert into Diploma values ('05/11/2015','Estudiante normal','C005',null,null,3)

insert into Estudiante values ('Jsuarez','Est01','Jsuarez@dbosco.com',2222222,2,2,3)
insert into Estudiante values ('Mcardozo','Est02','Mcardozo@uboldi.com',3333333,8,8,3)
insert into Estudiante values ('Esuarez','Est03','Esuarez@salle.com',4444444,null,12,3)

insert into PColegio_Rol values (1,2)
insert into PColegio_Rol values (2,2)
insert into PColegio_Rol values (3,2)
-----------------------------------------VIEWS----------------------------------
Create View Vista_PC_Rol
as
select loginPColegio, passPColegio, idRol 
from PersonalColegio pc inner join PColegio_Rol pcr on pcr.nroRegistroPColegio = pc.nroRegistroPColegio


select loginPColegio, passPColegio, idRol 
from PersonalColegio pc inner join PColegio_Rol pcr on pcr.nroRegistroPColegio = pc.nroRegistroPColegio
where pc.loginPColegio = 'salle'

select * from Estudiante