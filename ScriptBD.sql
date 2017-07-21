Create Database ProIntBD
go
use ProIntBD
go

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
Create table Diploma(
idDiploma int not null identity primary key,
fecha date not null,
metadatos varchar(300) not null,
codigohex varchar(300) unique not null,
codigoLegalizacion varchar(800) null,
idListaEstudiante int not null,
idGrupoDiploma int not null foreign key references GrupoDiploma (idGrupoDiploma)
)
go
Create table ListadeEstudiantes(
idListaEstudiante int not null identity primary key,
nombre varchar(50) not null,
apellidoPaterno varchar(50) not null,
apellidoMaterno varchar(50) not null,
correo varchar(80) unique not null,
paralelo varchar(50) not null,
promedio float not null,
idGrupoDiploma int not null foreign key references GrupoDiploma (idGrupoDiploma),
idGestion int not null foreign key references Gestion(idGestion),
idDiploma int null foreign key references Diploma(idDiploma)
)
go
Create table Legalizacion(
idLegalizacion int not null identity primary key,
firmaDigital varchar(800)null,
fechaL date null,
nroRegistroMins int not null foreign key references PersonalMinisterio (nroRegistroMins),
idDiploma int not null foreign key references Diploma(idDiploma)

insert into Legalizacion values (null,null,1,10)
)
go
Create table Estudiante(
nroRegistroEst int not null identity primary key,
loginEstudiante varchar(30) not null,
passEstudiante varchar(100) not null,
correo varchar(80) unique not null,
CI int not null foreign key references Persona (CI),
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
go
Create table Temp_ListadeEstudiantes(
idListaEstudiante int null,
nombre varchar(50) null,
apellidoPaterno varchar(50) null,
apellidoMaterno varchar(50) null,
correo varchar(80) unique null,
paralelo varchar(50) null,
promedio float null,
idGrupoDiploma int null,
idGestion int null,
idDiploma int null
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

insert into PColegio_Rol values (2,1)
insert into PColegio_Rol values (2,2)
insert into PColegio_Rol values (2,3)

insert into Gestion values ('Grobs','2012',1)
insert into Gestion values ('Zeidach','2016',2)
insert into Gestion values ('Yeet','2015',3)

insert into GrupoDiploma values ('Grupo Diploma de Don Bosco',1)
insert into GrupoDiploma values ('Grupo Diploma de Uboldi',2)
insert into GrupoDiploma values ('Grupo Diploma de Salle',3)

-------------------------------------------------------------------------------
insert into Legalizacion values (null,null,1,1)
insert into Legalizacion values (null,null,1,2)
insert into Legalizacion values (null,null,1,3)
insert into Legalizacion values (null,null,1,4)
insert into Legalizacion values (null,null,1,5)

insert into Estudiante values ('Jsuarez','Jsuarez','Jsuarez@dbosco.com',2222222,2,3)
insert into Estudiante values ('Mcardozo','Mcardozo','Mcardozo@uboldi.com',3333333,8,3)
insert into Estudiante values ('Esuarez','Esuarez','Esuarez@salle.com',4444444,12,3)
insert into Estudiante values ('Esuarez','Esuarez','sdwaesd@salle.com',5555555,13,3)

-----------------------------------------VIEWS----------------------------------
Create View Vista_PC_Rol
as
select loginPColegio, passPColegio, idRol 
from PersonalColegio pc inner join PColegio_Rol pcr on pcr.nroRegistroPColegio = pc.nroRegistroPColegio
--------------------------------------------------------------------------------------
Create View Vista_ListaE
as
select *
from ListadeEstudiantes
------------------------------------------------------------------------------------
Create View Vista_Perfil
as
select LE.idListaEstudiante as id, CONCAT(p.nombre,' ',p.apellidoPaterno,' ',p.apellidoMaterno) as nombreCompleto, telefono, p.direccion, p.CI,
		paralelo, promedio, G.nombrePromo, G.Año as año, fecha, C.nombre as colegio, metadatos, codigohex, codigoLegalizacion
				from Estudiante E
				inner join Persona p on p.CI = E.CI
				inner join ListadeEstudiantes LE on LE.idListaEstudiante = E.idListaEstudiante
				full outer join Diploma D on D.idDiploma = LE.idDiploma
				inner join Gestion G on G.idGestion = LE.idGestion
				inner join Colegio C on C.idColegio = G.idColegio
--------------------------------------------------------------------------------------------------------------------
Create View Vista_NoPerfil
as
select LE.idListaEstudiante as id, CONCAT(p.nombre,' ',p.apellidoPaterno,' ',p.apellidoMaterno) as nombreCompleto, telefono, p.direccion, p.CI,
		paralelo, promedio, G.nombrePromo, G.Año as año, C.nombre as colegio
				from Estudiante E
				inner join Persona p on p.CI = E.CI
				inner join ListadeEstudiantes LE on LE.idListaEstudiante = E.idListaEstudiante
				full outer join Diploma D on D.idDiploma = LE.idDiploma
				inner join Gestion G on G.idGestion = LE.idGestion
				inner join Colegio C on C.idColegio = G.idColegio
------------------------------------------TRIGGERS-------------------------------------------------------

------------------------------------------------------------------------------------------------------------------------
Create trigger tg_Diploma_LE on Diploma for insert
as
begin
declare @ID as int
declare @idLE as int

set @ID = (select idDiploma from inserted)
set @idLE = (select idListaEstudiante from inserted)
update ListadeEstudiantes set idDiploma = @ID where idListaEstudiante = @idLE
end
--------------------------------------------------------------------------------------------------------------------------
Create trigger tg_Estudiante on Estudiante for insert
as
begin
declare @ID as int
declare @IDLE as int
declare @Pass as varchar(50)
declare @PassMD5 as varchar(50)
declare @Correo as varchar(80)
declare @CorreoI as varchar(80)

set @ID = (select nroRegistroEst from inserted)
set @CorreoI = (select correo from inserted)
set @IDLE = (select idListaEstudiante from inserted)
set @Correo = (select correo from ListadeEstudiantes where idListaEstudiante = @IDLE )

if(@CorreoI = @Correo)
	begin
		set @ID = (select nroRegistroEst from inserted)
		set @Pass  = (select passEstudiante from inserted)
		set @PassMD5 = CONVERT(VARCHAR(32), HashBytes('MD5', @Pass), 2)
		update Estudiante set passEstudiante = @PassMD5 where nroRegistroEst = @ID
	end
else
	begin
		Delete from Estudiante where nroRegistroEst = @ID
	end
end

-----------------------------------------------------------------------------------------------------------------
Create trigger tg_Legal on Legalizacion for insert
as
begin
declare @ID as int
declare @FirDig as varchar(50)
declare @FirDigMD5 as varchar(300)
declare @FecLeg as date 
declare @IDM as int
declare @IDD as int

declare @CODDIP as varchar(300)
declare @CODDIPMD5 as varchar(800)
declare @CODLEG as varchar(800)
declare @CODLEGMD5 as varchar(800)
declare @CODMINS as varchar(300)
declare @CODMINSMD5 as varchar(800)

set @IDM = (select nroRegistroMins from inserted)	
set @ID = (select idLegalizacion from inserted)
set @IDD = (select idDiploma from inserted)

set @FecLeg = GETDATE()
set @CODMINS = (select firmaDigital from PersonalMinisterio where nroRegistroMins = @IDM)
set @CODMINSMD5 = CONVERT(VARCHAR(32), HashBytes('MD5', @CODMINS), 2)

set @CODDIPMD5 = (select codigohex from Diploma where idDiploma = @IDD)

set @CODLEG = @CODMINSMD5+''+@CODDIPMD5
set @CODLEGMD5 = CONVERT(VARCHAR(32), HashBytes('MD5', @CODLEG), 2)

update Diploma set codigoLegalizacion = @CODLEGMD5 where idDiploma = @IDD
update Legalizacion set firmaDigital = @CODLEGMD5, fechaL = @FecLeg where idLegalizacion = @ID
end
--------------------------------------------------------------------------------------
drop trigger tg_Legal

-------------------------------------PROCEDIMIENTOS ALMACENADOS------------------------------------------------
Create Procedure sp_ImportCSV
@csvPath varchar(50)
as
begin
EXEC('BULK INSERT Temp_ListadeEstudiantes FROM '''+ @csvPath +''' WITH ( FIRSTROW = 2, FIELDTERMINATOR = '';'',ROWTERMINATOR = ''\n'')')

	-- delete headings
	INSERT INTO ListadeEstudiantes
			   (
			   nombre,
			   apellidoPaterno,
			   apellidoMaterno,
			   correo,
			   paralelo,
			   promedio,
			   idGrupoDiploma,
			   idGestion,
			   idDiploma
			   )
		 SELECT 
			   nombre,
			   apellidoPaterno,
			   apellidoMaterno,
			   correo,
			   paralelo,
			   promedio,
			   idGrupoDiploma,
			   idGestion,
			   null
		 FROM Temp_ListadeEstudiantes
	-- clear table
	TRUNCATE TABLE Temp_ListadeEstudiantes
end

execute sp_ImportCSV 'D:\ListaEstudiantes.csv'

---------------------------------------------------------------------------------------------------------------
Create procedure sp_CrearDiplomas
as
begin
-----variables List Estudiantes
declare @idLe as int
declare @nomLe as varchar(50)
declare @appPLe as varchar(50)
declare @appMLe as varchar(50)
declare @corLe as varchar(80)
declare @paraLe as varchar(50)
declare @promLe as float
declare @idGPDLe as int
declare @idGesLe as int
declare @idDLe as int
declare @CodlegN as int

-----variables Diploma----

declare @Fec as date
declare @MetaDat as Varchar(300)
declare @CodH as varchar(300)
declare @CodLeg as varchar(800) 

declare Vuelta cursor for
select idListaEstudiante,nombre,apellidoPaterno,apellidoMaterno,correo,paralelo,promedio,idGrupoDiploma,idGestion,idDiploma
from Vista_ListaE

open Vuelta
fetch next from Vuelta into @idLe,@nomLe,@appPLe,@appMLe,@corLe,@paraLe,@promLe,@idGPDLe,@idGesLe,@idDLe
while (@@FETCH_STATUS = 0)
	begin
		if(@idDLe = 0 or @idDLe is null)
		begin
			set @Fec = GETDATE()
			set @MetaDat = 'Certificado de Bachiller'
			set @CodlegN = (FLOOR(rand()*(20-51)+51))*@idLe
			set @CodH = CONVERT(VARCHAR(32), HashBytes('MD5', convert(char(800),@CodlegN) ), 2)
			set @CodLeg = null

			if(@promLe>=51)
				begin
					insert into Diploma values (@Fec,@MetaDat,@CodH,@CodLeg,@idLe,@idGPDLe)
				end			
		end
	fetch next from Vuelta into @idLe,@nomLe,@appPLe,@appMLe,@corLe,@paraLe,@promLe,@idGPDLe,@idGesLe,@idDLe
	end
close Vuelta
deallocate Vuelta

select * from Diploma
end

drop procedure sp_CrearDiplomas

execute sp_CrearDiplomas

select * from Vista_ListaE
----------------------------------------------------------------------------------------------------------------------

update  PersonalColegio set passPColegio = 'baf8a7ba583324dc0a81070d3349a3d3' where nroRegistroPColegio = 1
update  PersonalColegio set passPColegio = '98302c0570ad746539c1eb637041c474' where nroRegistroPColegio = 2
update  PersonalColegio set passPColegio = 'aeabd5b1570d06a00c19d0d7e38d6f4a' where nroRegistroPColegio = 3

update  PersonalMinisterio  set passMinistro = '1c51a52b9d45a32b5779f7a020950093' where nroRegistroMins = 1
update  PersonalMinisterio  set passMinistro = 'e3afed0047b08059d0fada10f400c1e5' where nroRegistroMins = 2
-------------------------------------------------------------------Reportes---------------------------------------------
create view VR1
as
select D.idDiploma, CONVERT(VARCHAR(10), D.fecha, 103) as FechaDiploma, D.codigohex, D.metadatos, G.nombrePromo, 
					CONCAT(le.nombre,' ',le.apellidoPaterno,' ',le.apellidoMaterno) as NombreCompleto, C.nombre as NombreColegio, G.año as Año  
		from Diploma D
		inner join ListadeEstudiantes le on le.idDiploma = D.idDiploma
		inner join Gestion G on G.idGestion = le.idGestion
		inner join Colegio C on C.idcolegio = G.idColegio
		--Where C.nombre = 'DonBosco' and G.año = '2012'

create procedure sp_reporte1
@NomCole varchar(50),
@Año as int
as
begin
declare @idDip as int
declare @Fec as varchar(200)
declare @CodHex as varchar(200)
declare @Meta as varchar(200)
declare @nomPro as varchar(200)
declare @nomCom as varchar(200)
declare @nomCol as varchar(200)
declare @AñoG as int
declare @Row as int
declare @Nro as int

set @Nro = 0
set @Row = 0

declare CurRep1 cursor for 
select idDiploma, FechaDiploma, codigohex, metadatos, nombrePromo, NombreCompleto, NombreColegio, Año
from VR1
where NombreColegio like '%'+@NomCole+'%' and Año = @Año

open CurRep1

fetch next from CurRep1 into @idDip, @Fec, @CodHex, @Meta, @nomPro, @nomCom, @nomCol, @AñoG
while (@@FETCH_STATUS = 0)
begin

set @Nro = @Nro +1
set @Row = @Row + 1

fetch next from CurRep1 into @idDip, @Fec, @CodHex, @Meta, @nomPro, @nomCom, @nomCol, @AñoG

end

close CurRep1
deallocate CurRep1

select @NomCole as NombreColegioEleg, @Año as AñoEleg, @Nro as Numero, @Row as Cantidad, idDiploma, FechaDiploma, codigohex, metadatos, nombrePromo, NombreCompleto, NombreColegio, Año
from VR1
where NombreColegio like '%'+@NomCole+'%' and Año = @Año

end

execute sp_reporte1 'DonBosco',2012
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
create view VR2
as
select D.idDiploma, CONVERT(VARCHAR(10), D.fecha, 103) as FechaDiploma, D.codigohex, D.codigolegalizacion, D.metadatos, G.nombrePromo, L.fechaL,
					CONCAT(le.nombre,' ',le.apellidoPaterno,' ',le.apellidoMaterno) as NombreCompleto, C.nombre as NombreColegio, G.año as Año  
		from Diploma D
		inner join ListadeEstudiantes le on le.idDiploma = D.idDiploma
		inner join Gestion G on G.idGestion = le.idGestion
		inner join Colegio C on C.idcolegio = G.idColegio
		inner join Legalizacion L on L.idDiploma = D.idDiploma
		--Where fechaL 

create procedure sp_reporte2
@MesEleg int,
@AñoEleg as int
as
begin
declare @idDip as int
declare @Fec as varchar(200)
declare @CodHex as varchar(200)
declare @CodLeg as varchar(200)
declare @Meta as varchar(200)
declare @nomPro as varchar(200)
declare @nomCom as varchar(200)
declare @nomCol as varchar(200)
declare @AñoG as int
declare @AñoL as Date
declare @Row as int
declare @Nro as int

set @Nro = 0
set @Row = 0

declare CurRep2 cursor for 
select idDiploma, FechaDiploma, codigohex, codigolegalizacion, metadatos, nombrePromo, NombreCompleto, NombreColegio, Año, fechaL
from VR2
Where YEAR(fechaL)= @AñoEleg and MONTH(fechaL) = @MesEleg

open CurRep2

fetch next from CurRep2 into @idDip, @Fec, @CodHex, @CodLeg, @Meta, @nomPro, @nomCom, @nomCol, @AñoG, @AñoL
while (@@FETCH_STATUS = 0)
begin

set @Nro = @Nro +1
set @Row = @Row + 1

fetch next from CurRep2 into @idDip, @Fec, @CodHex, @CodLeg, @Meta, @nomPro, @nomCom, @nomCol, @AñoG, @AñoL

end

close CurRep2
deallocate CurRep2

select @MesEleg as MesEleg, @AñoEleg as AñoEleg, @Nro as Numero, @Row as Cantidad, idDiploma, FechaDiploma, codigohex, codigolegalizacion, metadatos, nombrePromo, NombreCompleto, NombreColegio, Año, fechaL
from VR2
Where YEAR(fechaL)= @AñoEleg and MONTH(fechaL) = @MesEleg

end

execute sp_reporte2 7,2017

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
create view VR3
as
select D.idDiploma, CONVERT(VARCHAR(10), D.fecha, 103) as FechaDiploma, D.codigohex, D.codigolegalizacion, D.metadatos, G.nombrePromo,
					CONCAT(le.nombre,' ',le.apellidoPaterno,' ',le.apellidoMaterno) as NombreCompleto, C.nombre as NombreColegio, G.año as Año  
		from Diploma D
		inner join ListadeEstudiantes le on le.idDiploma = D.idDiploma
		inner join Gestion G on G.idGestion = le.idGestion
		inner join Colegio C on C.idcolegio = G.idColegio
		--Where G.Año = 2015 and codigolegalizacion is null

create procedure sp_reporte3
@AñoEleg as int
as
begin
declare @idDip as int
declare @Fec as varchar(200)
declare @CodHex as varchar(200)
declare @CodLeg as varchar(200)
declare @Meta as varchar(200)
declare @nomPro as varchar(200)
declare @nomCom as varchar(200)
declare @nomCol as varchar(200)
declare @AñoG as int
declare @Row as int
declare @Nro as int

set @Nro = 0
set @Row = 0

declare CurRep3 cursor for 
select idDiploma, FechaDiploma, codigohex, codigolegalizacion, metadatos, nombrePromo, NombreCompleto, NombreColegio, Año
from VR3
where Año = @AñoEleg and codigolegalizacion is null

open CurRep3

fetch next from CurRep3 into @idDip, @Fec, @CodHex, @CodLeg, @Meta, @nomPro, @nomCom, @nomCol, @AñoG
while (@@FETCH_STATUS = 0)
begin

set @Nro = @Nro +1
set @Row = @Row + 1

fetch next from CurRep3 into @idDip, @Fec, @CodHex, @CodLeg, @Meta, @nomPro, @nomCom, @nomCol, @AñoG

end

close CurRep3
deallocate CurRep3

select  @AñoEleg as AñoEleg, @Nro as Numero, @Row as Cantidad, idDiploma, FechaDiploma, codigohex, codigolegalizacion, metadatos, nombrePromo, NombreCompleto, NombreColegio, Año
from VR3
where Año = @AñoEleg and codigolegalizacion is null

end


execute sp_reporte3 2015
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
create view VR4
as
select C.nombre, C.direccion, C.correo, CONCAT(p.nombre,' ',p.apellidoPaterno,' ',p.apellidoMaterno) as NombreCompleto, 
				R.nombre as Rol
				from Colegio C
						inner join PersonalColegio PC on PC.idColegio = C.idColegio
						inner join Persona P on P.CI = PC.CI
						inner join PColegio_Rol PCR on PCR.nroRegistroPColegio = PC.nroRegistroPColegio
						inner join Rol R on R.idRol = PCR.idRol

		--Where C.nombre = 'DonBosco' and G.año = '2012'

create procedure sp_reporte4
as
begin
declare @Nom as varchar(100)
declare @Dir as varchar(100)
declare @Correo as varchar(100)
declare @nomCom as varchar(100)
declare @Rol as varchar(100)
declare @Row as int
declare @Nro as int

set @Nro = 0
set @Row = 0

declare CurRep4 cursor for 
select nombre,direccion,correo,NombreCompleto,Rol
from VR4

open CurRep4

fetch next from CurRep4 into @Nom, @Dir, @Correo, @nomCom, @Rol
while (@@FETCH_STATUS = 0)
begin

set @Nro = @Nro +1
set @Row = @Row + 1

fetch next from CurRep4 into @Nom, @Dir, @Correo, @nomCom, @Rol
end

close CurRep4
deallocate CurRep4

select @Nro as Numero, @Row as Cantidad, nombre,direccion,correo,NombreCompleto,Rol
from VR4

end

execute sp_reporte4
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
create view VR5
as
select CONCAT(le.nombre,' ',le.apellidoPaterno,' ',le.apellidoMaterno) as NombreCompleto, G.nombrePromo, C.nombre as NombreColegio, G.año as Año, promedio 
		from  ListadeEstudiantes le 
		inner join Gestion G on G.idGestion = le.idGestion
		inner join Colegio C on C.idcolegio = G.idColegio
		where idDiploma is null

create procedure sp_reporte5

as
begin
declare @nomCom as varchar(200)
declare @nomPro as varchar(200)
declare @nomCol as varchar(200)
declare @AñoG as int
declare @Promedio as int
declare @Row as int
declare @Nro as int

set @Nro = 0
set @Row = 0

declare CurRep5 cursor for 
select NombreCompleto,nombrePromo,NombreColegio,Año,promedio
from VR5

open CurRep5

fetch next from CurRep5 into @nomCom, @nomPro, @nomCol, @AñoG, @Promedio
while (@@FETCH_STATUS = 0)
begin

set @Nro = @Nro +1
set @Row = @Row + 1

fetch next from CurRep5 into @nomCom, @nomPro, @nomCol, @AñoG, @Promedio

end

close CurRep5
deallocate CurRep5

select @Nro as Numero, @Row as Cantidad, NombreCompleto,nombrePromo,NombreColegio,Año,promedio
from VR5

end

execute sp_reporte5
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
create trigger tg_alta_Colegio on Colegio for insert
as
declare @Nom as varchar(30)
declare @Codclie as int
set @Nom = (select nombre from inserted)
set @Codclie = (select idColegio from inserted)
insert into Bitacora values('Se guardo Colegio: '+@Nom+' con id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_baja_Colegio on Colegio for delete
as
declare @Nom as varchar(30)
declare @Codclie as int
set @Nom = (select nombre from inserted)
set @Codclie = (select idColegio from inserted)
insert into Bitacora values('Se elimino Colegio: '+@Nom+' con id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_Mod_Colegio on Colegio for update
as
declare @Nom as varchar(30)
declare @Codclie as int
set @Nom = (select nombre from inserted)
set @Codclie = (select idColegio from inserted)
insert into Bitacora values('Se modifico Colegio: '+@Nom+' con id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
create trigger tg_alta_Legalizacion on Legalizacion for insert
as
declare @Codclie as int
set @Codclie = (select idLegalizacion from inserted)
insert into Bitacora values('Se guardo Legalizacion: con id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_baja_Legalizacion on Legalizacion for delete
as
declare @Codclie as int
set @Codclie = (select idLegalizacion from inserted)
insert into Bitacora values('Se elimino Legalizacion: con id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_Mod_Legalizacion on Legalizacion for update
as
declare @Codclie as int
set @Codclie = (select idLegalizacion from inserted)
insert into Bitacora values('Se modifico Legalizacion: con id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())

drop trigger tg_alta_Legalizacion
drop trigger tg_baja_Legalizacion
drop trigger tg_Mod_Legalizacion
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
create trigger tg_alta_GDiploma on GrupoDiploma for insert
as
declare @Nom as varchar(300)
declare @Codclie as int
set @Nom = (select nombre from inserted)
set @Codclie = (select idGrupoDiploma from inserted)
insert into Bitacora values('Se guardo GrupoDiploma: '+@Nom+' con id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_baja_GDiploma on GrupoDiploma for delete
as
declare @Nom as varchar(30)
declare @Codclie as int
set @Nom = (select nombre from inserted)
set @Codclie = (select idGrupoDiploma from inserted)
insert into Bitacora values('Se elimino GrupoDiploma: '+@Nom+' con id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_Mod_GDiploma on GrupoDiploma for update
as
declare @Nom as varchar(30)
declare @Codclie as int
set @Nom = (select nombre from inserted)
set @Codclie = (select idGrupoDiploma from inserted)
insert into Bitacora values('Se modifico GrupoDiploma: '+@Nom+' con id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
select * from Bitacora

select * from legalizacion
select * from Diploma